namespace Coursework.Models
{
    public class TopAnswer
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public int Score { get; set; }
    }
}