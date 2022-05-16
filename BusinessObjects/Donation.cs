using System;

namespace OnlyFundsAPI.BusinessObjects
{
    public class Donation
    {
        public int DonationID { get; set; }
        public int DonatorID { get; set; }
        public int DonatedID { get; set; }
        public DateTime DonationTime { get; set; }
        public double Amount { get; set; }
        public User Donator { get; set; }
        public User DonatedUser { get; set; }
    }
}