module Router

open Saturn
open Giraffe.Core
open Giraffe.ResponseWriters
open FSharp.Control.Tasks.ContextInsensitive

let browser = pipeline {
    plug acceptHtml
    plug putSecureBrowserHeaders
    plug fetchSession
    set_header "x-pipeline-type" "Browser"
}

let defaultView = scope {
    get "/" (htmlView Index.layout)
    get "/index.html" (redirectTo false "/")
    get "/default.html" (redirectTo false "/")
}

let browserRouter = scope {
    not_found_handler (htmlView NotFound.layout) //Use the default 404 webpage
    pipe_through browser //Use the default browser pipeline

    forward "" defaultView //Use the default view
}

//Other scopes may use different pipelines and error handlers

let api = pipeline {
    plug acceptJson
    set_header "x-pipeline-type" "Api"
}

let notFound next (ctx: Microsoft.AspNetCore.Http.HttpContext) =
    task {
        let message = sprintf "%s could not be found" ctx.Request.Path.Value
        return! json message next ctx
    }

let apiRouter = scope {
    not_found_handler (notFound >=> setStatusCode 404)
    pipe_through api

    forward "/albums" Albums.Controller.resource
}

let router = scope {
    forward "/api" apiRouter
    forward "" browserRouter
}