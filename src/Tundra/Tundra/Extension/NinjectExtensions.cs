using System.Linq;
using Ninject;

namespace Tundra.Extension
{
    /// <summary>
    /// Ninject Extension Class Declaration
    /// </summary>
    public static class NinjectExtensions
    {
        /// <summary>
        /// Binds the specified kernel.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <param name="kernel">The kernel.</param>
        public static void Bind<TService, TClass>(this IKernel kernel) where TClass : TService
        {
            if (!kernel.GetBindings(typeof(TService)).Any())
            {
                kernel.Bind<TService>().To<TClass>();
            }
        }

        /// <summary>
        /// Determines whether the ninject kernel has an active binding of the <see cref="TService" /> provided.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="kernel">The kernel.</param>
        /// <returns></returns>
        public static bool HasActiveBinding<TService>(this IKernel kernel)
        {
            return kernel.GetBindings(typeof (TService)).Any();
        }

        /// <summary>
        /// Indicates that the service should be bound to the specified implementation type.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <param name="kernel">The kernel.</param>
        public static void BindIfNull<TService, TClass>(this IKernel kernel) where TClass : TService
        {
            if (!kernel.GetBindings(typeof (TService)).Any())
            {
                kernel.Bind<TService>().To<TClass>();
            }
        }

        /// <summary>
        /// Binds the specified service to a constant class instance.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <param name="kernel">The kernel.</param>
        /// <param name="class">The class.</param>
        public static void BindToConstant<TService, TClass>(this IKernel kernel, TClass @class) where TClass : TService
        {
            if (!kernel.GetBindings(typeof (TService)).Any())
            {
                kernel.Bind<TService>().ToConstant(@class);
            }
        }

        /// <summary>
        /// Binds to constant singleton.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <param name="kernel">The kernel.</param>
        public static void BindToConstantSingleton<TService, TClass>(this IKernel kernel) where TClass : TService
        {
            if (!kernel.GetBindings(typeof (TService)).Any())
            {
                kernel.Bind<TService>().To<TClass>().InSingletonScope();
            }
        }

        /// <summary>
        /// Binds to self.
        /// </summary>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <param name="kernel">The kernel.</param>
        public static void BindToSelf<TClass>(this IKernel kernel) where TClass : class
        {
            if (!kernel.GetBindings(typeof (TClass)).Any())
            {
                kernel.Bind<TClass>().ToSelf();
            }
        }

        /// <summary>
        /// Binds to self singleton.
        /// </summary>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <param name="kernel">The kernel.</param>
        public static void BindToSelfSingleton<TClass>(this IKernel kernel) where TClass : class
        {
            if (!kernel.GetBindings(typeof (TClass)).Any())
            {
                kernel.Bind<TClass>().ToSelf().InSingletonScope();
            }
        }

        /// <summary>
        /// Binds to self constant.
        /// </summary>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <param name="kernel">The kernel.</param>
        /// <param name="class">The class.</param>
        public static void BindToSelfConstant<TClass>(this IKernel kernel, TClass @class) where TClass : class
        {
            if (!kernel.GetBindings(typeof (TClass)).Any())
            {
                kernel.Bind<TClass>().ToConstant(@class);
            }
        }

        /// <summary>
        /// Rebinds to self singleton.
        /// </summary>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <param name="kernel">The kernel.</param>
        public static void RebindToSelfSingleton<TClass>(this IKernel kernel) where TClass : class
        {
            kernel.Rebind<TClass>().ToSelf().InSingletonScope();
        }
    }
}