namespace MyFbAppFSharp

open databaseManager
open Services

module userProfileLogic =

    let FacebookUserProfileAsync = GetFacebookProfileAsync |> Async.RunSynchronously
    
    let FacebookProfileDb = getFacebookProfileDb

    (*let CheckUserProfileData = 
        CheckUser FacebookUserProfileAsync.Id && CheckTimeStamp FacebookUserProfileAsync.Id
     
     let CheckUserPostData =
        CheckPostUser FacebookUserProfileAsync.Id && CheckTimeStamp FacebookUserProfileAsync.Id*)

    (*let GetProfileData = 
        if CheckUserProfileData then 
            UpdateProfileApi
            FacebookProfileDb
        else
            UpdateProfileApi
            FacebookProfileDb*)
