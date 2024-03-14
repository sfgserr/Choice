namespace Choice.ReviewService.Api.Entities
{
    public class Review
    {
        public Review(string authorId, string userGuid, List<string> photoUris, string text, int grade)
        {
            AuthorId = authorId;
            UserGuid = userGuid;
            PhotoUris = photoUris;
            Text = text;
            Grade = grade;
        }

        public int Id { get; private set; }
        public string AuthorId { get; private set; }
        public Author? Author { get; private set; }
        public string UserGuid { get; private set; }
        public List<string> PhotoUris { get; private set; }
        public string Text { get; private set; }

        private int _grade;

        public int Grade 
        { 
            get => _grade;
            private set => _grade = value < 1 ? 1 : value > 5 ? 5 : value;
        }
    }
}
