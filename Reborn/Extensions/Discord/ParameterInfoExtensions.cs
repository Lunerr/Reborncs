using Discord.Commands;
using Reborn.Common;

namespace Reborn.Extensions.Discord
{
    public static class ParameterInfoExtensions
    {
        public static string Format(this ParameterInfo param)
            => Config.CAMEL_CASE.Replace(param.Name, " $1").ToLower();
    }
}
