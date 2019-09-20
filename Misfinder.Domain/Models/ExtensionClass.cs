using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisFinder.Domain.Models
{
    public static class ExtensionClass
    {

        public static IEnumerable<FoundItem> FilterByName(
        this IEnumerable<FoundItem> foundItemEnum, string name)
        {
            foreach (var foundItem in foundItemEnum)
            {
                if ((foundItem?.NameOfFoundItem ?? "") == name)
                {
                    yield return foundItem;
                }
            }
        }
    }
}