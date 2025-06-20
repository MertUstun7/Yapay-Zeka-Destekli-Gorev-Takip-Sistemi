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

        
        public DbSet<Company> Companies { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<TaskAssignment> TaskAssignments { get; set; }
        public DbSet<TaskReport> TaskReports { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Company>()
            .HasOne(c => c.Owner)
            .WithOne() 
            .HasForeignKey<Company>(c => c.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskAssignment>()
            .HasOne(a => a.AssignedBy)
            .WithMany(u => u.GivenTasks) 
            .HasForeignKey(a => a.AssignedById)
            .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<TaskAssignment>()
            .HasOne(a => a.AssignedTo)
            .WithMany(u => u.AssignedTasks) 
            .HasForeignKey(a => a.AssignedToId)
            .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<TaskReport>()
            .HasOne(tr => tr.CreatedBy)
            .WithMany(u => u.TaskReports)
            .HasForeignKey(tr => tr.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
            .HasOne(u => u.Company)
            .WithMany(c => c.Users)
            .HasForeignKey(u => u.CompanyId)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.Company)
            .WithMany(c => c.Tasks)
            .HasForeignKey(t => t.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.CreatedBy)
            .WithMany(u => u.CreatedTasks)
            .HasForeignKey(t => t.CreatedById)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskAssignment>()
            .HasOne(a => a.TaskItem)
            .WithMany(t => t.Assignments)
            .HasForeignKey(a => a.TaskItemId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskReport>()
            .HasOne(tr => tr.TaskItem)
            .WithMany() 
            .HasForeignKey(tr => tr.TaskItemId)
            .OnDelete(DeleteBehavior.Cascade);


        }
    }

}