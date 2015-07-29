using System;
using Tundra.Exceptions;

namespace Tundra.Interfaces.Container
{
    /// <summary>
    /// Container Interface
    /// </summary>
    public interface IContainer
    {
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
        ///         IoC.Container.Register&lt;Person&gt;();
        ///     }
        /// }
        /// </code>
        /// </example>
        void Register<TClass>();

        /// <summary>
        /// Registers a specified service / interface againt a concrete class
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <exception cref="DuplicateRegistrationException">thrown when the service of specified type is already registered.</exception>
        /// <remarks>
        /// An instance of the class is not created in this method. It only associates a interface with a contrete class. 
        /// So when the interface is used, it will be injected with an instance of the concrete class
        /// </remarks>
        /// <example>
        /// This example shows how to register an instance using <see cref="Register{TClass}"/>.
        /// <code>
        /// public interface IPerson
        /// {
        ///     string Name { get; set; }
        /// }
        /// 
        /// public class Person : IPerson
        /// {
        ///     public string Name { get; set; }
        /// }
        ///
        /// class TestClass
        /// {
        ///     static int Program(string[] args)
        ///     {
        ///         IoC.Container.Register&lt;IPerson,Person&gt;();
        ///     }
        /// }
        /// </code>
        /// </example>
        void Register<TService, TClass>() where TClass : TService;

        /// <summary>
        /// Registers a service instance.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <exception cref="DuplicateRegistrationException">thrown when the service of specified type is already registered.</exception>
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
        ///         IoC.Container.RegisterInstance&lt;Person&gt;(new Person());
        ///     }
        /// }
        /// </code>
        /// </example>
        void RegisterInstance<TService>(TService instance);

        /// <summary>
        /// Resolves the instance based on the service type.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns>
        /// an instance of TService
        /// </returns>
        /// <example>
        /// This example shows how to resolve an instance using <see cref="Resolve{TService}"/>.
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
        ///         Person person = IoC.Container.Resolve&lt;Person&gt;();
        ///     }
        /// }
        /// </code>
        /// </example>
        TService Resolve<TService>();

        /// <summary>
        /// Resolves an instance based on the specified type.
        /// </summary>
        /// <param name="type">The type of the service.</param>
        /// <returns>
        /// an instance of the type specified
        /// </returns>
        /// <exception cref="System.NotSupportedException">No registration found for service of Type specified</exception>
        /// <example>
        /// This example shows how to resolve an instance of the specified type.
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
        ///         Person person = IoC.Container.Resolve(typeof(Person)) as Person;
        ///         if (person != null)
        ///         {
        ///             // use person object
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        object Resolve(Type type);
    }
}