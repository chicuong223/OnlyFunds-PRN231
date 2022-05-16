namespace OnlyFundsAPI.BusinessObjects
{
    public class PostLike
    {
        public int PostID { get; set; }
        public int UserID { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
    }
}