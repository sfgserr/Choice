using Choice.ReviewService.Api.Entities;

namespace Choice.ReviewService.Api.ViewModels
{
    public class ReviewViewModel
    {
        public ReviewViewModel(Review review)
        {
            Id = review.Id;
            Author = review.Author!;
            UserGuid = review.UserGuid;
            PhotoUris = review.PhotoUris;
            Text = review.Text;
            Grade = review.Grade;
        }

        public int Id { get; }
        public Author Author { get; }
        public string UserGuid { get; }
        public List<string> PhotoUris { get; }
        public string Text { get; }
        public int Grade { get; }
    }
}
