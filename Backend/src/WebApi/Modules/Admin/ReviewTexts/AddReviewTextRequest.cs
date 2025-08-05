namespace WebApi.Modules.Admin.ReviewTexts
{
    public class AddReviewTextRequest
    {
        public AddReviewTextRequest(int grade, string text)
        {
            Grade = grade;
            Text = text;
        }

        public int Grade { get; }
        
        public string Text { get; }
    }
}