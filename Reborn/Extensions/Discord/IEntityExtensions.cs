using Discord;
using Reborn.Extensions.System;

namespace Reborn.Extensions.Discord
{
    public static class IEntityExtensions
    {
        public static string Bold(this IEntity<ulong> entity)
            => entity.ToString().Bold();
    }
}
