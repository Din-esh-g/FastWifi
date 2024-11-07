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
        public DbSet<Comments> Comments { get; set; }
        public DbSet<SuscribeList> SuscribeLists { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // If needed, configure relationships and primary keys
            modelBuilder.Entity<ServiceDetail>()
                .HasKey(sd => sd.ID);  // Primary key for ServiceDetail

            modelBuilder.Entity<InterventionSummary>()
                .HasKey(invs => invs.ID);  // Primary key for InterventionSummary
            modelBuilder.Entity<ProgressNote>()
                .HasKey(pn => pn.Id);  // Primary key for ProgressNote
            modelBuilder.Entity<Job>()
                .HasKey(j => j.Id);  // Primary key for Job
            modelBuilder.Entity<JobApplication>()
                .HasKey(ja => ja.Id);  // Primary key for JobApplication
            modelBuilder.Entity<Contact>()
                .HasKey(c => c.Id);  // Primary key for Contact
            modelBuilder.Entity<Comments>()
                .HasKey(c => c.ID);  // Primary key for Comments
            modelBuilder.Entity<SuscribeList>()
                .HasKey(sl => sl.Id);  // Primary key for SuscribeList


        }
    }
}
