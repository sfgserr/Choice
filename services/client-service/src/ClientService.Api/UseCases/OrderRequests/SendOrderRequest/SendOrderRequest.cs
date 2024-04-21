namespace Choice.ClientService.Api.UseCases.OrderRequests.SendOrderRequest
{
    public record SendOrderRequest
    {
        public SendOrderRequest(string description, List<string> photoUris, int categoryId, int searchRadius,
            bool toKnowPrice, bool toKnowDeadline, bool toKnowEnrollmentDate)
        {
            Description = description;
            PhotoUris = photoUris;
            CategoryId = categoryId;
            SearchRadius = searchRadius;
            ToKnowPrice = toKnowPrice;
            ToKnowDeadline = toKnowDeadline;
            ToKnowEnrollmentDate = toKnowEnrollmentDate;
        }

        public string Description { get; }
        public int CategoryId { get; }
        public List<string> PhotoUris { get; }
        public int SearchRadius { get; }
        public bool ToKnowPrice { get; }
        public bool ToKnowDeadline { get; }
        public bool ToKnowEnrollmentDate { get; }
    }
}
