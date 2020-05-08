using DDDTalk.Dominio.Infra.EF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DDDTalk.API.Infraestrutura
{
    public class EscolaContextoEFSeed
    {
        public async Task SeedAsync(EscolaContextoEF context, IWebHostEnvironment env, IOptions<APISettings> settings, ILogger<EscolaContextoEFSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(EscolaContextoEFSeed));

            await policy.ExecuteAsync(async () =>
            {
                var contentRootPath = env.ContentRootPath;
                using (context)
                {
                    context.Database.Migrate();
                    
                    await context.SaveChangesAsync();
                }
            });
        }

        private AsyncRetryPolicy CreatePolicy(ILogger<EscolaContextoEFSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }
    }
}
