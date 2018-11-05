using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class DataKickController : Controller
    {
        public IActionResult Index()
        {
            //The WebClient is a built-in DotNet object for making HTTP Requests.
            System.Net.WebClient webClient = new System.Net.WebClient();
            //I typically use "headers" to configure my request to the API.  In this case, I'm saying that I want to the response
            //as JSON instead of HTML. I might also use a header to specify my API Key.  Every API is going to work a little bit\
            //differently here, so read the documentation!
            webClient.Headers.Add("accept", "application/json");
            string jikanJson = webClient.DownloadString("https://api.jikan.moe/v3/manga/1/characters");
            //Almost all of our APIs use JSON formatting.  DotNet has a crappy built-in JSON parser, so most apps use the 
            //NewtonSoft.Json NuGet package instead.  See the "DadJoke" class below.

            JikanResponse jikanResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<JikanResponse>(jikanJson);
            return View("index", jikanResponse.characters);
        }
    }

    public class JikanResponse
    {
        public string request_hash { get; set; }
        public bool request_cached { get; set; }

        public int request_cache_expiry { get; set; }

        public JikanCharacters[] characters { get; set; }
    }

    public class JikanCharacters
    {
        public string url { get; set; }
        public string name { get; set; }
        public string image_url { get; set; }
        public string role { get; set; }
    }
}