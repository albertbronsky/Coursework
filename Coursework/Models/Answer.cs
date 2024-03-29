﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

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
        public int Score { get; set; } = 0;
    }
}