using System.ComponentModel.DataAnnotations;

namespace Coursework.Models
{
    public class AnswerVote
    {
        [Key]
        public int VoteId { get; set; }
        public string OwnerId { get; set; }
        public int Reaction { get; set; }
        
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
    }
}