namespace Choice.ClientService.Api.UseCases.OrderRequests.SendOrderRequest
{
    public record SendOrderRequest
    {
        public SendOrderRequest(string description, List<int> categories, int searchRadius,
            bool toKnowPrice, bool toKnowDeadline, bool toKnowEnrollmentDate)
        {
            Description = description;
            Categories = categories;
            SearchRadius = searchRadius;
            ToKnowPrice = toKnowPrice;
            ToKnowDeadline = toKnowDeadline;
            ToKnowEnrollmentDate = toKnowEnrollmentDate;
        }

        public string Description { get; }
        public List<int> Categories { get; }
        public int SearchRadius { get; }
        public bool ToKnowPrice { get; }
        public bool ToKnowDeadline { get; }
        public bool ToKnowEnrollmentDate { get; }
    }
}
