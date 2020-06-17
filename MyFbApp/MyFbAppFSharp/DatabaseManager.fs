namespace MyFbAppFSharp

open SQLite
open System
open System.IO
open DatabaseModels
open System.Linq.Expressions

module databaseManager =

    type FacebookPostsDbObject() =
        [<PrimaryKey; AutoIncrement>]
        member val Id = 0 with get, set
        member val TimeStamp = DateTime.MinValue with get, set
        member val Message = "" with get, set
        member val Created_time = DateTime.MinValue with get, set
        member val PostsId = "" with get, set
        member val CommentsNumber = 0 with get, set
        member val UserId = "" with get, set

    let convertFbPostDbToObject (item: FacebookPostsDb) =
        let obj = FacebookPostsDbObject()
        obj.Id <- item.Id
        obj.TimeStamp <- item.TimeStamp
        obj.Message <- item.Message
        obj.Created_time <- item.Created_time
        obj.PostsId <- item.PostsId
        obj.CommentsNumber <- item.CommentsNumber
        obj.UserId <- item.UserId
        obj

    let convertFbPostDbToModel (obj: FacebookPostsDbObject) : FacebookPostsDb =
        { Id = obj.Id
          TimeStamp = obj.TimeStamp
          Message = obj.Message
          Created_time = obj.Created_time
          PostsId = obj.PostsId
          CommentsNumber = obj.CommentsNumber
          UserId = obj.UserId }

    type FacebookProfileDbObject() =
        [<PrimaryKey; AutoIncrement>]
        member val Id = 0 with get, set
        member val TimeStamp = DateTime.MinValue with get, set
        member val Name = "" with get, set
        member val UserId = "" with get, set

    let convertFbProfileDbToObject (item: FacebookProfileDb) =
        let obj = FacebookProfileDbObject()
        obj.Id <- item.Id
        obj.TimeStamp <- item.TimeStamp
        obj.Name <- item.Name
        obj.UserId <- item.UserId
        obj

    let convertFbProfileDbToModel (obj: FacebookProfileDbObject) : FacebookProfileDb =
        { Id = obj.Id
          TimeStamp = obj.TimeStamp
          Name = obj.Name
          UserId = obj.UserId }

    type TokenDbObject() =
        [<PrimaryKey; AutoIncrement>]
        member val Id = 0 with get, set
        member val LongLiveToken = "" with get, set

    let convertTokenDbToObject (item: TokenDb) =
        let obj = TokenDbObject()
        obj.Id <- item.Id
        obj.LongLiveToken <- item.LongLiveToken
        obj

    let convertTokenDbToModel (obj: TokenDbObject) : TokenDb =
        { Id = obj.Id
          LongLiveToken = obj.LongLiveToken }

    let dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "MyDB.db3")

    let connect =
        let db = new SQLiteConnection(dbPath)
        db

    let database = connect

    let CreateTables =
        do database.CreateTable<FacebookPostsDbObject>() |> ignore
           database.CreateTable<FacebookProfileDbObject>() |> ignore
           database.CreateTable<TokenDbObject>() |> ignore

    let CheckToken = 
        let count = database.Table<TokenDbObject>().Count()
        count > 0

    let getLongLiveToken = 
        database.Table<TokenDbObject>().First().LongLiveToken

    let UpdateLongLiveToken token = 
        do database.DeleteAll<TokenDbObject>() |> ignore
        let newToken = TokenDbObject(Id= 0, LongLiveToken= token)
        (*let obj = convertTokenDbToObject token*)
        do database.Insert(newToken) |> ignore


    let UpdateProfile profile =
        let obj = convertFbProfileDbToObject profile
        do database.Execute("DELETE FROM FacebookProfileDb WHERE UserId = ?", obj.Id) |> ignore
           database.Insert(obj) |> ignore

    let UpdatePosts posts = 
        do for post in posts do
              let obj = convertFbPostDbToObject post
              do database.Update(obj) |> ignore

    let getFacebookProfileDb = 
        database.Table<FacebookProfileDbObject>().First()

    let getFacebookPostsDb = 
        database.Table<FacebookPostsDbObject>().ToList()

    (*let CheckTimeStamp (userId: string) =
        let result = DateTime.Compare(database.Table<FacebookProfileDbObject>().Where(w => w.UserId == userId).First().TimeStamp, DateTime.Now.AddDays(-5.0)) > 0
        result

    let CheckUser (userId: string) = 
        let result = database.Table<FacebookProfileDbObject>().Where(w => w.UserId == userId).Count() > 0
        result

    let CheckPostUser (userId: string) =
        let result = database.Table<FacebookPostsDbObject>().Where(userId).Count() > 0
        query {
            for UserId
        }
        let result = data
        result*)

    let DeleteToken =
        database.DeleteAll<TokenDbObject>() |> ignore
        