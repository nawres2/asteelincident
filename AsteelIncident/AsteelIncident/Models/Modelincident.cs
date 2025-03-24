using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AsteelIncident.Models
{
    public partial class Modelincident : DbContext
    {
        public Modelincident()
            : base("name=Modelincident")
        {
        }

        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Equipment> Equipments { get; set; }
        public virtual DbSet<IncidentComment> IncidentComments { get; set; }
        public virtual DbSet<IncidentLog> IncidentLogs { get; set; }
        public virtual DbSet<Incident> Incidents { get; set; }
        public virtual DbSet<IncidentType> IncidentTypes { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserActivity> UserActivities { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Incident>()
                .HasMany(e => e.Attachments)
                .WithRequired(e => e.Incident)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Incident>()
                .HasMany(e => e.IncidentComments)
                .WithRequired(e => e.Incident)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Incident>()
                .HasMany(e => e.IncidentLogs)
                .WithRequired(e => e.Incident)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Incident>()
                .HasMany(e => e.Reports)
                .WithRequired(e => e.Incident)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IncidentType>()
                .HasMany(e => e.Incidents)
                .WithRequired(e => e.IncidentType)
                .HasForeignKey(e => e.IncidentTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Attachments)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UploadedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Equipments)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.AssignedTo);

            modelBuilder.Entity<User>()
                .HasMany(e => e.IncidentComments)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.CommentedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.IncidentLogs)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.ActionBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Incidents)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.AssignedTo);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Incidents1)
                .WithRequired(e => e.User1)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Notifications)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Reports)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.ResolvedBy);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserActivities)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
