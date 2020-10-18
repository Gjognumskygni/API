namespace Gjognumskygni.Domain
{
    public class Vote
    {
        public int Id { get; set; }

        public int MemberOfParliamentId { get; set; }

        public MemberOfParliament MemberOfParliament { get; set; }

        public VoteType VoteType { get; set; }
    }
}