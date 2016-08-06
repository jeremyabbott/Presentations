open System
open System.Net
open Microsoft.FSharp.Control.WebExtensions

// *********************************
// Pipes vs. Composition
// *********************************
// Pipe operator
let sum =
    [|1..10|] 
    |> Array.filter (fun s -> s % 2 = 0) // array becomes last argument to Array.filter
    |> Array.sum // The filtered array becomes the last argument to Array.sum

// Partial Application
let evens = Array.filter (fun s -> s % 2 = 0) // create a partially applied function

// Compose
let sumEvens = evens >> Array.sum // compose events with Array.Sum
let sum' = sumEvens [|1..10|] // sumEvens takes an array and returns an int

// More Compose
let sumEvens' = // Output of first function must match input of second function
    Array.filter (fun s -> s % 2 = 0) >> Array.sum // Partially applied filter has an output of (int[] -> int[])
let sum'' = sumEvens [|1..10|]

// *********************************
// Discriminated Union
// *********************************

// Basic Discriminated Union
type Shape =
| Circle of Radius : float
| Triangle of Base : float * Height : float
| Rectangle of Length : float * Height : float
    member x.getArea () = 
        match x with // pattern matching
        | Circle (r) -> (r ** 2.0) * System.Math.PI 
        | Triangle (b, h) -> 0.5 * (b * h)
        | Rectangle (l, h) -> l * h

// create a circle
let circle = Shape.Circle 5. 

printfn "%f" (circle.getArea ())

// Discriminated Union that compiles down to an Enum that C# can use
type Categories =
    | Sedan = 1
    | Truck = 2
    | SUV = 3
    | Coupe = 4

printfn "%d" <| int Categories.SUV // prints 2

// *********************************
// Basic Pattern Matching
// *********************************

let posNeg number =
    match number with
    | n when n > 0 -> "positive"
    | n when n < 0 -> "negative"
    | _ -> "zero"


printfn "%s" (posNeg -1) // negative

// Abbreviated syntax with function keyword

let posNeg' =
    function
    | n when n > 0 -> "positive"
    | n when n < 0 -> "negative"
    | _ -> "zero"

printfn "%s" (posNeg -1) // negative

// *********************************
// Pattern matching against a tuple
// *********************************
let points = [0, 0; 1, 0; 0, 1; -2, 3] 
let locatePoint p =
    match p with
    | (0, 0) -> sprintf "%A is at the origin" p
    | (_, 0) -> sprintf "%A is on the x-axis" p
    | (0, _) -> sprintf "%A is on the y-axis" p
    | (x, y) -> sprintf "%A is at x: %i, y: %i" p x y

// Combining patterns
let locatePoint' p =
    match p with
    | (0, 0) -> sprintf "%A is at the origin" p
    | (_, 0) | (0, _) -> sprintf "%A is on an axis" p
    | (x, y) -> sprintf "%A is at x: %i, y: %i" p x y
    

points |> List.map locatePoint |> List.iter (fun s -> printfn "%s" s)
points |> List.map locatePoint' |> List.iter (fun s -> printfn "%s" s)

// *********************************
// Pattern matching against Record Types
// *********************************
type Model =
    | Six
    | SixPlus
    | Five
    | FiveS

type Phone = { Manufacturer : string; Model : Model; OperatingSystem : string; Storage : int }

let phones = [{ Manufacturer = "Apple"; Model = Model.Six; OperatingSystem = "iOS"; Storage = 64 };
                { Manufacturer = "Apple"; Model = Model.Six; OperatingSystem = "iOS"; Storage = 128 };
                { Manufacturer = "Apple"; Model = Model.SixPlus; OperatingSystem = "iOS"; Storage = 64 };
                { Manufacturer = "Apple"; Model = Model.Five; OperatingSystem = "iOS"; Storage = 64 }]

let isNewEnough =
    function
    | { Model = Model.SixPlus } -> true
    | { Model = Model.Six } -> true
    | _ -> false

phones
|> List.filter isNewEnough // only want "new" ones
|> List.iter
    (fun p -> printfn "This phone is new enough - Manufacturer: %s, Storage: %i" p.Manufacturer p.Storage)

// *********************************
// Active Patterns
// *********************************

