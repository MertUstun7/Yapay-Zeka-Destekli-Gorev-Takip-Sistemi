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
           .WithOne() // Tek yönlü ilişki: User sınıfında CompanyOwner için özel navigation yok
           .HasForeignKey<Company>(c => c.OwnerId)
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskAssignment>()
        .HasOne(a => a.AssignedBy)
        .WithMany(u => u.GivenTasks) // User sınıfındaki ICollection<TaskAssignment>
        .HasForeignKey(a => a.AssignedById)
        .OnDelete(DeleteBehavior.Restrict); // Silerken hata almasın

            modelBuilder.Entity<TaskAssignment>()
                .HasOne(a => a.AssignedTo)
                .WithMany(u => u.AssignedTasks) // User sınıfındaki ICollection<TaskAssignment>
                .HasForeignKey(a => a.AssignedToId)
                .OnDelete(DeleteBehavior.Restrict); // Ayn

            modelBuilder.Entity<TaskReport>()
    .HasOne(tr => tr.CreatedBy)
    .WithMany(u => u.TaskReports)
    .HasForeignKey(tr => tr.CreatedById)
    .OnDelete(DeleteBehavior.Restrict);

             // veya burada da Restrict deneyebilirsiniz


        }
    }

}