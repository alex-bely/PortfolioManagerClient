using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortfolioManagerClient.Controllers
{
    public class StockController : ApiController
    {
        [Route("api/Stock/{name}")]
        public Data Get(string name)
        {
            string json;

            try
            {
                using (var web = new WebClient())
                {

                    var url = String.Format("https://www.google.com/finance/info?q=NASDAQ:{0}", name);
                    json = web.DownloadString(url);
                }
            }
            catch(WebException ex)
            {
                return new Data { Ticker = name, Price = 1 };
            }
            

            //Google adds a comment before the json for some unknown reason, so we need to remove it
            json = json.Replace("//", "");

            var v = JArray.Parse(json);


            var ticker = v[0].SelectToken("t").ToString();
            var price = (decimal)v[0].SelectToken("l");

            return new Data(){ Ticker = ticker, Price = price };
            //Console.WriteLine($"{ticker} : {price}");

        }

        public class Data
        {
            public string Ticker { get; set; }
            public decimal Price { get; set; }
        }

    }
}
