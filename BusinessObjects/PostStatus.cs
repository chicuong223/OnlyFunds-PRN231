using System.ComponentModel;

namespace OnlyFundsAPI.BusinessObjects
{
    public enum PostStatus
    {
        [Description("Active")]
        Active,

        [Description("Inactive")]
        Inactive,

        [Description("Banned")]
        Banned
    }
}