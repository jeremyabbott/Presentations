- title : Being Productive with the F# Stack of Happiness
- description : An exploration of full stack F# with hot reloading on both the server and the client.
- author : Jeremy Abbott
- theme : moon
- transition : default

***

Being Productive with the F# Stack of Happiness 
<br />
Featuring Suave, Fable, FAKES, and Paket

![FsReveal](images/fsharp256.png)

### Presented by Jeremy Abbott

***

### Hi

- I'm Jeremy 🖖🏼
- Twitter: [@mrjabbott](http://twitter.com/mrjabbott)
- Email: jeremymabbott@gmail.com
- Blog: [jeremyabbott.github.io](http://jeremyabbott.github.io)

<iframe src="https://giphy.com/embed/3o7aTpj3LZxNF7OuJy" width="480" height="270" frameBorder="0" class="giphy-embed" allowFullScreen></iframe><p><a href="https://giphy.com/gifs/filmeditor-movie-mean-girls-3o7aTpj3LZxNF7OuJy">Too Gay to λ</a></p>

---

### About Me

- .NET developer for 10 years
- Functional programming hobbyist
- Trying to spread the word of F# in my small way

***

### The Slides

- Slides
  - On Azure: http://curryinginahurry.azurewebsites.net/fsharp-stack-of-happiness.html
  - On GitHub: https://github.com/jeremyabbott/Presentations
- Created using [FsReveal](https://github.com/fsprojects/FsReveal)
- Code
  - Suave: https://suave.io
  - Fable: http://fable.io
  - Fable-Suave-Scaffold: https://github.com/fable-compiler/fable-suave-scaffold
  
***

### The F# Stack of Happiness

![FsReveal](images/fsharpHasEverything.jpg)

---
### The F# Stack of Happiness

- Full F# stack with hot reloading on both the client and the server 🥞
- Makes use of Suave for the back-end on .NET Core 🎩
- Makes use of Fable for front-end clients web or mobile 🐉
- FAKE for builds 🛠
- Paket for .NET dependencies 📦
- Shared code between server and client 👏🏼
- F# everywhere ❤️

---

### Why?

- Moar F#
    - Pattern matching
    - Algebraic Data Types
    - Default immutability
- Less JavaScript (but also more)
    - What is `this`? 🤷🏼‍♀️
- Shared code between client and server
- Moar productivity! 🚀

***

### Suave

- Simple web development library written in F#
- Encourages you to think of your application as functions

---

### Hello World
    let simpleApp = (Successful.OK "Hello Portland")

    let betterApp =
        choose [
            GET >=> path "/hello" >=> simpleApp
            POST >=> path "/goodbye" >=> (Successful.OK "Goodbye")
        ]
    startWebServer defaultConfig betterApp

---
### WebParts
    type WebPart = HttpContext -> Async<HttpContext option>

- Suave is built around the idea of WebParts
- By composing a pipeline of functions you can build a response for a given request
- Accept a context and eventually return either another context (request/response) or None
 
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
- Depends on Webpack to work as of 1.0 Beta
- Fable integrates with the existing JavaScript ecosystem
- Fable lets you write F# and emit JavaScript you can be proud of!

---

### Fable Compatibility

Read the [docs](http://fable.io/docs/compatibility.html) yo

***

### Fable-Suave-Scaffold

- Full Stack F#
- Edit, Save, Recompile Workflow Throughout
- Leverages the Elmish architecture on the client with React
- All you need is dotnet core and VS Code. No heavy tooling.
- the Fable-Suave-Scaffold was extracted from production code running today
    - Shout out to Steffen Formkman
        - Paket, Fable-Suave-Scaffold, amazing F#er

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