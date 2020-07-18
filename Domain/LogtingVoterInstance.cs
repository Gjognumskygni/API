using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class LogtingVoterInstance
    {
        public int Id { get; set; }
        public DateTime ProposalDate { get; set; }
        public string Name { get; set; }
        public DateTime TermDate { get; set; }
        public VoteType VoteType { get; set; }
        public string Url { get; set; }
        public string Proposal { get; set; }
        public string Reading { get; set; }
    }
}
