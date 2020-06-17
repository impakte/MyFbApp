namespace MyFbAppFSharp

open Newtonsoft.Json
open System.IO
open GalaSoft.MvvmLight.Ioc
open System
open System.Reflection

module ConfigurationLoader = 
    
    type Config() =
        member val facebookAuthUrl = "" with get, set
        member val facebookRedirectUrl = "" with get, set
        member val facebookAppId = "" with get, set
        member val facebookAppSecret = "" with get, set
        member val scope = "" with get, set
        member val FbProfileUrl = "" with get, set
        member val FbUserPostUrl = "" with get, set
        member val FbUserPostCommentsUrl = "" with get, set

    let LoadConfig (embeddedResourceStream: Stream) =
        let streamReader = new StreamReader(embeddedResourceStream)
        let jsonString = streamReader.ReadToEnd()
        let configuration = JsonConvert.DeserializeObject<Config>(jsonString)
        configuration
