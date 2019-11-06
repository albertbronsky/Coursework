using System;
using System.ComponentModel.DataAnnotations;

namespace Coursework.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public string OwnerId { get; set; }

        public string Message { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateModified { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public bool Accepted { get; set; }
        //public List<int> LikedBy = new List<int>();
        //public List<int> DislikedBy = new List<int>();
    }
}