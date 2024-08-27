﻿using Microsoft.EntityFrameworkCore;
using VacancyOneTestTask.DataAccess.Entities;

namespace VacancyOneTestTask.DataAccess
{
    public class VacancyOneDbContext : DbContext
    {
        public VacancyOneDbContext(DbContextOptions<VacancyOneDbContext> options) : base(options)
        {
        }

        public DbSet<Entities.Task> Tasks { get; set; }

        public DbSet<AttachedFile> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entities.Task>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<AttachedFile>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<Entities.Task>()
                .HasMany(t => t.Files)
                .WithOne(f => f.Task)
                .HasForeignKey(f => f.TaskId);
        }
    }
}
