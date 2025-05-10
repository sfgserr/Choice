namespace WebApi.Modules.Admin.Reviews
{
    public class EditReviewRequest
    {
        public EditReviewRequest(Guid reviewId, string text, int grade)
        {
            ReviewId = reviewId;
            Text = text;
            Grade = grade;
        }

        public Guid ReviewId { get; }

        public string Text { get; }
        
        public int Grade { get; }
    }
}