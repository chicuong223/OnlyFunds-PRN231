namespace OnlyFundsAPI.BusinessObjects
{
    public class PostTagMap
    {
        public int TagID { get; set; }
        public int PostID { get; set; }
        public PostTag Tag { get; set; }
        public Post Post { get; set; }
    }
}