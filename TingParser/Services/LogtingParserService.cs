using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using ViewModel;

namespace TingParser.Services
{
    public class LogtingParserService : ILogtingParserService
    {
        private readonly Uri baseUrl = new Uri("https://logting.fo/");

        public VoteResultViewModel ParseVote(string content)
        {
            var removedHTML = StripHTML(content);

            string[] lines = removedHTML.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            );

            var finalLines = new List<string>();

            foreach (var line in lines)
            {
                string trimmedLine = line.TrimStart().TrimEnd();

                if (string.IsNullOrEmpty(trimmedLine))
                {
                    continue;
                }

                finalLines.Add(trimmedLine);
            }

            var voteResult = new VoteResultViewModel();

            string[] split = finalLines[0].Split(new String[] { "Mál:", "Viðgerð:" }, StringSplitOptions.RemoveEmptyEntries);

            voteResult.Term = int.Parse(Regex.Match(split[0], @"\d+").Value).ToString();
            voteResult.Topic = int.Parse(Regex.Match(split[1], @"\d+").Value).ToString();
            voteResult.Reading = int.Parse(Regex.Match(split[2], @"\d+").Value).ToString();


            DateTime.TryParseExact(finalLines[1], "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime resultDate);
            voteResult.VoteDate = resultDate;

            voteResult.Present = int.Parse(Regex.Match(finalLines.FirstOrDefault(x => x.Contains("Present")), @"\d+").Value);
            voteResult.YesCount = int.Parse(Regex.Match(finalLines.FirstOrDefault(x => x.Contains("Total JA")), @"\d+").Value);
            voteResult.NoCount = int.Parse(Regex.Match(finalLines.FirstOrDefault(x => x.Contains("Total NEI")), @"\d+").Value);
            voteResult.BlankCount = int.Parse(Regex.Match(finalLines.FirstOrDefault(x => x.Contains("Total BLANK")), @"\d+").Value);
            voteResult.AbsentCount = 33 - voteResult.Present;

            voteResult.YesVoters = new List<string>();
            voteResult.NoVoters = new List<string>();
            voteResult.BlankVoters = new List<string>();
            voteResult.AbsentVoters = new List<string>();

            int counter = finalLines.FindIndex(x => x.Contains("JA:")) + 1;

            for (int i = counter; i < (counter + voteResult.YesCount); i++)
            {
                voteResult.YesVoters.Add(finalLines[i].Substring(finalLines[i].IndexOf(" ") + 1));
            }

            counter = finalLines.FindIndex(x => x.Contains("NEI:")) + 1;

            for (int i = counter; i < (counter + voteResult.NoCount); i++)
            {
                voteResult.NoVoters.Add(finalLines[i].Substring(finalLines[i].IndexOf(" ") + 1));
            }

            counter = finalLines.FindIndex(x => x.Contains("BLANK:")) + 1;

            for (int i = counter; i < (counter + voteResult.BlankCount); i++)
            {
                voteResult.BlankVoters.Add(finalLines[i].Substring(finalLines[i].IndexOf(" ") + 1));
            }

            return voteResult;
        }

        public IList<string> ParseOverviewForRowLinks(string content)
        {
            var decodedContent = System.Web.HttpUtility.HtmlDecode(content);

            var doc = new HtmlDocument();
            doc.LoadHtml(content);

            var element = doc.GetElementbyId("row");
            var tbody = element.SelectSingleNode("//tbody");

            var nodes = tbody.Elements("tr");

            var list = new List<string>();

            foreach (var item in nodes)
            {
                var link = item.Element("td").Element("a").GetAttributeValue("href", "");
                var uri = new Uri(baseUrl, link);
                list.Add(uri.AbsoluteUri);
            }

            return list;
        }

        private string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }
    }
}
