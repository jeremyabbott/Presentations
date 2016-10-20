namespace SuaveDemo

open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful

module Simple =

    let formatId = sprintf "Hello! %d was requested."
    let random = System.Random()
    let next () = random.Next(10).ToString()

    let helloPart = path "/hello" >=> (OK "Hello LRTF!") // simple route web part that returns 200 and a string
    let helloWithIdPath = pathScan "/hello/%d" // Simple route web part with strongly typed parameters
    let helloWithIdPart = helloWithIdPath (fun id -> OK <| formatId id) // work with the input of the strongly typed route
    let randomPart = path "/random" >=> (OK (next())) // this only gets executed once
    let randomFixedPart = path "/randomFixed" >=> (warbler(fun _ -> OK <| next())) // this gets executed on each request

    let simpleApp() =
        choose [
            helloPart
            helloWithIdPart
            randomPart
            randomFixedPart
        ]        