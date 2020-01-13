﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MisFinder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MisFinder.Data.Data.Map
{
    class StateMap : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.HasData(
                new State { Id = 1, Name="Abia"},
                new State {Id=2, Name="Adamawa" },
                new State { Id=3,Name="Akwa Ibom"},
                new State {Id= 4,Name="Anambra" },
                new State {Id=5 ,Name="Bauchi" },
                new State {Id= 6,Name="Bayelsa" },
                new State {Id= 7,Name="Benue" },
                new State {Id= 8,Name="Borno" },
                new State {Id= 9,Name="CrossRiver" },
                new State {Id= 10,Name="Delta" },
                new State {Id= 11,Name="Ebonyi" },
                new State {Id= 12,Name="Edo" },
                new State {Id= 13,Name="Ekiti" },
                new State {Id= 14,Name="Enugu" },
                new State {Id= 15,Name="Gombe" },
                new State {Id= 16,Name="Imo" },
                new State {Id= 17,Name="Jigawa" },
                new State {Id= 18,Name="Kaduna" },
                new State {Id= 19,Name="Kano" },
                new State {Id= 20,Name="Katsina" },
                new State {Id= 21,Name="Kebbi" },
                new State {Id= 22,Name="Kogi" },
                new State {Id= 23,Name="Kwara" },
                new State {Id= 24,Name="Lagos" },
                new State {Id= 25,Name="Nassarawa" },
                new State {Id= 26,Name="Niger" },
                new State {Id= 27,Name="Ogun" },
                new State {Id= 28,Name="Ondo" },
                new State {Id= 29,Name="Osun" },
                new State {Id= 30,Name="Oyo" },
                new State {Id= 31,Name="Plateau" },
                new State {Id= 32,Name="Rivers" },
                new State {Id= 33,Name="Sokoto" },
                new State {Id= 34,Name="Taraba" },
                new State {Id= 35,Name="Yobe" },
                new State { Id=36, Name= "Zamfara"},
                new State { Id=37, Name= "Abuja"}
                );
              
            
        }
    }
}
