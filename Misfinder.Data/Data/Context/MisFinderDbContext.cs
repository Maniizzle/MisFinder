using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MisFinder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MisFinder.Data.Data.Context
{
   public class MisFinderDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public MisFinderDbContext(DbContextOptions<MisFinderDbContext> option):base(option)
        { }
       //  public DbSet<ClaimerDetail> ClaimerDetails { get; set; }
        public DbSet<LostItem> LostItems{ get; set; }
      //  public DbSet<ClaimerDetail> ClaimerItems { get; set; }
        public DbSet<FoundItem> FoundItems { get; set; }
                                       //public MisFinderDbContext(IdentityDbContext<MisFinderDbContext>)
                                       //{
    protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<FoundAndLostItem>()
            .HasKey(s => new { s.FoundItemId, s.LostItemId });
            base.OnModelCreating(builder);
        }
        //}

    }
}
