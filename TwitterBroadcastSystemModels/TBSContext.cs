using System.Data.Entity;
using TwitterBroadcastSystemModel.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TwitterBroadcastSystemModel
{
    public class TBSContext : DbContext
    {
        public TBSContext() : base("TBSContext")
        {
            Database.SetInitializer(new TBSContextInitializer());
        }

        public TBSContext(string connection = null) : base(connection ?? "TBSContext")
        {
            Database.SetInitializer(new TBSContextInitializer());
        }

        public DbSet<Action> Action { get; set; }
        public DbSet<Destination> Destination { get; set; }
        public DbSet<DestinationType> DestinationType { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<PriorityLevel> PriorityLevel { get; set; }
        public DbSet<Procedure> Procedure { get; set; }
        public DbSet<ProcedureCategory> ProcedureCategory { get; set; }
        public DbSet<Query> Query { get; set; }
        public DbSet<QueryTargetUser> QueryTargetUser { get; set; }
        public DbSet<RateLimit> RateLimit { get; set; }
        public DbSet<SearchTerm> SearchTerm { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
