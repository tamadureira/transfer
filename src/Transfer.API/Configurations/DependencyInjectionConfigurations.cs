using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Transfer.Application.Commands;
using Transfer.Application.Queries;
using Transfer.Core.Communication.Mediator;
using Transfer.Core.Messages.CommonMessages.Notifications;
using Transfer.Domain;
using Transfer.Infra.Data.Context;
using Transfer.Infra.Repository;

namespace Transfer.API.Configurations
{
    public static class DependencyInjectionConfigurations
    {
        public static IServiceCollection ConfigureDI(this IServiceCollection services)
        {
            // Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            //Transfer
            services.AddScoped<TransferContext>();
            services.AddScoped<ITransferRepository, TransferRepository>();
            services.AddScoped<ITransferQueries, TransferQueries>();
            services.AddScoped<IRequestHandler<TransferCarryOutCommand, bool>, TransferCarryOutCommandHandler>();

            return services;
        }
    }
}
