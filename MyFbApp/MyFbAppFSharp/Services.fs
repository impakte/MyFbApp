namespace MyFbAppFSharp

open FSharp.Data
open System.Net.Http
open databaseManager
open Newtonsoft.Json
open ConfigurationLoader
open HttpModels
open FacebookProfile

module Services = 

    let mutable accessToken = ""
    let facebookAppId = "545621829700082"
    let facebookAppSecret = "22c72da3ffc9d3becdfec8f024216364"
    let client = new HttpClient()

    let CheckTokenValidity = async{
        let! checkJson = client.GetStringAsync("https://graph.facebook.com/me?access_token=" + getLongLiveToken) |> Async.AwaitTask
        return checkJson.Contains("name")
        } 

    let getLongLiveToken token = async{
        let! tokenJson = client.GetStringAsync("https://graph.facebook.com/oauth/access_token?client_id=" +  (*config.facebookAppId*) facebookAppId +
                                                "&client_secret=" + (*config.facebookAppSecret*)facebookAppSecret + 
                                                "&grant_type=fb_exchange_token&fb_exchange_token=" + token) |> Async.AwaitTask
        let newToken = JsonConvert.DeserializeObject<Token>(tokenJson)
        accessToken <- newToken.access_token
        return newToken
    }

    let GetFacebookProfileAsync = async {
            let! userJson = client.GetStringAsync("https://graph.facebook.com/me/?fields=id,name,picture,cover,age_range,birthday,email,first_name,last_name&access_token=" + accessToken) |> Async.AwaitTask
            let facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(userJson)
            return facebookProfile
        }
