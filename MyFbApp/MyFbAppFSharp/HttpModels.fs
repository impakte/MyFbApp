namespace MyFbAppFSharp

open System

module HttpModels = 
    type CursorComment = 
        { Before: string
          After : string}

    type PagingDataComment =
        { Cursor: CursorComment}

    type Publisher =
        { Name: string
          Id: string }

    type CommentData = 
        { Created_time: DateTime
          User: Publisher
          Message: string  
          Id: string }

    type FacebookPostComments =
        { Data: List<CommentData>
          Paging: PagingDataComment }

    type Token = 
        { access_token: string
          token_type: string
          expires_in: int
        }