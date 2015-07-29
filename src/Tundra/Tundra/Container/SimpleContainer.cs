using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tundra.Exceptions;
using Tundra.Interfaces.Container;

namespace Tundra.Container
{
    /// <summary>
    /// Simple Container
    /// <para>
    /// The Simple Container class can be used with the singleton implementation of <see cref="Ioc"/>
    /// which will return an instance of <see cref="SimpleContainer"/>
    /// </para>
    /// </summary>
    public sealed class SimpleContainer : IContainer
    {
        /// <summary>
        /// Gets or sets the types.
        /// </summary>
        /// <value>
        /// The types.
        /// </value>
        private Dictionary<Type, Type> Types { get; set; }

        /// <summary>
        /// Gets or sets the instances.
        /// </summary>
        /// <value>
        /// The instances.
        /// </value>
        private Dictionary<Type, object> Instances { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleContainer" /> class.
        /// </summary>
        public SimpleContainer()
        {
            this.Types = new Dictionary<Type, Type>();
            this.Instances = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Registers and adds a type in the container.
        /// </summary>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <exception cref="DuplicateRegistrationException">thrown when an instance of a type is already registered.</exception>
        /// <remarks>No remarks specified</remarks>
        /// <example>
        /// This example shows how to register an instance using <see cref="Register{TClass}"/>.
        /// <code>
        /// public class Person
        /// {
        ///     public string Name { get; set; }
        /// }
        /// 
        /// class TestClass
        /// {
        ///     static int Program(string[] args)
        ///     {
        ///         Ioc.Container.Register&lt;Person&gt;();
        ///     }
        /// }
        /// </code>
        /// </example>
        public void Register<TClass>()
        {
            if (this.IsAlreadyRegistered<TClass>())
            {
                throw new DuplicateRegistrationException(string.Format("Service of Type '{0}' is already registered.", typeof (TClass).Name));
            }

            this.Types.Add(typeof (TClass), typeof (TClass));
        }

        /// <summary>
        /// Registers a specified service / interface againt a concrete class
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <exception cref="DuplicateRegistrationException"></exception>
        /// <exception cref="DuplicateRegistrationException"></exception>
        /// <remarks>
        /// An instance of the class is not created in this method. It only associates a interface with a contrete class.
        /// So when the interface is used, it will be injected with an instance of the concrete class
        /// </remarks>
        /// <example>
        /// This example shows how to register an instance using <see cref="Register{TClass}" />.
        /// <code>
        /// public interface IPerson
        /// {
        ///     string Name { get; set; }
        /// }
        /// public class Person : IPerson
        /// {
        ///     public string Name { get; set; }
        /// }
        /// class TestClass
        /// {
        ///     static int Program(string[] args)
        ///     {
        ///         Ioc.Container.Register&lt;IPerson,Person&gt;();
        ///     }
        /// }
        /// </code></example>
        public void Register<TService, TClass>() where TClass : TService
        {
            if (this.IsAlreadyRegistered<TService>())
            {
                throw new DuplicateRegistrationException(string.Format("Service of Type '{0}' is already registered.", typeof (TService).Name));
            }

            this.Types.Add(typeof (TService), typeof (TClass));
        }

        /// <summary>
        /// Registers a service instance.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <exception cref="DuplicateRegistrationException"></exception>
        /// <exception cref="DuplicateRegistrationException">thrown when the service of specified type is already registered.</exception>
        /// <remarks>
        /// No remarks specified
        /// </remarks>
        /// <example>
        /// This example shows how to register an instance using <see cref="Register{TClass}" />.
        /// <code>
        /// public class Person
        /// {
        ///     public string Name { get; set; }
        /// }
        /// class TestClass
        /// {
        ///     static int Program(string[] args)
        ///     {
        ///         Ioc.Container.RegisterInstance&lt;Person&gt;(new Person());
        ///     }
        /// }
        /// </code></example>
        public void RegisterInstance<TService>(TService instance)
        {
            if (this.IsAlreadyRegistered<TService>())
            {
                throw new DuplicateRegistrationException(string.Format("Service of Type '{0}' is already registered.", typeof (TService).Name));
            }

            this.Instances.Add(typeof (TService), instance);
        }

        /// <summary>
        /// Resolves the instance based on the service type.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns>
        /// an instance of TService
        /// </returns>
        public TService Resolve<TService>()
        {
            return (TService) this.Resolve(typeof (TService));
        }

        /// <summary>
        /// Resolves an instance based on the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// an instance of the type specified
        /// </returns>
        /// <exception cref="System.NotSupportedException">No registration found for service of Type specified</exception>
        public object Resolve(Type type)
        {
            if (!this.Types.ContainsKey(type))
            {
                if (!this.Instances.ContainsKey(type))
                {
                    throw new NotSupportedException(string.Format("No registration found for service of Type '{0}'.", type.Name));
                }
                return this.Instances[type];
            }

            var createdType = this.Types[type];

            var constructors = createdType.GetTypeInfo();
            ConstructorInfo mostSpecificConstructor = null;
            foreach (var constructor in constructors.DeclaredConstructors)
            {
                if (mostSpecificConstructor == null || mostSpecificConstructor.GetParameters().Length < constructor.GetParameters().Length)
                {
                    mostSpecificConstructor = constructor;
                }
            }

            var instance = Activator.CreateInstance(createdType, mostSpecificConstructor.GetParameters().Select(param => this.Resolve(param.ParameterType)).ToArray());
            return instance;
        }

        /// <summary>
        /// Determines whether the instance is already registered.
        /// </summary>
        /// <typeparam name="T">The type of service / interface or concrete class</typeparam>
        /// <returns>
        ///   <c>true</c> if the instance or type is already registered
        /// </returns>
        private bool IsAlreadyRegistered<T>()
        {
            return this.Instances.ContainsKey(typeof (T)) || this.Types.ContainsKey(typeof (T));
        }
    }
}