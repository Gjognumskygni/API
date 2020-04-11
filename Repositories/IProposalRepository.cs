using Domain;
using System;
using System.Collections.Generic;

namespace DAL
{
    public interface IProposalRepository
    {
        public Proposal GetProposal(int proposalId);
        
        public ICollection<MemberOfParliament> GetProposers(int proposalId);

        public ICollection<Vote> GetVotes(int proposalId);

        public ICollection<Vote> GetYesVotes(int proposalId);

        public ICollection<Vote> GetNoVotes(int proposalId);

        public ICollection<Vote> GetBlankVotes(int proposalId);

        public ICollection<Vote> GetAbsentVotes(int proposalId);
    }
}
