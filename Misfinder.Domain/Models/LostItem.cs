using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MisFinder.Domain.Models
{
    public class LostItem
    {
        public int Id { get; set; }
       
        public string NameOfLostItem { get; set; }
        [StringLength(250, MinimumLength = 5)]
        public string Description { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        [MaxLength(100)]
        public string SpecificLocation { get; set; }
        public string Color { get; set; }
        public DateTime DateMisplaced { get; set; }
        public ICollection<FoundAndLostItem> FoundLostItems { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser LostItemUser { get; set; }
        public bool IsFound { get; set; }
        public DateTime DateCreated { get; set; }
       // public LCDA LocationArea { get; set; }                    //public TimeSpan TimeMisplaced { get; set; }
                                                                   //public string StateFound { get; set; }



    }
    //public enum Location
    //{   
    //    Ikeja=1,
    //    OshodiIsolo,
    //    LagosIsland,
    //    IfakoIjaye,
    //    Alimosho,
    //    Agege,
    //    AjeromiIfelodun,
    //    AmuwoOdofin,
    //    Apapa,
    //    Badagry,
    //    Epe,
    //    EtiOsa,
    //    IbejuLekki,
    //    Ikorodu,
    //    Kosofe,
    //    LagosMainland,
    //    Mushin,
    //    Ojo,
    //    Shomolu,
    //    Surulere

    //}

    public enum LCDA
    {

    }
}

