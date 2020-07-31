using System;
using System.Net.Http;
using TingParser.Services;
using System.Linq;
using System.Collections.Generic;
using TingParser;
using System.Threading.Tasks;

namespace Crawler
{
    public class CrawlerService : ICrawlerService
    {
        private readonly HttpClient client;

        private readonly ILogtingParserService logtingParserService;

        public CrawlerService(ILogtingParserService logtingParserService)
        {
            this.client = new HttpClient();
            this.logtingParserService = logtingParserService;
        }

        public async Task Crawl()
        {
            Console.WriteLine("Initiating Crawling logting.fo");

            //var years = GetYears();
            var years = new List<int>() { 2019 };

            foreach (var year in years)
            {
                var pageCount = await GetTotalPageCountForYear(year);
                var casesList = await GetListOfCasesForYear(year, pageCount);
                var usedCases = 
            }

            return;
        }

        private async Task<int> GetTotalPageCountForYear(int year)
        {
            var responseMessage = await client.GetAsync($"https://logting.fo/search/advancedSearch.gebs?d-16544-p=1&year={year}&subject=&parliamentMember=&menuChanged=%23parameters.menuChanged&lawNo=&committee.id=&caseType=-1");

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException(responseMessage.ReasonPhrase);
            }

            var content = await responseMessage.Content.ReadAsStringAsync();

            var totalPageCount = logtingParserService.ParseGetPaginationCountFromAdvancedSearch(content);

            Console.WriteLine($"StatusCode: {responseMessage.StatusCode}. Overview page for {year} contains {totalPageCount} pages!");

            return totalPageCount;
        }

        private async Task<IEnumerable<(CaseType, string)>> GetListOfCasesForYear(int year, int totalPageCount)
        {
            IEnumerable<(CaseType, string)> list = new List<(CaseType, string)>();

            for (int page = 0; page < totalPageCount; page++)
            {
                try
                {
                    var pageList = await GetCasesFromYear(year, page + 1);
                    list = list.Concat(pageList);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed to parse year {year}, page {page}! {e.Message} \n {e.StackTrace}");
                }

                Console.WriteLine($"Page {page} processed!");
            }

            Console.WriteLine($"Total cases for year {year} is {list.Count()}!");

            return list;
        }

        public IList<int> GetYears()
        {
            var years = new List<int>();
            years.Add(13);
            years.Add(199);
            for (int i = 1990; i <= 2019; i++) years.Add(i);
            return years;
        }

        public async Task<IEnumerable<(CaseType, string)>> GetCasesFromYear(int year, int page)
        {
            var responseMessage = await client.GetAsync($"https://logting.fo/search/advancedSearch.gebs?d-16544-p={page}&year={year}&subject=&parliamentMember=&menuChanged=%23parameters.menuChanged&lawNo=&committee.id=&caseType=-1");

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"{responseMessage.StatusCode} {responseMessage.RequestMessage}");
            }

            var content = await responseMessage.Content.ReadAsStringAsync();

            return logtingParserService.ParseGetCaseUrlsFromAdvancedSearch(content);
        }

        public IDictionary<CaseType, ICollection<string>> ToDictionary(IEnumerable<(CaseType, string)> list)
        {
            IDictionary<CaseType, ICollection<string>> dictionary = new Dictionary<CaseType, ICollection<string>>();

            var Logaruppskot = list.Where(x => x.Item1 == CaseType.Logaruppskot).Select(x => x.Item2).ToList();
            var Rikistilmali = list.Where(x => x.Item1 == CaseType.Rikistilmali).Select(x => x.Item2).ToList();
            var Uppskot_Til_Samtyktar = list.Where(x => x.Item1 == CaseType.Uppskot_Til_Samtyktar).Select(x => x.Item2).ToList();
            var Figgjarlogaruppskot = list.Where(x => x.Item1 == CaseType.Figgjarlogaruppskot).Select(x => x.Item2).ToList();
            var Skrivligar_Fyrispurningar = list.Where(x => x.Item1 == CaseType.Skrivligar_Fyrispurningar).Select(x => x.Item2).ToList();
            var Spurningar52 = list.Where(x => x.Item1 == CaseType.Spurningar52).Select(x => x.Item2).ToList();
            var Muntligar_Fyrispurningar = list.Where(x => x.Item1 == CaseType.Muntligar_Fyrispurningar).Select(x => x.Item2).ToList();
            var Fragreiðing = list.Where(x => x.Item1 == CaseType.Fragreiðing).Select(x => x.Item2).ToList();
            var Nevndarmál = list.Where(x => x.Item1 == CaseType.Nevndarmál).Select(x => x.Item2).ToList();

            dictionary.Add(CaseType.Logaruppskot, Logaruppskot);
            dictionary.Add(CaseType.Rikistilmali, Rikistilmali);
            dictionary.Add(CaseType.Uppskot_Til_Samtyktar, Uppskot_Til_Samtyktar);
            dictionary.Add(CaseType.Figgjarlogaruppskot, Figgjarlogaruppskot);
            dictionary.Add(CaseType.Skrivligar_Fyrispurningar, Skrivligar_Fyrispurningar);
            dictionary.Add(CaseType.Spurningar52, Spurningar52);
            dictionary.Add(CaseType.Muntligar_Fyrispurningar, Muntligar_Fyrispurningar);
            dictionary.Add(CaseType.Fragreiðing, Fragreiðing);
            dictionary.Add(CaseType.Nevndarmál, Nevndarmál);

            return dictionary;
        }
    }
}