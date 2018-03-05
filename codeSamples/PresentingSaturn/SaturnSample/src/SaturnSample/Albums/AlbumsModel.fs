namespace Albums

[<CLIMutable>]
type Album = {
  Id: int
  Key: System.Guid
  Name: string
  Price: decimal
}

module Validation =
  let validate v =
    let validators = [
      fun u -> if isNull u.Name then Some ("Name", "Name shouldn't be empty") else None
    ]

    validators
    |> List.fold (fun acc e ->
      match e v with
      | Some (k,v) -> Map.add k v acc
      | None -> acc
    ) Map.empty
