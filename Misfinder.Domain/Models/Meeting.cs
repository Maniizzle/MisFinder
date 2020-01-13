using System;
using System.Collections.Generic;
using System.Text;

namespace MisFinder.Domain.Models
{
    public class Meeting : Audit
    {
        public int Id { get; set; }
        public DateTime UserSelectedDate { get; set; }
        public DateTime USerSelectedDate2 { get; set; }

        public bool IsSelectFirstDate { get; set; }
        public bool IsSelectSecondDate { get; set; }
        public string MeetingVenue { get; set; }
        public bool IsUserTwoSelectedDate { get; set; }

        //  public bool ISUserOneSeleectedDate { get; set; }
        public DateTime? DateUserTwoSelectedDate { get; set; }

        public DateTime MeeetingTime { get; set; }
        public LostItem LostItem { get; set; }
        public FoundItem FoundItem { get; set; }
        public int SelectedCount { get; set; }
        public MeetingStatus Status { get; set; } = MeetingStatus.Pending;
        public int LocalGovernmentId { get; set; }
        public LocalGovernment LocalGovernment { get; set; }
        public bool IsAdminIncluded { get; set; }
    }

    public enum MeetingStatus
    {
        Scheduled = 1,
        Rescheduled,
        Succeessful,
        Pending
    }
}