namespace OnlyFundsAPI.BusinessObjects
{
    public class Bookmark
    {
        public int PostID { get; set; }
        public int UserID { get; set; }
        public string Description { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
    }
}