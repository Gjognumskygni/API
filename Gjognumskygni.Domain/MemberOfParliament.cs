using System;

namespace Gjognumskygni.Domain
{
    public class MemberOfParliament
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int PartyId { get; set; }

        public int TermId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public MemberOfParliamentRole MemberOfParliamentRole { get; set; }

        public Person Person { get; set; }

        public Party Party { get; set; }

        public Term Term { get; set; }
    }
}
