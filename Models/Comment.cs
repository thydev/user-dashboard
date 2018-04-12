using System;

namespace userdb.Models
{
    public class Comment : BaseEntity
    {
        public int CommentId { get; set; }
        public int MessageId { get; set; }
        public Message Message { get; set; }
        public string CommentText { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

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