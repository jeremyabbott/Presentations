namespace SuaveDemo.Data

open System
open System.Collections.Generic

module Data =
    type Concert = {
        Id : int
        Band : string
        Venue : string
        Date : DateTime
    }

    let private database = Dictionary<int, Concert>()

    let getConcerts () =
        database.Values :> seq<Concert>

    let addConcert concert =
        let id = database.Count + 1
        let concertToAdd = { concert with Id = id }
        database.Add(id, concertToAdd)
        concertToAdd