namespace OnlyFundsAPI.BusinessObjects
{
    public class Follow
    {
        public int FollowerID { get; set; }
        public int FolloweeID { get; set; }
        public User Follower { get; set; }
        public User Followee { get; set; }
    }
}