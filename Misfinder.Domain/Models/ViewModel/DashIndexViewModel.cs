using System;
using System.Collections.Generic;
using System.Text;

namespace MisFinder.Domain.Models.ViewModel
{
    public class DashIndexViewModel
    {
        public int LostItemsCount { get; set; }
        public int FoundItemCount { get; set; }
        public int FOundItemClaimCount { get; set; }
        public int LostItemClaimCount { get; set; }
        public int ActiveClaimCount { get; set; }
        public int ScheduledMeetingCount { get; set; }
    }
}