namespace MyFbAppFSharp

open databaseManager
open Services

module loginLogic = 
    
    let CheckToken =
        if databaseManager.CheckToken then CheckTokenValidity |> Async.RunSynchronously
            else false

    let test =
        true

    let SetTokenAsync token =
        let LongLiveToken = getLongLiveToken token |> Async.RunSynchronously
        UpdateLongLiveToken LongLiveToken.access_token
        
        