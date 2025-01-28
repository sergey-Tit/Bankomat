using Application.Accounts;
using Contracts.Accounts;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IBankAccountService, BankAccountService>();

        return collection;
    }
}