namespace Domain
{
    public class Proposer
    {
        public int Id { get; set; }

        public int MemberOfParliamentId { get; set; }

        public MemberOfParliament MemberOfParliament { get; set; }
    }
}