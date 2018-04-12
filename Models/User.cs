using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace userdb.Models
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Level { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [InverseProperty("Sender")]
        public List<Message> SentMessages { get; set; }

        [InverseProperty("Receiver")]
        public List<Message> ReceivedMessages { get; set; }

        public List<Comment> Comments { get; set; }

        public User()
        {
            SentMessages = new List<Message>();
            ReceivedMessages = new List<Message>();
            Comments = new List<Comment>();
        }

        public string FullName { 
            get {
                return this.FirstName + " " + this.LastName;
            }
        }

        public string LevelString {
            get {
                return this.Level == 1 ? "Admin" : "Normal";
            }
        }
    }
}