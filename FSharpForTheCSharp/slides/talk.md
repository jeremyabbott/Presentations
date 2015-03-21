- title : Getting Functional w/ F#
- description : F# for the C# Dev
- author : Jeremy Abbott
- theme : moon
- transition : default

***

## F# for the C# Dev

![FsReveal](images/fsharp256.png)

### Presented by Jeremy Abbott

***

### Hi

- I'm Jeremy
  - Not the figure skater
- Developer at Praeses
- 9 years of exp. between programming and PMing
- Twitter: @mrjabbott
- Email: jeremymabbott@gmail.com
- Blog: jeremyabbott.github.io

***

### Why Do We Care About Functional Programming?
- I got interested in F# because it seems to be everywhere
- F# is gaining a lot of traction in the .NET community
- C# is getting more functional
- JavaScript is everywhere, and works best when treated as a functional language
- Being comfortable with functional concepts is pragmatic

---

### What is it?
- FP treats computation as the evaluation of mathematical functions and avoids changing-state and mutable data
  - Thanks Wikipedia
- Functional like f(x) = x + 2
- A given input will always have the same output
- f(x) is immutable - its definition is not going to change

***

### What's F#'s Story?
#### (Alternate Title: Not Another Language)

- F# is a cross-platform, functional-first, multi-paradigm programming language
- Built on .NET and interoperable - works with C# (... and VB)
- Created by Don Syme, Microsoft Researcher, and one of the contributors to .NET generics

---

### F#: 10,000 ft View

- Constructs are immutable by default
- Everything is an expression and evaluates to something (even if that something is nothing)
- Multi-paradigm language with a focus on functional principles
- But supports OO principles too
- Statically typed
- Syntax derives from OCaml

--- 

### Why F#?
#### From the F# Marketing Dept.

- Default immutability limits unwanted side effects
- Type inference leads to more predictable code
- Terse syntax leads to cleaner, more modular code

---

### F# in the Wild

- Canopy - Wrapper over selenium written in F# - F#rictionless web testing
- FAKE - F# Make - A DSL for build tasks
- Xamarin - Write cross-platform apps in F#!

***

### Immutable / Mutable

- Immutable - Unchangeable over time or unable to be changed
- Mutable - The opposite of immutable
- Did you know C# strings are immutable?
  - When you concatenate two strings you’re actually getting a new string.
  - In C# readonly and const keywords provide immutable behavior

***

### Hello World

    [<EntryPoint>]
    let main argv = 
	    printfn "%s" "Hello world!"

        0 // return an integer exit code

***

### Let's Talk Functional

- f(x) = x + 2
- This is a function that adds 2 to the input (x)
- x is the "domain"
- The range is the value of x (whatever that is) + 2
- this function maps x to x + 2
- Functions accept “exactly one input” (more on this later)

---

### F# Example
    
    let add2 x = x + 2

- val add2 : int -> int
- val add2 : domiain -> range
- The function named add2 maps int to int
- No matter what happens elsewhere in the world (or application), this function will always return the same output for a given input.

---

### Values, Let and Binding

    let add2 x = x + 2

- In the function add2
- add2 and x are just names for values
  - They're immutable
- Values are not variables
- add2 is the name for the function that maps an integer onto an integer
- x is the name of the input to add2
- In a purely mathematical context, adding 2 to an input does not change that input.

---

### Functions are Values

- You can pass functions as arguments to functions because they're "just" values
- Functions that accept functions as arguments are called higher order functions

***

### Instant Feedback with FSI

- No more math, just code!
- FSharp Interactive
- In Visual Studio
- Or from Fsi.exe

***

### Immutable by Default

    let x = 2 // value x : int = 2
    x = 3 // val it : bool = false
    x = 2 // // val it : bool = true

- F# evaluated x = 3 as an expression (everything in F# is an expression)
- The result of the expression x = 3 is false (because x is 2)

***

### Type Inference and Static Typing

    let saySomethingShort x y = sprintf "%s %s" x y

- Types are inferred based on usage
- F# is evaluated from top down
- Binding order matters
- File order matters
- White-space matters
- This allows the compiler to make assumptions about our code, like what types are being passed to our functions

---

### Let’s Break it Down

    let saySomethingShort x y = sprintf "%s %s" x y

