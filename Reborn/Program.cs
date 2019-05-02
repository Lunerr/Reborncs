using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Reborn.Common;
using Reborn.Utilities;
using Reborn.Utility;

namespace Reborn
{
    class Program
    {
        static Task Main(string[] args)
            => Start(args);

        static async Task Start(string[] args)
        {
            var parsedArgs = await Arguments.ParseAsync(args);
            var credsFileName = parsedArgs["credentials"];

            if (!File.Exists(credsFileName))
                await Arguments.TerminateAsync($"The {credsFileName} file does not exist.");

            var creds = JsonConvert.DeserializeObject<Credentials>(await File.ReadAllTextAsync(credsFileName), Config.JSON_SETTINGS);

            var client = new DiscordSocketClient(new DiscordSocketConfig{});

            var commands = new CommandService(new CommandServiceConfig
            {
                DefaultRunMode = RunMode.Sync,
                IgnoreExtraArgs = true
            });

            var rand = new ThreadLocal<Random>(() => new Random(Guid.NewGuid().GetHashCode()));

            var services = new ServiceCollection()
                .AddSingleton(creds)
                .AddSingleton(commands)
                .AddSingleton(rand)
                .AddSingleton(client);

            Loader.LoadServices(services);

            var provider = services.BuildServiceProvider();

            Loader.LoadEvents(provider);

            await commands.AddModulesAsync(Assembly.GetEntryAssembly(), provider);
            await client.LoginAsync(TokenType.Bot, creds.Token);
            await client.StartAsync();
            await Task.Delay(-1);
        }
    }
}
