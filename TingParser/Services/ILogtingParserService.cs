using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel;

namespace TingParser.Services
{
    public interface ILogtingParserService
    {
        public VoteResultViewModel ParseVote(string content);

        int ParseGetPaginationCountFromAdvancedSearch(string content);

        IList<string> ParseGetPaginationUrlFromAdvancedSearch(string content);

        IList<(CaseType, string)> ParseGetCaseUrlsFromAdvancedSearch(string content);

        CaseType ParseCaseType(string content);

        IList<string> ParseCaseNormalUrls(string content);

        IList<string> ParseOverviewForRowLinks(string content);
    }
}
