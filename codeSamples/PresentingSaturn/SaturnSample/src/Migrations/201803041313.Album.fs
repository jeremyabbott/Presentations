namespace Migrations
open SimpleMigrations

[<Migration(201803041313L, "Create Albums")>]
type CreateAlbums() =
  inherit Migration()
  override __.Up() =
    base.Execute(@"CREATE TABLE Albums(
      Id INT NOT NULL,
      Key TEXT NOT NULL,
      Name TEXT NOT NULL,
      Price DECIMAL NOT NULL
    )")
  override __.Down() =
    base.Execute(@"DROP TABLE Albums")