- Types are static, but do not have to be stated explicitly
- inputs x and y need to be strings, but we didn't have to state it
- Inputs are curried (I'll explain)
- Functions can be partially applied (I'll explain)

---

### Type Annoations

    let saySomethingShort' (x:string) (y:string) =
        sprintf "%s %s" x y

- The previous example could have been written with the argument types stated explicitly
- Most of the time this isn't necessary, which means we can work with typed data without having to write everything out

---

### Currying

    let saySomethingShort x y = sprintf "%s %s" x y

- Functions accept “exactly one input”
- But that’s totally two parameters (x and y)
- The compiler reads “x:string -> y:string -> string” which can be said as “x goes to y goes to string”
  - x maps on to y which maps onto a string
- The F# compiler rewrites functions with multiple inputs into individual functions that accept one parameter
- This is called currying, and is named after Haskell Curry

---

### Currying Example
	
    let saySomethingShort x y =
        sprintf "%s %s" x y

    let saySomethingShort'' x =
        (fun y -> sprintf "%s %s" x y)

---

### Partial Application

- Only supply some of the arguments to a function
- Compose new, specialized “higher order” functions out of generic underlying functions

---

### Partial Application (Cont.)

    let saySomethingShort x y = sprintf "%s %s" x y
    let sayHelloTo = saySomethingShort "Hello"
    printfn "%s" (sayHelloTo "World") // Hello World

- The value sayHelloTo is a function that accepts a string and returns a string
- The value saySomethingShort is a function that accepts two strings
- We partially applied it by only passing some of the arguments, resulting in a new function that expects the rest of the arguments

--- 

### Piping
    
    let sum =
        [|1..10|] 
        |> Array.filter (fun s -> s % 2 = 0) 
        |> Array.sum
    // val sum : int = 30

- Array.filter accepts a filtering function and an array
- Array.sum accepts an array
- Piping lets us pass in one expression as the last argument to a function

***

### F# Types
- F# is Part of the .NET Framework
- Supports all of the .NET core data types
- byte
- int
- float
- bool
- string
- ...

---

### F# Types - Unit

- Everything in F# is an expression
- Expressions ultimately evaluate to a value
- Sometimes we just want a side effect and not an actual value
- The unit type - () - represents a value that we are not interested in.
- It is the absence of a specific value

---

### F# Types - Unit (Cont.)
- Define a function that doesn’t accept any arguments
- (except that every function takes exactly one input)
- let example () = some expression
- example () // call example and pass in the expected argument- in this case unit

---

### F# Types (Cont.)

    let sayHelloWorld () = printfn "Hello World"

- sayHelloWorld is a function that has a domain of unit and a range of unit (unit -> unit)

---

### Tuples

    let myTuple =
        ("1", 2, 3.0f) // string * int * float

- The most basic functional type
- “functional types” refers to types common in functional programming
- Group values within a single immutable construct
- In spite of the comma delimited syntax, tuples are not collections

---

### Record Types

    type Person =
        { FirstName : string; LastName : string }
    let someone =
        {   FirstName = "Jeremy";
            LastName = "Abbott" }

- The F# version of classes
- Immutable construct for group named values (labels)
- Individual values can be made mutable
- Commonly used in place of classes

---

### Record Types and Immutability

    let someone =
        { FirstName = "Jeremy"; LastName = "Abbott" }

    // copy everything and update first name
    let updateFirstName person firstName =
        { person with FirstName = firstName }

    let updatedPerson = updateFirstName someone "John"
  
- someone is immutable
- We can copy the values of someone, into a new record and then update the properties we care about

--- 

### Discriminated Unions
- Discrete cases that are related to each other
- Enumerations evolved
- Compiles down to an enumeration if the value of each union case is an integer
- Used for:
  - simple object hierarchies
  - representing tree structures
  - Replacing type abbreviations

---

### Discriminated Unions

type Shape =
| Circle of Radius : float
| Triangle of Base : float * Height : float
| Rectangle of Length : float * Height : float
    member x.getArea () = 
        match x with // pattern matching
        | Circle (r) -> (r ** 2.0) * System.Math.PI 
        | Triangle (b, h) -> 0.5 * (b * h)
        | Rectangle (l, h) -> l * h

---

### Measurements
- F# provides support for units of measurement
- You can provide definitions for converting units of measurement
- You can define your own units of measurement
- "We'll never use this in our business apps..."

---

### Measurements

Demo

---

### Functional Types

Demo

***

### Control Flow
#### F# supports control flow through
- while loops
- while (bool expression) do something
- for loops
- for i = 0 to 100 do something
- enumerable for loop
- for i in [0..10] do something

_All require body to evaluate to unit_

---

### Branching

let someValue = 
    if boolExpression then “yes”
    elif boolExpression then “maybe"
    else “no”

_Pattern matching is preferred_

***

### Pattern Matching

- One of F#’s most powerful features
- Match expressions are F#’s primary branching mechanism
- Match expressions are like C# switch statements that evaluate to a value
- Compiler will issue a warning if expressions are missing from pattern

---

### Pattern Matching
#### Super Basic Example

    let someValue number =
    	match number with
    	| n when n > 0 -> "postive"
    	| n when n < 0 -> "negative"
    	| _ -> "zero"

printfn "%s" (someValue -1) // negative

--- 

### Patterning Matching

Demo

***

### F# Collections

    // Array
    let fruits = [| "apple"; "banana"; "orange" |]

    // Lists
    let fruits' = [ "apple"; "banana"; "orange" ]

    // Sequence
    let fruits'' = seq { for f in fruits -> f }

---

### F# Lists

- F# lists are have a head/tail structure
- The head/tail structure supports recursion, which is the preferred mechansism for working with enumerable data in functional programming
- The head of the list is the first element in the list
- The tail is everything else
- The typical pattern is look at the head, evaluate an expression against it (and can keep it or dump it depending on the needs), and then continues processing the tail

---

### Tail Recursion

Demo

***

### Asynchronous Workflows

- F#’s answer to parallelism and async
- F# has special bindings (let!, do!) along with the async keyword for implementing parallel and async workflows.
- async { expression }

---

### Asynchronous Workflows (cont.)

- Create Async objects that represent work that needs to happen.
- Use a triggering function to start the work.
- let! - starts an async computation immediately, where let creates an Async object to start later (also applies to do! return!). 

---

### Asynchronous Workflows

Demo

***

### Something Completely Different

- Develop a cross-platform app using Xamarin
- Let's see the code!

### Questions?

Questions?

***

### Thank You

Thank you!

***
