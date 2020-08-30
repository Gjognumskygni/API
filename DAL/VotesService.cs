using DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;
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
