type Result<'TSuccess,'TFailure> = 
    | Success of 'TSuccess
    | Failure of 'TFailure

// There's a type in F# like this already called Choice
// https://msdn.microsoft.com/visualfsharpdocs/conceptual/core.choice%5b%27t1%2c%27t2%5d-union-%5bfsharp%5d