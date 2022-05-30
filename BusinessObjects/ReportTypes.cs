using System.ComponentModel;

namespace OnlyFundsAPI.BusinessObjects
{
    public enum ReportTypes
    {
        [Description("Post")]
        Post,
        [Description("Comment")]
        Comment,
        [Description("User")]
        User
    }
}