using System;
using System.Collections.Generic;
using System.Text;
using FS20.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FS20.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<EmployeePosition> EmployeePosition { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<CompetitionType> CompetitionType { get; set; }
        public DbSet<Season> Season { get; set; }
        public DbSet<Competition> Competition { get; set; }
        public DbSet<TrainingType> TrainingType { get; set; }
        public DbSet<Training> Training { get; set; }
        public DbSet<Opponent> Opponent { get; set; }
        public DbSet<Stadium> Stadium { get; set; }
        public DbSet<TrainingsPresence> TrainingsPresence { get; set; }
        public DbSet<MembershipFee> MembershipFee { get; set; }
        public DbSet<PlayerMembershipFee> PlayerMembershipFee { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Groups1)
                .WithOne(e => e.Employee)
                .HasForeignKey(e => e.CoachID)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Groups2)
                .WithOne(e => e.Employee1)
                .HasForeignKey(e => e.AssistantCoach1ID)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Employee>()
              .HasMany(a => a.Groups3)
              .WithOne(a => a.Employee2)
              .HasForeignKey(a => a.AssistantCoach2ID)
            .OnDelete(DeleteBehavior.NoAction)
            ;


        }

    }
}
