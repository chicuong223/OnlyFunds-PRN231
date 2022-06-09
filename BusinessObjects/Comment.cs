using System;
using System.Collections.Generic;

namespace OnlyFundsAPI.BusinessObjects
{
    public class Comment
    {
        public int? CommentID { get; set; }
        public int? UploaderID { get; set; }
        public int PostID { get; set; }
        public DateTime? CommentTime { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; }
        public User Uploader { get; set; }
        public Post Post { get; set; }
        public ICollection<CommentLike> Likes { get; set; }
    }
}