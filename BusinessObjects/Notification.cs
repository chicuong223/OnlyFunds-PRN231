using System;

namespace OnlyFundsAPI.BusinessObjects
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public int ReceiverID { get; set; }
        public string Content { get; set; }
        public DateTime NotificationTime { get; set; }
        public bool IsRead { get; set; }
        public User Receiver { get; set; }
    }
}