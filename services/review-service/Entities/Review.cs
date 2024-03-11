namespace Choice.ReviewService.Api.Entities
{
    public class Review
    {
        public Review(string authorGuid, string userGuid, List<string> photoUris, string text, int grade)
        {
            AuthorGuid = authorGuid;
            UserGuid = userGuid;
            PhotoUris = photoUris;
            Text = text;
            Grade = grade;
        }

        public int Id { get; }
        public Author? Author { get; private set; }
        public string AuthorGuid { get; private set; }
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
