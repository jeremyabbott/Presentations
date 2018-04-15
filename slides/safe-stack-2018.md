- title : SAFE Stack 2018
- description : An exploration of full stack F# with hot reloading on both the server and the client.
- author : Jeremy Abbott
- theme : black
- transition : default

***

# SAFE Stack 2018

## Powerful Full Stack Productivity

### With F# on the Client and Server

![FsReveal](images/safe-logo.png)

### Presented by Jeremy Abbott

***

### Hi

- I'm Jeremy 🖖🏼
- Twitter: [@mrjabbott](http://twitter.com/mrjabbott)
- Email: jeremymabbott@gmail.com
- Blog: [jeremyabbott.github.io](http://jeremyabbott.github.io)

***

### The Slides

- Slides
  - On Azure: http://jeremypresents.azurewebsites.net/safe-stack-2018.html
  - On GitHub: https://github.com/jeremyabbott/Presentations
- Created using [FsReveal](https://github.com/fsprojects/FsReveal)

---

### Resources

[Safe-Stack Docs](https://safe-stack.github.io/docs/)

***

## The SAFE Stack

![FsReveal](images/fsharpHasEverything.jpg)

---

### Safe Apps: Functional-First Full Stack Devolopment

- F# all the way down ❤️
- S: Server-side F# with Suave, Giraffe, or Saturn
- A: Cloud-ready on Azure (or your favorite cloud provider)
- F: Client-side F# via Fable
- E: Model-view-update architecture via Elmish
- Shared code between server and client 👏🏼

---

### Why

- Type Safety!
- Rapid Feature Development
- Practical Code Reuse
- Moar F#
  - Pattern matching
  - Algebraic data types
  - Default immutability
- Less JavaScript (but also more)
  - What is `this`? 🤷🏼‍♀️
- Commercial support available
  - [λ Factory](http://lambdafactory.io/)
  - [Compositional IT](https://www.compositional-it.com/)

***

### Server-Side F#

- [Suave](https://suave.io/)
- [Giraffe](https://github.com/giraffe-fsharp/Giraffe)
- [Saturn](https://saturnframework.github.io/docs/)

---

### Hello World

#### Via Suave

    let simpleApp = (Successful.OK "Hello F# Conf")

    let betterApp =
        choose [
            GET >=> path "/hello" >=> simpleApp
            POST >=> path "/goodbye" >=> (Successful.OK "Goodbye")
        ]
    startWebServer defaultConfig betterApp

---

### Via Saturn

    let helloFsharpConf = text "👋🏼 F# Conf 2018"

    let apiRouter = scope {
        get "/hello" helloFsharpConf
    }

    let app = application {
        router apiRouter
        url "http://0.0.0.0:8085/"
        use_gzip
    }


---

### Saturn CLI

    dotnet new -i Saturn.Template
    dotnet new saturn -lang F# -o OutputDir -n AppName
    cd OutputDir/src/AppName
    dotnet saturn gen.json Pokemon Pokemon id:string number:string name:string
    dotnet saturn migration

***

### Fable

- F# |> Babel

---

### JavaScript 😭

<img src="images/typeScript.png" style="float: left; width: 45%; margin-right: 1%; margin-bottom: 0.5em;">
<img src="images/typeScript2.png" style="float: left; width: 45%; margin-right: 1%; margin-bottom: 0.5em;">
<p style="clear: both;">
<!--![typescript1](images/typeScript.png)
![typescript2](images/typeScript2.png)-->

---

### Not JavaScript ❤️
![addFable](images/fableAdd.png)
![addFable2](images/fableAdd2.png)

- Real static typing with type inference!
- The F# compiler tells you something is wrong

---

### How it Works

- F# -> Fable -> ES6 -> Babel -> ES5
- Webpack converts F# to ES6 using the Fable compiler
- Webpack converts ES6 to ES5
- Fable integrates with the existing JavaScript ecosystem
- Fable lets you write F# and emit JavaScript you can be proud of!

***

### Elmish

- Leverage the "model view update" architecture pioneered by Elm
- Models define application state
- Messages declared as cases in a discriminated union

---

### Basic Elmish



***

### SAFE Prerequisites

- .NET Core SDK 2.x
- Node 8.x
- Yarn (or use NPM)
- Mono (for macOS/Linux)
- IDE (Just use VS Code w/ Ionide)

---

### SAFE Template

    dotnet new -i Safe.Template
    dotnet new SAFE -lang F# -n BestFrameworkEver -o BestFrameworkEver
    cd BestFrameworkEver
    ./build.sh

---

### Template Options

Template options content

***

### Let's Make a Form

Form demo


***

### OSS Shout Out

1. Ionide
1. Suave
1. Fable
1. Paket
1. Fake
1. Expecto
1. Canopy

---

### Deployment

- It's really easy to deploy this stack using docker
- Docker Hub/Azure
- Docker Cloud/Digital Ocean w/ Linux

***

### Paket

- Paket is an alternative (and better) package manager for .NET
- Allows you to reference Nuget, Git repos, and HTTP sources
- Paket keeps track of exact versions of the pacakges you install
    - It also gives you visibility into your transitive dependencies

***

### FAKE

- F# Make: A DSL for build tasks
- Write your build scripts in F#

***

### Questions

Any questions?

***

### Summary

- Full stack F# to make you more productive
- F# on the server with Suave running on .NET Core
- F# on the client with Fable, leveraging the power of the JavaScript ecosystem
- Paket for .NET dependency management
- FAKE for writing maintainable build scripts

***

### Resources

- [F# Foundation](http://fsharp.org/)
- [F# Applied](http://products.tamizhvendan.in/fsharp-applied/)
- [The Book of F#](https://www.nostarch.com/fsharp)
- [F# for Fun and Profit](https://fsharpforfunandprofit.com/)