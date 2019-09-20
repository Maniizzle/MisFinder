using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MisFinder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MisFinder.Data.Data.Map
{
    class LocalGovernmentMap : IEntityTypeConfiguration<LocalGovernment>

    {
        public void Configure(EntityTypeBuilder<LocalGovernment> builder)
        {
            builder.HasData(new LocalGovernment { Id = 1, Name = "Agege", StateId = 24 },
            new LocalGovernment { Id = 2, Name = "Alimosho", StateId = 24 },
            new LocalGovernment { Id = 3, Name = "Apapa", StateId = 24 },
            new LocalGovernment { Id = 4, Name = "Ifako-Ijaye", StateId = 24 },
            new LocalGovernment { Id = 5, Name = "Ikeja", StateId = 24 },
            new LocalGovernment { Id = 6, Name = "Kosofe", StateId = 24 },
            new LocalGovernment { Id = 7, Name = "Mushin", StateId = 24 },
            new LocalGovernment { Id = 8, Name = "Oshodi-Isolo", StateId = 24 },
            new LocalGovernment { Id = 9, Name = "Shomolu", StateId = 24 },
            new LocalGovernment { Id = 10, Name = "Eti-Osa", StateId = 24 },
            new LocalGovernment { Id = 11, Name = "LagosIsland", StateId = 24 },
            new LocalGovernment { Id = 12, Name = "Lagos Mainland", StateId = 24 },
            new LocalGovernment { Id = 13, Name = "Surulere", StateId = 24 },
            new LocalGovernment { Id = 14, Name = "Ojo", StateId = 24 },
            new LocalGovernment { Id = 15, Name = "Ajeromi-Ifelodun", StateId = 24 },
            new LocalGovernment { Id = 16, Name = "Amuwo-Odofin", StateId = 24 },
            new LocalGovernment { Id = 17, Name = "Badagry", StateId = 24 },
            new LocalGovernment { Id = 18, Name = "Ikorodu", StateId = 24 },
            new LocalGovernment { Id = 19, Name = "Ibeju-Lekki", StateId = 24 },
            new LocalGovernment { Id = 20, Name = "Epe", StateId = 24 });

        }
    }
}
