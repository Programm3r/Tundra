using System;
using System.Diagnostics;
using Ninject;
using Ninject.Modules;
using Tundra.Interfaces.Data;
using Tundra.Models.Tables;
using Tundra.Providers;

namespace Tundra.Modules
{
    /// <summary>
    /// Data Access Module Class
    /// </summary>
    public class DataAccessModule : NinjectModule
    {
        #region DataAccessModule - Private Fields

        /// <summary>
        /// The db instance path
        /// </summary>
        private readonly string _path;

        #endregion

        /// <summary>
        /// Gets the data access provider.
        /// </summary>
        /// <value>
        /// The data access provider.
        /// </value>
        public IDataAccess DataAccessProvider { get; private set; }

        #region DataAccessModule - CTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="DataAccessModule"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public DataAccessModule(string path)
        {
            this._path = path;
        }

        #endregion

        #region DataAccessModule - Base Overrides

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            this.Kernel.Bind<IDataAccess>().ToConstant<DataAccessProvider>(new DataAccessProvider(this._path));

            this.DataAccessProvider = base.Kernel.Get<IDataAccess>();

            this.CreateDatabaseMappings();
        }

        #endregion

        #region DataAccessModule - Public Virtual Methods

        /// <summary>
        /// Creates the database mappings.
        /// </summary>
        public virtual void CreateDatabaseMappings()
        {
            try
            {
                // map the tables
                this.DataAccessProvider.Map<CacheTable, string>(x => x.Key);
                // initialize the data access provider
                this.DataAccessProvider.Initialize();
            }
            catch (InvalidOperationException invalid)
            {
                // Within the LexDB instance it will call this method: CheckNotSealed
                // Which will throw the following exception: InvalidOperationException("DbInstance is already initialized");
                // This exception is thrown when Map or Initialize is called on dbInstance
                // which means it has already been initialized
                Debug.WriteLine("{0}", invalid);
            }
        }

        #endregion
    }
}