using System.ComponentModel.DataAnnotations;

namespace Coursework.Models
{
    public class QuestionVote
    {
        [Key]
        public int VoteId { get; set; }
        public string OwnerId { get; set; }
        public int Reaction { get; set; }
        
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}