using System.Collections.Generic;

namespace Gjognumskygni.Domain
{
    public class Proposal
    {
        public int Id { get; set; }

        public ICollection<Proposer> Proposers { get; }

        public ICollection<Vote> Votes { get; }
    }
}
