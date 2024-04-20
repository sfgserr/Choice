namespace Choice.ClientService.Api.UseCases.OrderRequests.SendOrderRequest
{
    public record SendOrderRequest
    {
        public SendOrderRequest(string description, List<string> photoUris, List<int> categories, int searchRadius,
            bool toKnowPrice, bool toKnowDeadline, bool toKnowEnrollmentDate)
        {
            Description = description;
            PhotoUris = photoUris;
            Categories = categories;
            SearchRadius = searchRadius;
            ToKnowPrice = toKnowPrice;
            ToKnowDeadline = toKnowDeadline;
            ToKnowEnrollmentDate = toKnowEnrollmentDate;
        }

        public string Description { get; }
        public List<int> Categories { get; }
        public List<string> PhotoUris { get; }
        public int SearchRadius { get; }
        public bool ToKnowPrice { get; }
        public bool ToKnowDeadline { get; }
        public bool ToKnowEnrollmentDate { get; }
    }
}