let (|XAxis|YAxis|Origin|Other|) p = // Create an active recognizer function
    match p with
    | (0, 0) -> Origin // order matters
    | (_, 0) -> XAxis
    | (0, _) -> YAxis    
    | _ -> Other

let locatePoint'' p =
    match p with
    | Origin -> sprintf "On the origin: %A" p
    | XAxis -> sprintf "On the x-axis: %A" p
    | YAxis -> sprintf "On the y-axis: %A" p
    | Other -> sprintf "Not on an axis: %A" p

points |> List.map locatePoint'' |> List.iter (fun s -> printfn "%s" s)

// *********************************
// Async
// *********************************

let asyncInt = async {
    let r = new System.Random()
    let n = r.Next(500, 2500)
    printfn "Here we go again %O" DateTime.Now.TimeOfDay
    do! Async.Sleep n
    printfn "Done at %O" DateTime.Now.TimeOfDay
    return n
    }

let x = Async.RunSynchronously asyncInt // wait for the async operation to finish

printfn "Test blocking %d" x

let asyncUnit = async {
    do asyncInt |> ignore
}

Async.Start asyncUnit // no way to get the result back at this point

// Child tasks
let timeOfDay () = System.DateTime.Now.TimeOfDay

let longWorkflow = async {
    printfn "starting child @ %A" <| timeOfDay ()
    do! Async.Sleep 1500
    printfn "finishing child @ %A" <| timeOfDay ()

    let r = new System.Random()
    return r.Next(10)
}

let parentWorkflow = async {

    printfn "Starting parent @ %A" <| timeOfDay ()

    let! childAsync = Async.StartChild longWorkflow // returns Async<'T> (int in this case)

    printfn "Do some work"
    do! Async.Sleep 1000

    printfn "Wait on the child task"
    let! result = childAsync

    printfn "Parent done"
}

Async.RunSynchronously parentWorkflow

printfn "Test not blocking"

// Async with continuations

let fetchHtml url = async {
    try
        let uri = new System.Uri(url)
        let webClient = new WebClient()
        let! html = webClient.AsyncDownloadString(uri)
        printfn "Read %d characters for %s" html.Length url
        printfn "First 50 characters are %s" <| html.Substring(0, 50)
    with
        | ex -> printfn "%s" (ex.Message);
}

Async.StartWithContinuations (
    fetchHtml "http://google.com",
    (fun _ -> printfn "success"),
    (fun _ -> printfn "not success"),
    (fun _ -> printfn "canceled"))

[|"http://github.com"; "http://fsharp.org"; "http://stackoverflow.com"|]

let v =
    [|"http://github.com"; "http://fsharp.org"; "http://stackoverflow.com"|]
    |> Array.map fetchHtml
    |> Async.Parallel
    |> Async.RunSynchronously

// *********************************
// Mailbox Processors/Agents
// *********************************

// common type alias for MailboxProcessors
type Agent<'T> = MailboxProcessor<'T>

let countingAgent = Agent.Start(fun inbox ->
    let rec messageLoop currentCount = async {
        let! msg = inbox.Receive()
        let newCount = currentCount + msg
        printfn "New count: %i" newCount
        return! messageLoop newCount
         
        }
    messageLoop 0
    )

countingAgent.Post 1 // New count: 1
countingAgent.Post 1 // New count: 2
countingAgent.Post 2 // New count: 4

type Message = // discriminated union representing the types of messages I can expect
    | Post of int
    | Reply of int * AsyncReplyChannel<string>

let postMessage =
    function // function-syntax pattern matching
    | Post i -> i
    | Reply (i, a) -> i

// Use a type annotation to make it clear what kind of message I'm going to receive
let boringBot = Agent.Start(fun (inbox : MailboxProcessor<Message>) ->
    let rec messageLoop currentMessage = async {
        let! msg = inbox.Receive() // get the message
        
        let currentState = postMessage currentMessage // get the current state
        let newState = postMessage msg // get the state of the message
        
        match msg with
        | Reply (i, a) ->
            let message = sprintf "New state: %i" (i + currentState)
            a.Reply (message)
        | _ -> ()
                
        return! messageLoop (Message.Post(newState + currentState))
    }
    messageLoop <| Message.Post 0
)

boringBot.Post <| Message.Post 5

let getReply v = boringBot.PostAndReply (fun rc -> Message.Reply(v, rc))

let reply = getReply 1

printfn "%s" reply