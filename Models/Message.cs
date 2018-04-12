using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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

        public List<Comment> Comments { get; set; }

        public Message()
        {
            Comments = new List<Comment>();
        }

        public string Duration {
            get {
                double min = DateTime.Now.Subtract(this.CreatedAt).TotalMinutes;
                if ( min < 60 ) return $"{(int)min} minutes ago";

                double hrs = DateTime.Now.Subtract(this.CreatedAt).TotalHours;
                if ( hrs < 24 ) return $"{(int)hrs} hours ago";

                double days = DateTime.Now.Subtract(this.CreatedAt).TotalDays;
                return $"{(int)days} days ago";
            }
        }
    }
}