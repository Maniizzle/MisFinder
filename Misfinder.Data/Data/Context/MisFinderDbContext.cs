using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MisFinder.Data.Data.Map;
using MisFinder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MisFinder.Data.Data.Context
{
    public class MisFinderDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public MisFinderDbContext(DbContextOptions<MisFinderDbContext> option) : base(option)
        { }

        //  public DbSet<ClaimerDetail> ClaimerDetails { get; set; }
        public DbSet<LostItem> LostItems { get; set; }

        //  public DbSet<ClaimerDetail> ClaimerItems { get; set; }
        public DbSet<FoundItem> FoundItems { get; set; }

        public DbSet<State> States { get; set; }
        public DbSet<LocalGovernment> LocalGovernments { get; set; }
        public DbSet<LostItemClaim> LostItemClaims { get; set; }
        public DbSet<FoundItemClaim> FoundItemClaims { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Meeting> Meetings { get; set; }

        //public MisFinderDbContext(IdentityDbContext<MisFinderDbContext>)
        //{
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new StateMap());
            builder.ApplyConfiguration(new LocalGovernmentMap());

            builder.Entity<LostItem>()
           .Property(p => p.ItemCategory)
           .HasConversion(
               new EnumToStringConverter<ItemCategory>());

            builder.Entity<FoundItem>()
            .Property(p => p.ItemCategory)
            .HasConversion(
                new EnumToStringConverter<ItemCategory>());

            builder.Entity<FoundItemClaim>()
                        .Property(p => p.ItemCategory)
                        .HasConversion(
                            new EnumToStringConverter<ItemCategory>());

            builder.Entity<LostItemClaim>()
         .Property(p => p.Status)
         .HasConversion(
             new EnumToStringConverter<ClaimStatus>());

            builder.Entity<FoundItemClaim>()
                        .Property(p => p.Status)
                        .HasConversion(
                            new EnumToStringConverter<ClaimStatus>());

            builder.Entity<Meeting>()
                       .Property(p => p.Status)
                       .HasConversion(
                           new EnumToStringConverter<MeetingStatus>());

            builder.Entity<IdentityRole>().HasData(
             new { Id = "3", Name = "User", NormalizedName = "User" });

            //builder.Entity<FoundItemClaim>()
            //           .Property(p => p.Status)
            //           .HasConversion(
            //               new EnumToStringConverter<MeetingStatus>());

            //  builder.Entity<FoundItem>().HasOne(c => c.State).WithMany().OnDelete(DeleteBehavior.Restrict);
            //builder.Entity<LostItem>().HasOne(c => c.State).WithMany().OnDelete(DeleteBehavior.Restrict);

            //}
        }
    }
}