using Reborn.Common;
using System.Collections.Generic;
using System.Linq;

namespace Reborn.Extensions.System
{
    public static class StringExtensions
    {
        public static string UpperFirstChar(this string input)
            => input.Any() ? input.First().ToString().ToUpper() + input.Substring(1) : string.Empty;

        public static string Bold(this string input)
            => $"**{Config.MARKDOWN_REGEX.Replace(input.ToString(), "")}**";

        public static bool GetInclusions(this string input, IReadOnlyDictionary<string, InclusionCmd> values, out List<InclusionCmd> cmds)
        {
            var result = false;
            cmds = new List<InclusionCmd>();

            foreach (var item in values)
            {
                if (input.Contains(item.Key))
                {
                    cmds.Add(item.Value);

                    result = true;
                }
            }

            return result;
        }
    }
}
