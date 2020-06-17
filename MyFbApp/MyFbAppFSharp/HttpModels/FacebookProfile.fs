namespace MyFbAppFSharp

open System

module FacebookProfile =
    
    type Data = 
        { IsSilhouette: bool
          Url: string }

    type Picture =
        {
            Data: Data }
    type Cover = 
        { Id: string
          OffsetY: int
          Source: string }

    type AgeRange =
        { Min: int}
    
    type Device =
        { Os: string }

    type FacebookProfile = 
        { Name: string
          Picture : Picture
          Locale: string
          Link: string
          Cover: Cover
          AgeRange: AgeRange
          Devices: Device[]
          FirstName: string
          LastName: string
          Gender: string
          IsVerified: bool
          Id: string }

