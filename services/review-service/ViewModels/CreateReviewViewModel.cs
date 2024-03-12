namespace Choice.ReviewService.Api.ViewModels
{
    public class CreateReviewViewModel
    {
        public CreateReviewViewModel(string guid, string text, int grade, List<string> photoUris)
        {
            Guid = guid;
            Text = text;
            Grade = grade;
            PhotoUris = photoUris;
        }

        public string Guid { get; }
        public string Text { get; }
        public int Grade { get; }
        public List<string> PhotoUris { get; }
    }
}
