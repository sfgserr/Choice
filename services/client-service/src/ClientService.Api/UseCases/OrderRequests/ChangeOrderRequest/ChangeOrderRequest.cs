namespace Choice.ClientService.Api.UseCases.OrderRequests.ChangeOrderRequest
{
    public class ChangeOrderRequest
    {
        public ChangeOrderRequest(int id, string description, List<string> photoUris, int categoryId, int searchRadius,
            bool toKnowPrice, bool toKnowDeadline, bool toKnowEnrollmentDate)
        {
            Id = id;
            Description = description;
            PhotoUris = photoUris;
            CategoryId = categoryId;
            SearchRadius = searchRadius;
            ToKnowPrice = toKnowPrice;
            ToKnowDeadline = toKnowDeadline;
            ToKnowEnrollmentDate = toKnowEnrollmentDate;
        }

        public int Id { get; }
        public string Description { get; }
        public int CategoryId { get; }
        public List<string> PhotoUris { get; }
        public int SearchRadius { get; }
        public bool ToKnowPrice { get; }
        public bool ToKnowDeadline { get; }
        public bool ToKnowEnrollmentDate { get; }
    }
}
