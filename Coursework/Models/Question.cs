using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Coursework.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string OwnerId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)] public DateTime DateCreated { get; set; }

        [DataType(DataType.Date)] public DateTime? DateModified { get; set; }

        public QuestionStatus Status { get; set; } = QuestionStatus.Opened;

        public int Score { get; set; } = 0;
    }
    
    public enum QuestionStatus
    {
        Opened,
        Resolved,
        Duplicate
    }
}