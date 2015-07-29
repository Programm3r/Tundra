using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Tundra.Interfaces.Data;
using Tundra.Models.Tables;

namespace Tundra.Providers
{
    /// <summary>
    /// Base Cache Provider Class
    /// </summary>
    public abstract class BaseCacheProvider
    {
        #region BaseCacheProvider - Properties

        /// <summary>
        /// Gets the data store.
        /// </summary>
        /// <value>
        /// The data store.
        /// </value>
        public IDataAccess DataStore { get; private set; }

        #endregion

        #region BaseCacheProvider - CTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCacheProvider" /> class.
        /// </summary>
        /// <param name="dataStore">The data store.</param>
        /// <exception cref="System.ArgumentNullException">dataStore;There aren't any bindings associated with the interface IDataAccess. Provide a binding in the IKernel instance.</exception>
        protected BaseCacheProvider(IDataAccess dataStore)
        {
            if (dataStore == null)
            {
                throw new ArgumentNullException("dataStore", "There aren't any bindings associated with the interface IDataAccess. Provide a binding in the IKernel instance.");
            }

            this.DataStore = dataStore;
        }

        #endregion

        #region BaseCacheProvider - Protected Methods

        /// <summary>
        /// Gets the cached data.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="keyName">Name of the key.</param>
        /// <returns></returns>
        /// <exception cref="System.NullReferenceException">DataStore is null. Make use of the appropriate base overload constructor and pass in an IDataAccess</exception>
        protected TResult GetCacheData<TResult>([CallerMemberName] string keyName = "")
        {
            if (this.DataStore == null)
            {
                throw new NullReferenceException("DataStore is null. Make use of the appropriate base overload constructor and pass in an IDataAccess");
            }

            var filteredEntry = this.DataStore.GetFilteredEntry<CacheTable>(f => f.Key == keyName);
            return filteredEntry != null ? JsonConvert.DeserializeObject<TResult>(filteredEntry.Value) : default(TResult);
        }

        /// <summary>
        /// Stores the cache data.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <exception cref="System.NullReferenceException">DataStore is null. Make use of the appropriate base overload constructor and pass in an IDataAccess</exception>
        protected void StoreCacheData<TValue>(TValue value, CacheTable.CacheLifetime lifetime, [CallerMemberName] string keyName = "")
        {
            if (this.DataStore == null)
            {
                throw new NullReferenceException("DataStore is null. Make use of the appropriate base overload constructor and pass in an IDataAccess");
            }

            this.DataStore.InsertOrUpdateTable(new CacheTable
            {
                Key = keyName,
                Value = JsonConvert.SerializeObject(value),
                Lifetime = lifetime
            });
        }

        /// <summary>
        /// Clears the cache data based on the filter provided.
        /// </summary>
        /// <param name="lifetimeFilter">The lifetime filter.</param>
        protected void ClearCacheData(Func<CacheTable, bool> lifetimeFilter)
        {
            if (this.DataStore != null)
            {
                this.DataStore.DeleteByFilter(lifetimeFilter);
            }
        }

        #endregion
    }
}