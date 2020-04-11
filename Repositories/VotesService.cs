using DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel;

namespace DAL
{
    public class VotesService : IVotesService
    {
        private readonly TransparencyContext _context;

        public VotesService(TransparencyContext context)
        {

        }

        public void AddVoteResult(VoteResultViewModel voteResult)
        {
            
        }
    }
}
