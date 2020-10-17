using DAL.Model;
using ViewModel;

namespace DAL
{
    public class VotesService : IVotesService
    {
        private readonly ApplicationDbContext _context;

        public VotesService(ApplicationDbContext context)
        {

        }

        public void AddVoteResult(VoteResultViewModel voteResult)
        {

        }
    }
}
