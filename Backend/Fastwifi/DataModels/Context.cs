using Fastwifi.Models;
using Microsoft.EntityFrameworkCore;

namespace Fastwifi.DataModels
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options): base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<ProgressNote> ProgressNotes { get; set; }
        public DbSet<InterventionSummary> InterventionSummaries { get; set; }
        public DbSet<ServiceDetail> ServiceDetails { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<Contact> Contacts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // If needed, configure relationships and primary keys
            modelBuilder.Entity<ServiceDetail>()
                .HasKey(sd => sd.ID);  // Primary key for ServiceDetail

            modelBuilder.Entity<InterventionSummary>()
                .HasKey(invs => invs.ID);  // Primary key for InterventionSummary

        }
    }
}
