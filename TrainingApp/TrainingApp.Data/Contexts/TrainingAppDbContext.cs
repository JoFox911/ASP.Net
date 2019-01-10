﻿using Microsoft.EntityFrameworkCore;
using TrainingApp.Data.Models;

namespace TrainingApp.Data.Contexts
{
    public class TrainingAppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public TrainingAppDbContext(DbContextOptions<TrainingAppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
