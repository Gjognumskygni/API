using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TingParser.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FetchVotesController : ControllerBase
    {
        private readonly ILogtingParserService logtingParserService;
        private readonly IHttpClientFactory clientFactory;

        public FetchVotesController(IHttpClientFactory clientFactory, ILogtingParserService logtingParserService)
        {
            this.clientFactory = clientFactory;
            this.logtingParserService = logtingParserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery(Name = "votePage")] string url)
        {
            HttpClient httpClient = clientFactory.CreateClient();
            var response = await httpClient.GetAsync(new Uri(url));

            if (response.IsSuccessStatusCode)
            {
                var httpContent = await response.Content.ReadAsStringAsync();
                var decodedContent = System.Web.HttpUtility.HtmlDecode(httpContent);
                var voteResult = logtingParserService.ParseHtmlContent(decodedContent);
                return Ok(voteResult);
            }

            return NoContent();
        }
    }
}