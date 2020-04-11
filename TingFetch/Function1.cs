using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Text;
using TingParser.Services;

namespace TingFetch
{
    public static class FetchVotes
    {
        [FunctionName("FetchVotes")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            HttpClient client = new HttpClient();

            string name = req.Query["votePage"];

            var response = await client.GetAsync($"{name}");
            var pageContents = await response.Content.ReadAsStringAsync();
            
            log.LogInformation("Fetched Content");

            var decodedContent = System.Web.HttpUtility.HtmlDecode(pageContents);

            var service = new LogtingParserService();
            var voteResult = service.ParseHtmlContent(decodedContent);

            log.LogInformation("Parsed Votes");

            string json = JsonConvert.SerializeObject(voteResult);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
        }
    }
}
