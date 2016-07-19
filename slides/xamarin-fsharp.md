- title : What's New for F# in Xamarin?
- description : Explore the Additional F# Support in Xamarin
- author : Jeremy Abbott
- theme : moon
- transition : default

***
##What's New for F# in Xamarin?

![FsReveal](images/fsharp256.png)
![FsReveal](images/pikachu.png)

### Presented by Jeremy Abbott

***

### Hi

- I'm Jeremy
- Twitter: [@mrjabbott](http://twitter.com/mrjabbott)
- Email: jeremymabbott@gmail.com
- Blog: [jeremyabbott.github.io](http://jeremyabbott.github.io)
- Enterprise Architect at Praeses in Shreveport Louisiana

***

### Not The Figure Skater

![FsReveal](images/JeremyAbbottFigureSkater.jpg)

***

### FsReveal

- [Slides created with FsReveal](https://github.com/fsprojects/FsReveal)
- Can use markdown or F# script files!

***

### Why F# and Xamarin?

- The same reasons you would use F# anywhere else
- Type Inference
- Concise Code
- Correct Code
- Type Providers

***

### What are We Talking About?
1. What's new in Xamarin for F#
2. F# and Xamarin Forms
3. iOS Auto Layout and F#

***

### So. What is new in Xamarin for F# Users?
- Xamarin now supports F# 4.0
- Multiplatform Templates
  - Shared Projects
  - PCLs
- Syntax highlighting and auto-complete in FSI
- Beta support for FAKE
- Xamarin.Forms support for F#

***

### New Project Templates
Demo

***

### FAKE
- Domain specific language for building projects
- F# Make
- Need to have a bash build script and F# build script in your root directory
  - build.sh
  - build.fsx

***

### F# Enhancements in Xamarin Studio
- Global Symbol Search
- Syntax Highlighting Improvements
  - Mutable what?
- FAKE Integration (Preview)

***

### Xamarin.Forms
Out of the Box support

***

### F# and XAML
- You can use XAML to markup your pages without a type provider
- Checkout [this video](http://www.wintellect.com/devcenter/jwood/using-xaml-f-xamarin-forms-screencast) to see it in action. 

***

### iOS Auto Layout Constraints
- iOS Auto Layout is used to dynamically size UI elements
- Can be setup in storyboards or programmatically
- Constraints are defined using linear equations:
  - y = mx+b
  - view1.attribute = m * view2.attribute + constant

***

### The Auto Layout DSL
- Makes use of F# type extensions, custom operators, and record types
- Allows you to create type auto layout constraints semantically

***

### The Auto Layout DSL

Demo

### Questions?

Questions?

***

### Resources

- [Slides created with FsReveal](https://github.com/fsprojects/FsReveal)
- [Samples](https://github.com/jeremyabbott/FSharpXamarin)