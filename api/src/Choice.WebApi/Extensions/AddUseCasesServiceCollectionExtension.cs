using Choice.Application.UseCases.Categories.GetCategories;
using Choice.Application.UseCases.Categories.UpdateCategory;
using Choice.Application.UseCases.ChatMessages.GetChatMessages;
using Choice.Application.UseCases.Clients.CreateClient;
using Choice.Application.UseCases.Clients.GetClient;
using Choice.Application.UseCases.Clients.GetClients;
using Choice.Application.UseCases.Clients.UpdateClient;
using Choice.Application.UseCases.Companies.CreateCompany;
using Choice.Application.UseCases.Companies.GetCompanies;
using Choice.Application.UseCases.Companies.GetCompany;
using Choice.Application.UseCases.Companies.UpdateCompany;
using Choice.Application.UseCases.Messages.SendChatMessage;
using Choice.Application.UseCases.OrderMessages.GetOrderMessages;
using Choice.Application.UseCases.OrderMessages.SendOrderMessage;
using Choice.Application.UseCases.OrderMessages.UpdateOrderMessage;
using Choice.Application.UseCases.Orders.CreateOrder;
using Choice.Application.UseCases.Orders.GetOrder;
using Choice.Application.UseCases.Orders.UpdateOrder;
using Choice.Application.UseCases.Reviews.GetReviews;
using Choice.Application.UseCases.Reviews.SendReview;
using Microsoft.Extensions.DependencyInjection;

namespace Choice.WebApi.Extensions
{
    public static class AddUseCasesServiceCollectionExtension
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<ISendReviewUseCase, SendReviewUseCase>();

            services.AddScoped<IGetReviewsUseCase, GetReviewsUseCase>();

            services.AddScoped<IUpdateOrderUseCase, UpdateOrderUseCase>();

            services.AddScoped<IGetOrderUseCase, GetOrderUseCase>();

            services.AddScoped<ICreateOrderUseCase, CreateOrderUseCase>();
            services.Decorate<ICreateOrderUseCase, CreateOrderValidationUseCase>();

            services.AddScoped<IUpdateOrderMessageUseCase, UpdateOrderMessageUseCase>();

            services.AddScoped<ISendOrderMessageUseCase, SendOrderMessageUseCase>();
            services.Decorate<ISendOrderMessageUseCase, SendOrderMessageValidationUseCase>();

            services.AddScoped<IGetOrderMessagesUseCase, GetOrderMessagesUseCase>();

            services.AddScoped<IUpdateCompanyUseCase, UpdateCompanyUseCase>();

            services.AddScoped<IGetCompanyUseCase, GetCompanyUseCase>();

            services.AddScoped<IGetCompaniesUseCase, GetCompaniesUseCase>();

            services.AddScoped<ICreateCompanyUseCase, CreateCompanyUseCase>();
            services.Decorate<ICreateCompanyUseCase, CreateCompanyValidationUseCase>();

            services.AddScoped<IUpdateClientUseCase, UpdateClientUseCase>();

            services.AddScoped<IGetClientsUseCase, GetClientsUseCase>();

            services.AddScoped<IGetClientUseCase, GetClientUseCase>();

            services.AddScoped<ICreateClientUseCase, CreateClientUseCase>();
            services.Decorate<ICreateClientUseCase, CreateClientValidationUseCase>();

            services.AddScoped<ISendChatMessageUseCase, SendChatMessageUseCase>();
            services.Decorate<ISendChatMessageUseCase, SendChatMessageValidationUseCase>();

            services.AddScoped<IGetChatMessagesUseCase, GetChatMessagesUseCase>();

            services.AddScoped<IUpdateCategoryUseCase, UpdateCategoryUseCase>();

            services.AddScoped<IGetCategoriesUseCase, GetCategoriesUseCase>();

            return services;
        }
    }
}
