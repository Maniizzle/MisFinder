using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MisFinder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MisFinder.Data.Data.Map
{
    class LocationMap : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasData(
                new Location { Id = 1, Name="Abia"},
                new Location {Id=2, Name="Adamawa" },
                new Location { Id=3,Name="Akwa Ibom"},
                new Location {Id= 4,Name="Anambra" },
                new Location {Id=5 ,Name="Bauchi" },
                new Location {Id= 6,Name="Bayelsa" },
                new Location {Id= 7,Name="Benue" },
                new Location {Id= 8,Name="Borno" },
                new Location {Id= 9,Name="CrossRiver" },
                new Location {Id= 10,Name="Delta" },
                new Location {Id= 11,Name="Ebonyi" },
                new Location {Id= 12,Name="Edo" },
                new Location {Id= 13,Name="Ekiti" },
                new Location {Id= 14,Name="Enugu" },
                new Location {Id= 15,Name="Gombe" },
                new Location {Id= 16,Name="Imo" },
                new Location {Id= 17,Name="Jigawa" },
                new Location {Id= 18,Name="Kaduna" },
                new Location {Id= 19,Name="Kano" },
                new Location {Id= 20,Name="Katsina" },
                new Location {Id= 21,Name="Kebbi" },
                new Location {Id= 22,Name="Kogi" },
                new Location {Id= 23,Name="Kwara" },
                new Location {Id= 24,Name="Lagos" },
                new Location {Id= 25,Name="Nassarawa" },
                new Location {Id= 26,Name="Niger" },
                new Location {Id= 27,Name="Ogun" },
                new Location {Id= 28,Name="Ondo" },
                new Location {Id= 29,Name="Osun" },
                new Location {Id= 30,Name="Oyo" },
                new Location {Id= 31,Name="Plateau" },
                new Location {Id= 32,Name="Rivers" },
                new Location {Id= 33,Name="Sokoto" },
                new Location {Id= 34,Name="Taraba" },
                new Location {Id= 35,Name="Yobe" },
                new Location { Id=36, Name= "Zamfara"},
                new Location { Id=37, Name= "Abuja"}
                );
              
            
        }
    }
}
