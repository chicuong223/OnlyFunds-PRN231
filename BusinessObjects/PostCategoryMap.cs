namespace OnlyFundsAPI.BusinessObjects
{
    public class PostCategoryMap
    {
        public int CategoryID { get; set; }
        public int PostID { get; set; }
        public PostCategory Category { get; set; }
        public Post Post { get; set; }
    }
}