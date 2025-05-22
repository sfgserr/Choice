namespace Administration.Application.Queries.GetAuthorReviews
{
    public class ReviewDto
    {
        public Guid Id { get; }

        public string Name { get; }

        public string Text { get; }
        
        public int Grade { get; }
    }
}