using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<MemberOfParliament> MemberOfParliaments { get; set; }

        public DbSet<Party> Parties { get; set; }

        public DbSet<Term> Terms { get; set; }

        public DbSet<Person> Persons { get; set; }
        
        public DbSet<Proposal> Proposals { get; set; }

        public DbSet<Proposer> Proposers { get; set; }

        public DbSet<Vote> Votes { get; set; }
    }
}
