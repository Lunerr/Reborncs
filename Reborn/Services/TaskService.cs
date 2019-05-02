using Discord;
using Reborn.Entities;
using System;
using System.Threading.Tasks;

namespace Reborn.Services
{
    public sealed class TaskService : Service
    {
        private readonly LoggingService _logger;

        public TaskService(LoggingService logger)
        {
            _logger = logger;
        }

        public Task TryRun(Func<Task> task)
        {
            new Action(async () =>
            {
                try
                {
                    await task();
                }
                catch (Exception ex)
                {
                    await _logger.LogAsync(LogSeverity.Error, ex.ToString());
                }
            })();

            return Task.CompletedTask;
        }
    }
}
