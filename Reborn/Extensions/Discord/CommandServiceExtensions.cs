﻿using Discord.Commands;

namespace Reborn.Extensions.Discord
{
    public static class CommandServiceExtensions
    {
        public static CommandInfo GetCommand(this CommandService commandService, ICommandContext context, int argPos)
            => commandService.Search(context, argPos).Commands[0].Command;
    }
}
