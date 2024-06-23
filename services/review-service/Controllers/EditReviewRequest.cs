namespace ReviewService.Api.Controllers
{
    public class EditReviewRequest
    {
        public EditReviewRequest(int id, int grade, string text, List<string> photoUris)
        {
            Id = id;
            Grade = grade;  
            Text = text;
            PhotoUris = photoUris;
        }

        public int Id { get; }
        public int Grade { get; }
        public string Text { get; }
        public List<string> PhotoUris { get; }
    }
}
