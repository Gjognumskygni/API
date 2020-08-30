using System;
using System.Collections.Generic;
using System.Text;
using ViewModel;

namespace DAL
{
    public interface IVotesService
    {
        public void AddVoteResult(VoteResultViewModel voteResult);
    }
}
