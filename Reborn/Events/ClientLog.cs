using Discord;
using Microsoft.Extensions.DependencyInjection;
using Reborn.Entities;
using Reborn.Services;
using System;
using System.Threading.Tasks;

namespace Reborn.Events
{
    public sealed class ClientLog : Event
    {
        private readonly LoggingService _logger;

        public ClientLog(IServiceProvider provider) : base(provider)
        {
            _logger = provider.GetRequiredService<LoggingService>();

            _client.Log += OnLogAsync;
        }

        private Task OnLogAsync(LogMessage msg)
            => _taskService.TryRun(async () =>
            {
                await _logger.LogAsync(msg.Severity, msg.Source + ": " + (msg.Exception?.ToString() ?? msg.Message));
            });
    }
}
