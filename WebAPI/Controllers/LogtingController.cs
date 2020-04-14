using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TingParser.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogtingController : ControllerBase
    {
        private readonly ILogtingParserService logtingParserService;
        private readonly IHttpClientFactory clientFactory;

        public LogtingController(IHttpClientFactory clientFactory, ILogtingParserService logtingParserService)
        {
            this.clientFactory = clientFactory;
            this.logtingParserService = logtingParserService;
        }

        [HttpGet("votes")]
        public async Task<IActionResult> GetVotesAsync([FromQuery(Name = "url")] string url)
        {
            HttpClient httpClient = clientFactory.CreateClient();
            var response = await httpClient.GetAsync(new Uri(url));

            if (response.IsSuccessStatusCode)
            {
                var httpContent = await response.Content.ReadAsStringAsync();
                var decodedContent = System.Web.HttpUtility.HtmlDecode(httpContent);
                var voteResult = logtingParserService.ParseVote(decodedContent);
                return Ok(voteResult);
            }

            return NoContent();
        }

        [HttpGet("index")]
        public async Task<IActionResult> GetIndexAsync([FromQuery(Name = "url")] string url)
        {
            HttpClient httpClient = clientFactory.CreateClient();
            var response = await httpClient.GetAsync(new Uri(url));

            if (response.IsSuccessStatusCode)
            {
                var httpContent = await response.Content.ReadAsStringAsync();
                var decodedContent = System.Web.HttpUtility.HtmlDecode(httpContent);

                var doc = new HtmlDocument();
                doc.LoadHtml(decodedContent);

                var element = doc.GetElementbyId("row");

                return Ok();
            }

            return NoContent();
        }
    }
}