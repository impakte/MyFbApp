namespace MyFbAppFSharp

open System

module DatabaseModels = 
    type FacebookPostsDb =
        { Id: int
          TimeStamp: DateTime
          Message: string
          Created_time: DateTime
          PostsId: string
          CommentsNumber: int
          UserId: string }

    type FacebookProfileDb = 
        { Id: int
          TimeStamp: DateTime
          Name: string
          UserId: string }

    type TokenDb =
        { Id: int
          LongLiveToken:string }

