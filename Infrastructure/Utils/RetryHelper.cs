using System;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using Serilog;

namespace Infrastructure.Utils
{
    public static class RetryHelper
    {
        public static async Task<T> RetryAttempt<T>(Func<Task<T>> func, string functionName, CancellationToken cancellationToken)
        {
            return await Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (ex, span) => { Log.Error(ex, $"{functionName} failed after {span.TotalSeconds:n1}s"); })
                .ExecuteAsync(async () => await func());
        }
        // TODO add cancellation here https://chrismroberts.com/2020/01/06/cancelling-a-running-retry-policy-in-polly/
        
        public static async Task RetryAttempt(Func<Task> func, string functionName, CancellationToken cancellationToken)
        {
             await Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (ex, span) => { Log.Error(ex, $"{functionName} failed after {span.TotalSeconds:n1}s"); })
                .ExecuteAsync(async () => await func());
        }
        // TODO add cancellation here https://chrismroberts.com/2020/01/06/cancelling-a-running-retry-policy-in-polly/
    }
}