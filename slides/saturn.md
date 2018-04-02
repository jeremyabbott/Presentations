- title : Saturn: F# |> MVC
- description : An overview of Saturn, a framework for writing ASP.NET Core Web Apps with F#
- author : Jeremy Abbott
- theme : moon
- transition : default

***

## Saturn: F# |> MVC

![FsReveal](images/fsharp256.png)

## Presented by Jeremy Abbott

***

### Hi

- I'm Jeremy 🖖🏼
- Twitter: [@mrjabbott](http://twitter.com/mrjabbott)
- Email: jeremymabbott@gmail.com
- Blog: [jeremyabbott.github.io](http://jeremyabbott.github.io)

---

<iframe src="https://giphy.com/embed/3o7aTpj3LZxNF7OuJy" width="480" height="270" frameBorder="0" class="giphy-embed"></iframe><p>Too Gay to λ</p>

---

### About Me

- Senior Software Engineer @ Incomm Digital Solutions
- F# and Functional Programming Fan

***

### The Slides

- Slides
  - On Azure: http://jeremypresents.azurewebsites.net/Saturn.html
  - On GitHub: https://github.com/jeremyabbott/Presentations
- Created using [FsReveal](https://github.com/fsprojects/FsReveal)

---

### Saturn

<iframe src="https://giphy.com/embed/mFqPsiBhLZ0wo" width="480" height="384" frameBorder="0" class="giphy-embed"></iframe>

Not this Saturn

---

### This is Saturn

- An opinionated functional-first micro-web-framework
- Even thought it's not a planet, it *does* have "rings":
  - Kestrel and ASP.NET Core
  - Giraffe
- Created by [Krzysztof Cieslak](http://kcieslak.io)
  - Also created:
    1. Ionide 👏🏼
    1. Forge 👏🏼
    1. VSCode Elm Extension 👏🏼

---

### Also Moons

- Dapper for performant SQL data access
- Simple.Migrater for data migration support

---

### Why?

- Reduces the barrier to entry for folks new to F#
- Abstracts away raw HTTP manipulation until you need it.
- The MVC pattern is familiar to those coming from other web frameworks
- High team productivity thanks to the Saturn CLI

***

### Before Saturn...

- You're an F#er that wants to write your next web app at work with F#...

---

### Can I Haz Functional?

You show this to your teammates who haven't used a lot of F#:

    let simpleApp = (Successful.OK "Hello F#ers")

    let betterApp =
        choose [
            GET >=> path "/hello" >=> simpleApp
            POST >=> path "/goodbye" >=> (Successful.OK "Goodbye")
        ]
    startWebServer defaultConfig betterApp

---

### What's That >=>

<iframe src="https://giphy.com/embed/12mPcp41D9a1i0" width="480" height="275" frameBorder="0" class="giphy-embed" allowFullScreen></iframe>

---

### Suave/Giraffe Are AMAZING

But Saturn can ease people into the abstractions used by these frameworks.

---

### With Saturn

    let get (ctx: HttpContext, name : string) =
        task {
            return! Controller.json ctx ("Hello " + name)
        }

    let resource = controller {
        show get // routes to GET /<resource>/name
    }

---

### Recognition

- "It's just an async function that returns JSON"
- "It looks a little like JavaScript, but with real types"

***

### Pipelines

- Saturn makes brilliant use of computation expressions
- The `pipeline` CE is used to combine Giraffe HTTP Handlers in a declarative manner

---

### Pipelines (cont.)

```fsharp
let api = pipeline {
    plug acceptJson
    set_header "x-pipeline-type" "Api"
}
// api is an HttpHandler
// acceptJson is also an HttpHandler
// set_header returns an HttpHandler
// plug takes any HttpHandler
```

***

### Scopes

CE use to combine `pipeline`s, `controller`s, and other `HttpHandlers`

```fsharp
let apiRouter = scope {
    not_found_handler notFound
    pipe_through api // add a pipeline
    forward "/albums" Albums.Controller.resource
}
```

***

### Scopes (cont.)

```fsharp
let indexHandler (name : string) =
    sprintf "Hello %s" name
    |> json

let anotherScope = scope {
    getf "/hello/%s" indexHandler
}
```

***

### Controllers

CE used to create convention based routing/handlers

```fsharp
// w/o controller
route "/person" >=>
    choose [
        POST >=>
            fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let! person = ctx.BindModelAsync<Person>()
                return! json person next ctx
            }
        PUT >=>
            fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let! person = ctx.BindModelAsync<Person>()
                return! json person next ctx
            }
    ]
```

---

### Controllers (cont.)

```fsharp
let post (ctx : HttpContext) =
    task {
        let! person = ctx.BindModelAsync<Person>()
        return! Controller.json ctx "Created!"
    }
let put (ctx : HttpContext) =
    task {
        let! person = ctx.BindModelAsync<Person>()
        return! Controller.json ctx "Updated!"
    }

let pc = controller {
    create post
    update put
}
```

***

### The Application CE

- Builder that returns an IWebHost
- Declarative syntax that abstracts
  - IWebHostBuilder
  - IServiceCollection
  - IApplicationBuilder

---

### The Album Application

```fsharp
let app = application {
    pipe_through endpointPipe

    router Router.router
    url "http://0.0.0.0:8085/"
    memory_cache
}
```

***

### Saturn CLI

This goes in paket.references:

```text
dotnet-saturn
```

And then

```sh
# dotnet saturn gen.json <singular> <plural> <fields>
dotnet saturn gen.json Album Albums Id:int Key:guid Name:string Price:decimal
```

---

### Saturn CLI gen Output

- Model with properties based on passed in fields
- Repository with CRUD functions (via dapper)
- Controller with CRUD actions
- Server-side views (if `gen/gen.html` called)
- Migration for new Albums database table

---

### Saturn CLI Migrations

```fsharp
[<Migration(201803041313L, "Create Albums")>]
type CreateAlbums() =
  inherit Migration()
  override __.Up() =
    base.Execute(@"CREATE TABLE Albums(
      Id INT NOT NULL,
      Key TEXT NOT NULL,
      Name TEXT NOT NULL,
      Price DECIMAL NOT NULL
    )") // sqlite data types
  override __.Down() =
    base.Execute(@"DROP TABLE Albums")
```

---

### Run the Migrations

```sh
# update the database to the latest migration
dotnet saturn migration
```

***

### What's Next?

Install the template:

```bash
dotnet new -i Saturn.Template
dotnet new saturn -lang F#
```

***

### Any Questions?

***

### Resources

- [Reinventing MVC pattern for web programming with F#](http://kcieslak.io/Reinventing-MVC-for-web-programming-with-F)
- [Saturn Docs](https://saturnframework.github.io/docs/)
- [Saturn Source](https://github.com/SaturnFramework/Saturn)
- [Giraffe Docs](https://github.com/giraffe-fsharp/Giraffe)
- [F# Foundation](http://fsharp.org/)