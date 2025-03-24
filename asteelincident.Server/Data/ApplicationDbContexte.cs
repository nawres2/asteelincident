using System.Collections.Generic;
using System.Collections.Generic;
using asteelincident.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace asteelincident.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }


        public DbSet<Department> Departments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<IncidentType> IncidentTypes { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<IncidentComment> IncidentComments { get; set; }
        public DbSet<IncidentLog> IncidentLogs { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Report> Reports { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Incident>()
                .HasOne(i => i.CreatedByUser)
                .WithMany() // Assuming the User class does not need a collection of incidents
                .HasForeignKey(i => i.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull); // or another delete behavior

            modelBuilder.Entity<Incident>()
                .HasOne(i => i.AssignedToUser)
                .WithMany()
                .HasForeignKey(i => i.AssignedTo)
                .OnDelete(DeleteBehavior.SetNull); // or another delete behavior
        
        
      
        }
    }
}
