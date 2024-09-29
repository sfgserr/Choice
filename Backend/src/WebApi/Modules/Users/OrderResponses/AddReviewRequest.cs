namespace WebApi.Modules.Users.OrderResponses
{
    public class AddReviewRequest
    {
        public AddReviewRequest(Guid responseId, Guid toUserId, string text, int grade)
        {
            ResponseId = responseId;
            ToUserId = toUserId;
            Text = text;
            Grade = grade;
        }
        
        public Guid ResponseId { get; }
        
        public Guid ToUserId { get; }

        public string Text { get; }
        
        public int Grade { get; }
    }
}