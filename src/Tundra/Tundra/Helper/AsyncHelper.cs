using System;
using System.Threading.Tasks;

namespace Tundra.Helper
{
    /// <summary>
    /// A-synchronize Helper Class
    /// </summary>
    public static class AsyncHelper
    {
        /// <summary>
        /// Loads the data in an asynchronous manner.
        /// </summary>
        /// <typeparam name="T">The specified type that is expected when callback is called as well as the loaded func.</typeparam>
        /// <param name="callback">The callback.</param>
        /// <param name="loader">The loader.</param>
        public static void LoadData<T>(Action<T> callback, Func<Task<T>> loader)
        {
            var task = loader();
            var awaiter = task.GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                if (task.Exception != null)
                {
                    throw task.Exception;
                }
                callback(task.Result);
            });
        }

        /// <summary>
        /// Loads the data in an asynchronous manner.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <param name="loader">The loader.</param>
        /// <example>
        /// <code>
        ///     class Foo
        ///     {
        ///         void Run()
        ///         {
        ///             AsyncHelper.LoadData(() =>
        ///             {
        ///                 // callback region
        ///             }, DoWork);
        ///         }
        ///
        ///         async Task DoWork()
        ///         {
        ///             await Task.Factory.StartNew(() =>
        ///             {
        ///                 // do some long running operation
        ///             });
        ///         }
        ///     }
        /// </code>
        /// </example>
        public static void LoadData(Action callback, Func<Task> loader)
        {
            var task = loader();
            var awaiter = task.GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                if (task.Exception != null)
                {
                    throw task.Exception;
                }
                callback();
            });
        }
    }
}