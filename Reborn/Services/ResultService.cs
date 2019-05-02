using Discord;
using Discord.Commands;
using Discord.Net;
using Reborn.Common;
using Reborn.Entities;
using Reborn.Extensions.Discord;
using Reborn.Extensions.System;
using System;
using System.Threading.Tasks;

namespace Reborn.Services
{
    public sealed class ResultService : Service
    {
        private readonly LoggingService _logger;
        private readonly CommandService _commands;

        public ResultService(LoggingService logger, CommandService commands)
        {
            _logger = logger;
            _commands = commands;
        }

        public Task HandleResultAsync(Context ctx, IResult result, int argPos)
        {
            if (result.IsSuccess)
                return Task.CompletedTask;

            var message = result.ErrorReason;

            switch (result.Error)
            {
                case CommandError.ParseFailed:
                    message = "You have provided an invalid number.";
                    break;
                case CommandError.Exception:
                    return HandleExceptionAsync(ctx, ((ExecuteResult)result).Exception);
                case CommandError.BadArgCount:
                    var cmd = _commands.GetCommand(ctx, argPos);

                    message = $"You are incorrectly using this command.\n" +
                              $"**Usage:** `{Config.PREFIX}{cmd.GetUsage()}`\n" +
                              $"**Example:** `{Config.PREFIX}{cmd.GetExample()}`";
                    break;
            }

            return ctx.ReplyErrorAsync(message);
        }

        public async Task HandleExceptionAsync(Context ctx, Exception ex)
        {
            var last = ex.Last();
            var message = last.Message;

            if (last is HttpException httpEx)
            {
                if (!Config.DISCORD_CODES.TryGetValue(httpEx.DiscordCode.GetValueOrDefault(), out message) &&
                    !Config.HTTP_CODES.TryGetValue(httpEx.HttpCode, out message))
                {
                    message = last.Message;
                }
            }
            else
            {
                await _logger.LogAsync(LogSeverity.Error, $"{ex}");
            }

            await ctx.ReplyErrorAsync(message);
        }
    }
}
