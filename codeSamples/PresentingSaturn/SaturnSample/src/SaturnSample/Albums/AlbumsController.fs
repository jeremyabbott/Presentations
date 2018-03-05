namespace Albums

open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.ContextInsensitive
open Config
open Saturn

module Controller =

  let indexAction (ctx : HttpContext) =
    task {
      let cnf = Controller.getConfig ctx
      let! result = Database.getAll cnf.connectionString
      match result with
      | Ok result ->
        return! Controller.json ctx (Seq.map Database.toAlbum result)
      | Error ex ->
        return raise ex
    }

  let showAction (ctx: HttpContext, id : string) =
    task {
      let cnf = Controller.getConfig ctx
      let! result = Database.getById cnf.connectionString id
      match result with
      | Ok (Some result) ->
        return! Controller.json ctx result
      | Ok None ->
        return! Response.notFound ctx "Value not fund"
      | Error ex ->
        return raise ex
    }

  let createAction (ctx: HttpContext) =
    task {
      let! input = Controller.getModel<Album> ctx
      let validateResult = Validation.validate input
      if validateResult.IsEmpty then

        let cnf = Controller.getConfig ctx
        let! result = Database.insert cnf.connectionString input
        match result with
        | Ok _ ->
          return! Response.ok ctx ""
        | Error ex ->
          return raise ex
      else
        return! Response.badRequest ctx "Validation failed"
    }

  let updateAction (ctx: HttpContext, id : string) =
    task {
      let! input = Controller.getModel<Album> ctx
      let validateResult = Validation.validate input
      if validateResult.IsEmpty then
        let cnf = Controller.getConfig ctx
        let! result = Database.update cnf.connectionString input
        match result with
        | Ok _ ->
          return! Response.ok ctx ""
        | Error ex ->
          return raise ex
      else
        return! Response.badRequest ctx "Validation failed"
    }

  let deleteAction (ctx: HttpContext, id : string) =
    task {
      let cnf = Controller.getConfig ctx
      let! result = Database.delete cnf.connectionString id
      match result with
      | Ok _ ->
        return! Response.ok ctx ""
      | Error ex ->
        return raise ex
    }

  let resource = controller {
    index indexAction
    show showAction
    create createAction
    update updateAction
    delete deleteAction
  }

