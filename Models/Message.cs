using System;

namespace userdb.Models
{
    public class Message : BaseEntity
    {
        public int MessageId { get; set; }
        public string MessageText { get; set; }

        public int Sender_UserId { get; set; }
        public User Sender { get; set; }
        
        public int Receiver_UserId { get; set; }
        public User Receiver { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}