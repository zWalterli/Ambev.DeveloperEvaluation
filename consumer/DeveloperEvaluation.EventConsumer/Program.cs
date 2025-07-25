using DeveloperEvaluation.EventConsumer.Models;
using DeveloperEvaluation.EventConsumer.Utils;
using EasyNetQ;
using EasyNetQ.Serialization.NewtonsoftJson;

using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
    .Build();

var connectionString = config["RabbitMq:Connection"]?.ToString();
Console.WriteLine($"Connecting to RabbitMQ at '{connectionString}'");

var bus = RabbitHutch.CreateBus(connectionString, register =>
{
    register.Register(typeof(ISerializer), _ => new NewtonsoftJsonSerializer());
});

Console.WriteLine("🎧 Listening for messages...");

await MessageConsumer.RegisterJsonConsumer<EventMessage>(bus, EventQueueName.SaleCreated, async msg =>
{
    Console.WriteLine($"📥 SaleCreated: {msg.Id}");
    await Task.CompletedTask;
});

await MessageConsumer.RegisterJsonConsumer<EventMessage>(bus, EventQueueName.SaleUpdated, async msg =>
{
    Console.WriteLine($"📥 SaleUpdated: {msg.Id}");
    await Task.CompletedTask;
});

await MessageConsumer.RegisterJsonConsumer<EventMessage>(bus, EventQueueName.SaleCancelled, async msg =>
{
    Console.WriteLine($"📥 SaleCancelled: {msg.Id}");
    await Task.CompletedTask;
});

await MessageConsumer.RegisterJsonConsumer<EventMessage>(bus, EventQueueName.ItemCancelled, async msg =>
{
    Console.WriteLine($"📥 ItemCancelled: {msg.Id}");
    await Task.CompletedTask;
});

await Task.Delay(Timeout.Infinite);