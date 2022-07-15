using System.ComponentModel;

namespace OnlyFundsAPI.BusinessObjects
{
    public enum FileType
    {
        [Description("File")]
        File = 0,
        [Description("Video")]
        Video = 1,
        [Description("Image")]
        Image = 2,
        [Description("Music")]
        Music = 3
    }
}