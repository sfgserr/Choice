using Choice.CompanyService.Api.Entities;
using Choice.CompanyService.Api.Repositories;
using Choice.EventBus.Messages.Events;
using MassTransit;

namespace Choice.CompanyService.Api.Consumers
{
    public class ReviewLeftConsumer : IConsumer<ReviewLeftEvent>
    {
        private readonly ICompanyRepository _repository;

        public ReviewLeftConsumer(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<ReviewLeftEvent> context)
        {
            ReviewLeftEvent @event = context.Message;

            Company company = await _repository.Get(@event.UserGuid);

            if (company is null)
                return;

            company.AddReview(@event.Grade);

            await _repository.Update(company);
        }
    }
}
