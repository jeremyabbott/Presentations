- title : Logary - Logging for .NET
- description : Lighting Talk on Logary
- author : Jeremy Abbott
- theme : moon
- transition : default

***

Logary - Logging for .NET

![FsReveal](images/fsharp256.png)

### Presented by Jeremy Abbott

***

### Logary
- Logary is a high performance, multi-target logging, metric, tracing and health-check library for mono and .NET.
- Non-blocking logging
- Written in F#
- Now w/ C# Extensions!

***

### Targets
- Logary can log to multiple targets
- Support for logging to
  - TextWriter (file)
  - Console
  - Debugger Console (VS, Xamarin Studio, Mono Develop)
  - Databases via ADO.NET
  - More

***

### Adapters
- Plugins into other libraries
- Those libraries can then send their logs to logary
- Adapters for
  - Log4Net
  - Suave
  - TopShelf
  - EventStore

***


### Demo

1. Basic logging to a file
2. Using the Suave Adapter -> Sends Suave's log info to Logary

***

### Questions?

Questions?

***

### Resources

- [Logary](http://logary.github.io/)
- [F# for Fun and Profit](http://fsharpforfunandprofit.com/)
- [F# Foundation](http://fsharp.org/) 
  - Become a member!
- [F# on Slack](http://fsharp.slack.com)
- [#FSharp on Twitter](https://twitter.com/search?f=tweets&vertical=default&q=%23fsharp&src=typd)
- [The Book of F# by Dave Fancher](https://www.nostarch.com/fsharp)
- [Slides created with FSReveal](https://github.com/fsprojects/FsReveal)