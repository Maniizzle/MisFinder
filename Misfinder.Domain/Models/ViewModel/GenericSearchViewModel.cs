using System;
using System.Collections.Generic;
using System.Text;

namespace MisFinder.Domain.Models.ViewModel
{
    public class GenericSearchViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public SearchViewModel SearchView { get; set; }
    }
}