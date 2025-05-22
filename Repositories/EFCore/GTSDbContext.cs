using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Entities
{
    public class GTSDbContext : IdentityDbContext<User>
    {
        public GTSDbContext(DbContextOptions<GTSDbContext> options)
            : base(options)
        {
        }

        //public DbSet<User> Users { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // Users Configuration
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Managers Configuration
            modelBuilder.Entity<Manager>()
                .HasOne(m => m.User)
                .WithOne(u => u.Manager)
                .HasForeignKey<Manager>(m => m.ManagerId)
                .OnDelete(DeleteBehavior.Cascade); 

            // Workers Configuration
            modelBuilder.Entity<Worker>()
                .HasOne(w => w.Users)
                .WithOne(u => u.Worker)
                .HasForeignKey<Worker>(w => w.WorkerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Worker>()
                .HasOne(w => w.Manager)
                .WithMany(m => m.Worker)
                .HasForeignKey(w => w.ManagerId)
                .OnDelete(DeleteBehavior.NoAction); 

            // Tasks Configuration
            modelBuilder.Entity<Assignment>()
                .HasOne(t => t.AssignedWorker)
                .WithMany(w => w.Tasks)
                .HasForeignKey(t => t.AssignedTo)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<Assignment>()
                .HasOne(t => t.AssignedManager)
                .WithMany(m => m.AssignedTask)
                .HasForeignKey(t => t.AssignedBy)
                .OnDelete(DeleteBehavior.NoAction); 

            // Reports Configuration
            modelBuilder.Entity<Report>()
                .HasOne(r => r.Uploader)
                .WithMany(u => u.Reports)
                .HasForeignKey(r => r.UploadedBy)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Task)
                .WithMany(t => t.Reports)
                .HasForeignKey(r => r.TaskId)
                .OnDelete(DeleteBehavior.SetNull); 

            // Index Configurations
            modelBuilder.Entity<Worker>()
                .HasIndex(w => w.ManagerId);

            modelBuilder.Entity<Assignment>()
                .HasIndex(t => t.AssignedTo);

            modelBuilder.Entity<Report>()
                .HasIndex(r => r.UploadedBy);
        }
    }

}