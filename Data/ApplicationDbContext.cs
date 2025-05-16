using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection.PortableExecutable;
using VaccinationTracker.Models;

namespace VaccinationTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<VaccinationSchedule> VaccinationSchedules { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var hasher = new PasswordHasher<IdentityUser>();
            var adminUser = new IdentityUser
            {
                Id = "1", // Static ID for the admin user
                UserName = "Admin@VT.com",
                NormalizedUserName = "ADMIN@VT.COM",
                Email = "Admin@VT.com",
                NormalizedEmail = "ADMIN@VT.COM",
                EmailConfirmed = true,
                SecurityStamp = string.Empty
            };

            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Test123$");

            modelBuilder.Entity<IdentityUser>().HasData(adminUser);

            var adminRole = new IdentityRole
            {
                Id = "1", // Static ID for the admin role
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            modelBuilder.Entity<IdentityRole>().HasData(adminRole);

            var adminUserRole = new IdentityUserRole<string>
            {
                UserId = "1", // Static ID for the admin user
                RoleId = "1"  // Static ID for the admin role
            };

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(adminUserRole);

            modelBuilder.Entity<VaccinationSchedule>()
                .HasOne(vs => vs.User) // Define relationship with User
                .WithMany()
                .HasForeignKey(vs => vs.UserId);

            modelBuilder.Entity<VaccinationSchedule>()
                .HasOne(vs => vs.Vaccine) // Define relationship with Vaccine
                .WithMany()
                .HasForeignKey(vs => vs.VaccineId);
        }
    }
}
