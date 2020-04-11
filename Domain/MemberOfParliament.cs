using System;

namespace Domain
{
    public class MemberOfParliament
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public Person Person { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public MemberRole MemberOfParliamentRole { get; set; }
    }
}
