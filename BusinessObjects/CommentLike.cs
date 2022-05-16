namespace OnlyFundsAPI.BusinessObjects
{
    public class CommentLike
    {
        public int CommentID { get; set; }
        public int UserID { get; set; }
        public Comment Comment { get; set; }
        public User User { get; set; }
    }
}