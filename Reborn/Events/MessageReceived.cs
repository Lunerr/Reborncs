using Discord;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using Reborn.Common;
using Reborn.Entities;
using Reborn.Services;
using System;
using System.Threading.Tasks;
using System.Linq;
using Reborn.Extensions.System;
using System.Collections.Generic;

namespace Reborn.Events
{
    public sealed class MessageReceived : Event
    {
        private readonly CommandService _commandService;
        private readonly ResultService _resultService;
        private readonly LoggingService _logger;
        private readonly IServiceProvider _provider;

        public MessageReceived(IServiceProvider provider) : base(provider)
        {
            _provider = provider;
            _commandService = _provider.GetRequiredService<CommandService>();
            _resultService = _provider.GetRequiredService<ResultService>();
            _logger = _provider.GetRequiredService<LoggingService>();

            _client.MessageReceived += OnMessageReceivedAsync;
        }

        private Task OnMessageReceivedAsync(IMessage socketMsg)
            => _taskService.TryRun(async () =>
            {
                if (!(socketMsg is IUserMessage msg) || msg.Author.IsBot || msg.Embeds.Count > 0)
                    return;

                var ctx = new Context(_client, msg, _provider);

                if (msg.Content.GetInclusions(Config.INCLUSION_COMMANDS, out List<InclusionCmd> cmds))
                {
                    foreach (var cmd in cmds)
                    {
                        if (!string.IsNullOrWhiteSpace(cmd.path))
                            await msg.Channel.SendFileAsync(Config.DATA_DIRECTORY + cmd.path);
                        else if (!string.IsNullOrWhiteSpace(cmd.response))
                            await ctx.SendAsync(cmd.response);
                    }
                }

                int argPos = 0;

                if (!msg.HasCharPrefix(Config.PREFIX, ref argPos))
                {
                    return;
                }

                var result = await _commandService.ExecuteAsync(ctx, argPos, _provider);

                await _resultService.HandleResultAsync(ctx, result, argPos);
            });
    }
}
