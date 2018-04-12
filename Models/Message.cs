using System;

namespace userdb.Models
{
    public class Message : BaseEntity
    {
        public int MessageId { get; set; }
        public string MessageText { get; set; }

        public int SenderUserId { get; set; }
        public User Sender { get; set; }
        
        public int ReceiverUserId { get; set; }
        public User Receiver { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}