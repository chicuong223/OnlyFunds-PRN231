#nullable disable

using System.Collections.Generic;

namespace OnlyFundsAPI.BusinessObjects
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Banned { get; set; }
        public bool Active { get; set; }
        public string AvatarUrl { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Follow> Followers { get; set; }  //who follows you
        public ICollection<Follow> Follows { get; set; }    //you follow
        public ICollection<Bookmark> Bookmarks { get; set; }
    }
}