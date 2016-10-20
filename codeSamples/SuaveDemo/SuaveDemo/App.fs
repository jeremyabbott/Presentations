// Learn more about F# at http://fsharp.org

open Suave
open SuaveDemo
open System.Net

[<EntryPoint>]
let main argv = 
    
    // Override the default port
    let config =
        { defaultConfig with
            bindings = [ HttpBinding.mk HTTP IPAddress.Loopback (uint16 8084)] }
    
    // config -> WebPart -> unit
    //startWebServer config <| Simple.simpleApp()
    startWebServer config <| Rest.concertApp()
    0 // return an integer exit code
