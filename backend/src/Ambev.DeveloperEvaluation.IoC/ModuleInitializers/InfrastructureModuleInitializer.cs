using Ambev.DeveloperEvaluation.Application.Services;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using EasyNetQ;
using EasyNetQ.Serialization.NewtonsoftJson;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class InfrastructureModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<DefaultContext>());
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ISaleRepository, SaleRepository>();

        var rabbitMqConnection = builder.Configuration["ConnectionStrings:RabbitMqConnection"]?.ToString();
        builder.Services.AddSingleton(RabbitHutch.CreateBus(rabbitMqConnection, register =>
        {
            register.Register(typeof(ISerializer), _ => new NewtonsoftJsonSerializer());
        }));

        builder.Services.AddScoped<IEventPublisherService, RabbitMqPublisher>();
    }
}