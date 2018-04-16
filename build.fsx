#I @"packages/FsReveal/fsreveal/"
#I @"packages/FAKE/tools/"
#I @"packages/Suave/lib/net40"

#r "FakeLib.dll"
#r "Suave.dll"

#load "fsreveal.fsx"

// Git configuration (used for publishing documentation in gh-pages branch)
// The profile where the project is posted

open FsReveal
open Fake
open Fake.Git
open System.IO
open System.Diagnostics
open Suave
open Suave.Operators
open Suave.Sockets
open Suave.Sockets.Control
open Suave.WebSocket
open Suave.Files

let gitUser = getBuildParam "githubuser"
let gitPassword = getBuildParam "githubpassword"

let outDir = __SOURCE_DIRECTORY__ </> "output"
let slidesDir = __SOURCE_DIRECTORY__ </> "slides"

Target "Clean" (fun _ ->
    CleanDirs [outDir]
)

let fsiEvaluator =
    let evaluator = FSharp.Literate.FsiEvaluator()
    evaluator.EvaluationFailed.Add(fun err ->
        traceImportant <| sprintf "Evaluating F# snippet failed:\n%s\nThe snippet evaluated:\n%s" err.StdErr err.Text )
    evaluator

let copyStylesheet() =
    try
        CopyFile (outDir </> "css" </> "custom.css") (slidesDir </> "custom.css")
    with
    | exn -> traceImportant <| sprintf "Could not copy stylesheet: %s" exn.Message

let copyPics() =
    try
      CopyDir (outDir </> "images") (slidesDir </> "images") (fun _ -> true)
    with
    | exn -> traceImportant <| sprintf "Could not copy picture: %s" exn.Message

let generateFor (file:FileInfo) =
    try
        // copyPics()
        let rec tryGenerate trials =
            try
                FsReveal.GenerateFromFile(file.FullName, outDir, fsiEvaluator = fsiEvaluator)
            with
            | _ when trials > 0 -> tryGenerate (trials - 1)
            | exn ->
                traceImportant <| sprintf "Could not generate slides for: %s" file.FullName
                traceImportant exn.Message

        tryGenerate 3

        // copyStylesheet()
    with
    | :? FileNotFoundException as exn ->
        traceImportant <| sprintf "Could not copy file: %s" exn.FileName

let refreshEvent = new Event<_>()

let handleWatcherEvents (events:FileChange seq) =
    for e in events do
        let fi = fileInfo e.FullPath
        traceImportant <| sprintf "%s was changed." fi.Name
        match fi.Attributes.HasFlag FileAttributes.Hidden || fi.Attributes.HasFlag FileAttributes.Directory with
        | true -> ()
        | _ -> generateFor fi
    refreshEvent.Trigger()

let socketHandler (webSocket : WebSocket) =
  fun _ -> socket {
    while true do
      let! _ =
        Control.Async.AwaitEvent(refreshEvent.Publish)
        |> Suave.Sockets.SocketOp.ofAsync
      do! webSocket.send Text ("refreshed" |> System.Text.Encoding.ASCII.GetBytes |> ByteSegment) true
  }

let startWebServer () =
    let rec findPort port =
        let portIsTaken =
            if isMono then false else
            System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners()
            |> Seq.exists (fun x -> x.Port = port)

        if portIsTaken then findPort (port + 1) else port

    let port = findPort 8083

    let serverConfig =
        { defaultConfig with
           homeFolder = Some (FullName outDir)
           bindings = [ HttpBinding.createSimple HTTP "127.0.0.1" port ]
        }
    let app =
      choose [
        Filters.path "/websocket" >=> handShake socketHandler
        Writers.setHeader "Cache-Control" "no-cache, no-store, must-revalidate"
        >=> Writers.setHeader "Pragma" "no-cache"
        >=> Writers.setHeader "Expires" "0"
        >=> browseHome ]
    startWebServerAsync serverConfig app |> snd |> Async.Start
    Process.Start (sprintf "http://localhost:%d/index.html" port) |> ignore

Target "GenerateSlides" (fun _ ->
    !! (slidesDir + "/**/*.md")
    ++ (slidesDir + "/**/*.fsx")
    |> Seq.map fileInfo
    |> Seq.iter generateFor
    copyPics()
    copyStylesheet()

    [   ".gitignore"
        ".travis.yml"
        "bower.json"
        "CONTRIBUTING.md"
        "Gruntfile.js"
        "LICENSE"
        "package.json"
        "paket.dependencies"
        "paket.version"
        "822a9c937c902807e376713760e1f07845951d5a.zip"]
    |> List.iter (fun s -> outDir </> s |> FileUtils.rm)

    outDir
        </> "reveal.js-822a9c937c902807e376713760e1f07845951d5a"
        |> FileUtils.rm_rf

)

Target "KeepRunning" (fun _ ->
    use watcher = !! (slidesDir + "/**/*.*") |> WatchChanges handleWatcherEvents

    startWebServer ()

    traceImportant "Waiting for slide edits. Press any key to stop."

    System.Console.ReadKey() |> ignore

    watcher.Dispose()
)

Target "ReleaseSlides" (fun _ ->
    match gitUser, gitPassword with
    | null, null -> failwith "Git username and password are required"
    | "", "" -> failwith "Git username and password are required"
    | u, p ->
        let publishDir = __SOURCE_DIRECTORY__ </> "publish"
        let repoUrl = "https://jeremypresentation@jeremypresents.scm.azurewebsites.net/jeremypresents.git"
        CleanDir publishDir

        Repository.cloneSingleBranch "" repoUrl "master" publishDir

        fullclean publishDir
        CopyRecursive outDir publishDir true |> tracefn "%A"
        Git.Staging.StageAll publishDir
        let commitMessage = sprintf "Publish slides %s" <| System.DateTime.Now.ToLongDateString()
        Git.Commit.Commit publishDir commitMessage
        Git.Branches.push publishDir

)
// Target "ReleaseSlides" (fun _ ->
//     if gitOwner = "myGitUser" || gitProjectName = "MyProject" then
//         failwith "You need to specify the gitOwner and gitProjectName in build.fsx"
//     let tempDocsDir = __SOURCE_DIRECTORY__ </> "temp/gh-pages"
//     CleanDir tempDocsDir
//     Repository.cloneSingleBranch "" (gitHome + "/" + gitProjectName + ".git") "gh-pages" tempDocsDir

//     fullclean tempDocsDir
//     CopyRecursive outDir tempDocsDir true |> tracefn "%A"
//     StageAll tempDocsDir
//     Git.Commit.Commit tempDocsDir "Update generated slides"
//     Branches.push tempDocsDir
// )

"Clean"
  ==> "GenerateSlides"
  ==> "KeepRunning"

"GenerateSlides"
==> "ReleaseSlides"

RunTargetOrDefault "KeepRunning"