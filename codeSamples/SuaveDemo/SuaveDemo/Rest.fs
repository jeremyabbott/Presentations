namespace SuaveDemo

open Suave
open Suave.Filters
open Suave.Operators
open Suave.RequestErrors
open SuaveDemo.Data
open SuaveDemo.Json

module Rest =

    // Records defining shape of API resources

    type GetListResource<'a> = unit -> seq<'a>
    type AddResource<'a> = 'a -> 'a

    type ApiResource<'a> =
    | List of GetListResource<'a>
    | Add of AddResource<'a>

    // Helper function for building out common resources for a given type
    let buildResource name (resource : ApiResource<'a>) =
        let resourcePath = path <| "/" + name

        resourcePath >=>
            match resource with
            | List(r) -> GET >=> (warbler(fun _ -> r() |> JSON))
            | Add(r) -> POST >=> request (getResourceFromReq >> r >> JSON)
        
    let concertListResource = buildResource "concert" (List(Data.getConcerts))
    let saveConcertResource = buildResource "concert" (Add(Data.addConcert))
    
    let concertResources = choose [concertListResource; saveConcertResource]

    let concertApp() =
        choose [
            concertResources
            NOT_FOUND "The resource you requested could not be found."
        ]