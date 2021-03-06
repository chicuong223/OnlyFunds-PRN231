#nullable disable

using System;
using System.Collections.Generic;

namespace OnlyFundsAPI.BusinessObjects
{
    public class Post
    {
        public int PostID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UploadTime { get; set; }
        public int UploaderID { get; set; }
        public FileType? AttachmentType { get; set; }
        public string FileURL { get; set; }
        public string Preview { get; set; }
        public PostStatus Status { get; set; }
        public User Uploader { get; set; }
        public bool Active { get; set; }
        public ICollection<PostTagMap> TagMaps { get; set; }
        public ICollection<PostLike> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Bookmark> Bookmarks { get; set; }
    }
}