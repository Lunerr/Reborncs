using System;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Reborn.Services;

namespace Reborn.Entities
{
    public abstract class Event
    {
        protected readonly DiscordSocketClient _client;
        protected readonly TaskService _taskService;

        public Event(IServiceProvider provider)
        {
            _client = provider.GetRequiredService<DiscordSocketClient>();
            _taskService = provider.GetRequiredService<TaskService>();
        }
    }
}
