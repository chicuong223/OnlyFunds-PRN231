using System.Collections.Generic;

namespace OnlyFundsAPI.BusinessObjects
{
    public class PostCategory
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public bool Active { get; set; }
        ICollection<PostCategoryMap> PostMaps { get; set; }
    }
}