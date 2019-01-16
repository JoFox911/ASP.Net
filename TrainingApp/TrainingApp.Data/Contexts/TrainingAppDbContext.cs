using System;
using Microsoft.EntityFrameworkCore;
using TrainingApp.Data.DTO.Account;
using TrainingApp.Data.Models.Account;

namespace TrainingApp.Data.Contexts
{
    public class TrainingAppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbQuery<UserDetailDTO> UserDetailDTO { get; set; }
        public DbQuery<UserListDTO> UserListDTO { get; set; }
        public DbSet<Role> Roles { get; set; }

        public TrainingAppDbContext(DbContextOptions<TrainingAppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string superadminRoleName = "superadmin";
            string adminRoleName = "admin";
            string userRoleName = "user";

            // добавляем роли
            Role superadminRole = new Role { Id = Guid.NewGuid(), Name = superadminRoleName };
            Role adminRole = new Role { Id = Guid.NewGuid(), Name = adminRoleName };
            Role userRole = new Role { Id = Guid.NewGuid(), Name = userRoleName };

            User superadminUser = new User { Id = Guid.NewGuid(), Email = "superadmin@gmail.com", Password = "1", RoleId = superadminRole.Id };
            User adminUser = new User { Id = Guid.NewGuid(), Email = "admin@gmail.com", Password = "1", RoleId = adminRole.Id };
            User userUser = new User { Id = Guid.NewGuid(), Email = "user@gmail.com", Password = "1", RoleId = userRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { superadminRole, adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { superadminUser, adminUser, userUser });
            base.OnModelCreating(modelBuilder);
        }
    }
}
