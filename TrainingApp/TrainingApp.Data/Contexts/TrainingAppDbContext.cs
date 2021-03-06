﻿using System;
using Microsoft.EntityFrameworkCore;
using TrainingApp.Data.DTO.Account;
using TrainingApp.Data.DTO.Goods;
using TrainingApp.Data.Models.Account;
using TrainingApp.Data.Models.Goods;

namespace TrainingApp.Data.Contexts
{
    public class TrainingAppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbQuery<UserDetailDTO> UserDetailDTO { get; set; }
        public DbQuery<UserListDTO> UserListDTO { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbQuery<RoleListDTO> RoleListDTO { get; set; }

        public DbSet<Goods> Goods { get; set; }
        public DbQuery<GoodsListDTO> GoodsListDTO { get; set; }


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

            User superadminUser = new User { Id = Guid.NewGuid(), Email = "superadmin@gmail.com", Password = "1", RoleId = superadminRole.Id, FirstName = "a", LastName = "a", SurName = "a" };
            User adminUser = new User { Id = Guid.NewGuid(), Email = "admin@gmail.com", Password = "1", RoleId = adminRole.Id, FirstName = "a", LastName = "a", SurName = "a" };
            User userUser = new User { Id = Guid.NewGuid(), Email = "user@gmail.com", Password = "1", RoleId = userRole.Id, FirstName = "a", LastName = "a", SurName = "a" };

            Goods good1 = new Goods { Id = Guid.NewGuid(), Name = "Good1", Price = 10, Count = 3  };
            Goods good2 = new Goods { Id = Guid.NewGuid(), Name = "Good2", Price = 20, Count = 4 };

            modelBuilder.Entity<Role>().HasData(new Role[] { superadminRole, adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { superadminUser, adminUser, userUser });
            modelBuilder.Entity<Goods>().HasData(new Goods[] { good1, good2 });
            base.OnModelCreating(modelBuilder);
        }
    }
}
