using System;
using System.Collections.Generic;
using System.Text;

namespace MisFinder.Domain.Models
{
    public enum ItemCategory : byte
    {
        Electronics = 1,
        Cosmetics,
        Automobile,
        Keys,
        Jewellery,
        Stationery,
        Food,
        Animal,
        Bags,
        Clothing,
        Wears
    }

    public enum ClaimStatus : byte
    {
        Pending = 1,
        Invalid,
        Valid,
        Successful
    }
}