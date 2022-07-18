using System.ComponentModel;

namespace OnlyFundsAPI.BusinessObjects
{
    public enum ReportStatus
    {
        [Description("Unresolved")]
        Unresolved,

        [Description("Resolved")]
        Resolved,

        [Description("Rejected")]
        Rejected
    }
}