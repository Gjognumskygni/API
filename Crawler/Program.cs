using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TingParser.Services;
using System.Linq;
using System.Collections.Generic;

namespace Crawler
{
    class Program
    {
        static async Task Main(string[] args)
        {
            {
                HttpClient client = new HttpClient();

                var serviceProvider = new ServiceCollection()
                    .AddSingleton<ILogtingParserService, LogtingParserService>()
                    .BuildServiceProvider();

                //configure console logging
                var parserService = serviceProvider.GetService<ILogtingParserService>();

                Console.WriteLine("Crawling Logting.fo from 0 to infinity - and beyond!");

                var years = GetYears();

                foreach (var year in years.Select(x => x.ToString()))
                {
                    var url = $"https://logting.fo/search/advancedSearch.gebs?d-16544-p=2&year={year}&subject=&parliamentMember=&menuChanged=%23parameters.menuChanged&lawNo=&committee.id=&caseType=-1";

                    var responseMessage = await client.GetAsync(url);

                    if (!responseMessage.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"StatusCode: {responseMessage.StatusCode}. Found no overview page for {year}!");
                        continue;
                    }

                    Console.WriteLine($"StatusCode: {responseMessage.StatusCode}. Overview page for {year}!");
                }

                return;

                // Check progress
                var id = 1;
                while (true)
                {
                    await Task.Delay(5000);

                    var url2 = $"https://logting.fo/casenormal/viewState.gebs?caseState.id={id++}&menuChanged=16";

                    var responseMessage = await client.GetAsync(url2);

                    if (!responseMessage.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Found no page at {url2}!");
                        continue;
                    }

                    Console.WriteLine($"Found a page at {url2}!");

                    var body = await responseMessage.Content.ReadAsStringAsync();

                    var pv = parserService.ParseVote(body);
                }
            }
        }

        public static IList<int> GetYears()
        {
            var years = new List<int>();
            years.Add(13);
            years.Add(199);
            for (int i = 1990; i <= 2019; i++) years.Add(i);
            return years;
        }
    }
}