namespace OnlyFundsAPI.BusinessObjects
{
    public class OTP
    {
        public int UserID { get; set; }
        public string Code { get; set; }
        public User User { get; set; }
    }
}