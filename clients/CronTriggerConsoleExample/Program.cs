using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("This is a simple example of how to use CronTrigger.");

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<MyCronTriggerService>();
    })
    .Build();

await host.RunAsync();
