using System.Collections.Generic;

namespace OnlyFundsAPI.BusinessObjects
{
    public class PostTag
    {
        public int TagID { get; set; }
        public string TagName { get; set; }
        public bool Active { get; set; }
        ICollection<PostTagMap> PostMaps { get; set; }
    }
}