using Discord.Commands;
using Reborn.Common;
using System.IO;
using System.Threading.Tasks;

namespace Reborn.Modules
{
    [Name("Memes")]
    [Summary("Yeet")]
    public class MemesModule : ModuleBase<Context>
    {
        [Command("order")]
        [Summary("i’ll have two number 9s, a number 9 large, a number 6 with extra dip, a number 7, two number 45s, one with cheese, and a large soda.")]
        public async Task Order()
        {
            await Context.Channel.SendFileAsync(Config.DATA_DIRECTORY + "order.png");
        }
    }
}
