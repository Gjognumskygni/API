using Domain;
using System.Threading.Tasks;
using ViewModel;

namespace TingParser.Services
{
    public interface ILogtingParserService
    {
        public VoteResultViewModel ParseVote(string content);
    }
}
