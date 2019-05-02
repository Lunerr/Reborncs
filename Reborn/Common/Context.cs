using Discord;
using Discord.Commands;
using Microsoft.Extensions.DependencyInjection;
using Reborn.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reborn.Common
{
    public sealed class Context : ICommandContext
    {
        private readonly SendingService _sender;

        public IDiscordClient Client { get; }
        public IGuild Guild { get; }
        public IMessageChannel Channel { get; }
        public ITextChannel TextChannel { get; }
        public IUser User { get; }
        public IGuildUser GuildUser { get; }
        public IUserMessage Message { get; }

        public Context(IDiscordClient client, IUserMessage msg, IServiceProvider provider)
        {
            _sender = provider.GetRequiredService<SendingService>();

            Client = client;
            Message = msg;
            Channel = msg.Channel;
            TextChannel = msg.Channel as ITextChannel;
            Guild = TextChannel?.Guild;
            User = msg.Author;
            GuildUser = User as IGuildUser;
        }

        public async Task<IUserMessage> DmAsync(string description, string title = null)
            => await _sender.SendAsync(await User.GetOrCreateDMChannelAsync(), description, title, guild: Guild);

        public Task<IUserMessage> SendFieldsAsync(IReadOnlyList<string> fieldOrValue, Color? color = null)
            => _sender.SendFieldsAsync(Channel, fieldOrValue, color);

        public Task<IUserMessage> SendFieldsErrorAsync(IReadOnlyList<string> fieldOrValue)
            => _sender.SendFieldsErrorAsync(Channel, fieldOrValue);

        public Task<IUserMessage> SendAsync(string description, string title = null, Color? color = null)
            => _sender.SendAsync(Channel, description, title, color);

        public Task<IUserMessage> ReplyAsync(string description, string title = null, Color? color = null)
            => _sender.ReplyAsync(User, Channel, description, title, color);

        public Task<IUserMessage> ReplyErrorAsync(string description)
            => _sender.ReplyErrorAsync(User, Channel, description);
    }
}
