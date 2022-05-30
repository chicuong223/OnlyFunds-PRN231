using System;

namespace OnlyFundsAPI.BusinessObjects
{
    public class Report
    {
        public int ReportID { get; set; }
        public int ReporterID { get; set; }
        public int ReportedObjectID { get; set; }
        public ReportTypes ReportType { get; set; }
        public string Description { get; set; }
        public DateTime ReportTime { get; set; }
        public ReportStatus Status { get; set; }
        public User Reporter { get; set; }
    }
}