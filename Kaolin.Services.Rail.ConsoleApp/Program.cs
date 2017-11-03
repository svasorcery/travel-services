using System;
using Microsoft.Extensions.DependencyInjection;

namespace Kaolin.Services.Rail.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddOptions()
                .AddInMemorySessionProvider()
                .Configure<PassRzdRu.Parser.Config>(config =>
                {
                    config.Polling = new PassRzdRu.Parser.Config.PollingConfig(60, 1000);
                })
                .Configure<PassRzdRu.RailClient.Config>(config =>
                {
                    config.Username = "your_login_here";
                    config.Password = "your_password_here";
                })
                .AddSingleton<PassRzdRu.Parser.PassRzdRuClient>()
                .AddTransient<Models.Rail.Abstractions.IRailClient, PassRzdRu.RailClient.PassRzdRuRailClient>();
        }
    }
}
