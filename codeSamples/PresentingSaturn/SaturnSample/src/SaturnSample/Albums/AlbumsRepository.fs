namespace Albums

open Database
open Microsoft.Data.Sqlite
open System.Threading.Tasks

module Database =
  [<CLIMutable>]
  type StupidSqliteAlbum = {
    Id: int
    Key: byte array
    Name: string
    Price: decimal
  }
  let getAll connectionString : Task<Result<StupidSqliteAlbum seq, exn>> =
    use connection = new SqliteConnection(connectionString)
    query connection "SELECT Id, Key, Name, Price FROM Albums" None

  let getById connectionString id : Task<Result<StupidSqliteAlbum option, exn>> =
    use connection = new SqliteConnection(connectionString)
    querySingle connection "SELECT Id, Key, Name, Price FROM Albums WHERE Id=@Id" (Some <| dict ["id" => id])

  let update connectionString v : Task<Result<int,exn>> =
    use connection = new SqliteConnection(connectionString)
    execute connection "UPDATE Albums SET Id = @Id, Key = @Key, Name = @Name, Price = @Price WHERE Id=@Id" v

  let insert connectionString v : Task<Result<int,exn>> =
    use connection = new SqliteConnection(connectionString)
    execute connection "INSERT INTO Albums(Id, Key, Name, Price) VALUES (@Id, @Key, @Name, @Price)" v

  let delete connectionString id : Task<Result<int,exn>> =
    use connection = new SqliteConnection(connectionString)
    execute connection "DELETE FROM Albums WHERE Id=@Id" (dict ["id" => id])

  let toAlbum (s: StupidSqliteAlbum) : Album =
    {
      Id = s.Id
      Key = System.Guid(s.Key)
      Price = s.Price
      Name = s.Name
    }
