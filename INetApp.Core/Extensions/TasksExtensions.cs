using System;
using System.Threading.Tasks;

namespace INetApp.Extensions
{
    /// <summary>
    /// Tasks.
    /// </summary>
    public static class Tasks
    {
        /// <summary>
        /// Runs the delay.
        /// </summary>
        /// <param name="miliseconds">Miliseconds.</param>
        /// <param name="runnable">Runnable.</param>
        public static void RunDelay(int miliseconds, Action runnable)
        {
            Task.Run(async () =>
            {
                await Task.Delay(miliseconds);
                runnable?.Invoke();
            });
        }
    }
}
