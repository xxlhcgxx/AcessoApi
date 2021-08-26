using Api.Domain.Interfaces.Services.Acesso;
using Api.Domain.Interfaces.Services.Log;
using Api.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ITransfer, TransferService>();
            serviceCollection.AddTransient<IAccount, AccountService>();
            serviceCollection.AddTransient<ILoggerService, LoggerService>();


        }
    }
}
