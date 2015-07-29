using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Lex.Db;
using Tundra.Interfaces.Data;

namespace Tundra.Providers
{
    /// <summary>
    /// Data Access Provider Class
    /// </summary>
    public class DataAccessProvider : IDataAccess, IDisposable
    {
        #region DataAccessProvider - Private Fields

        /// <summary>
        /// The DB instance
        /// </summary>
        private readonly DbInstance _db;

        #endregion

        #region DataAccessProvider - CTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="DataAccessProvider" /> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public DataAccessProvider(string path)
        {
            this._db = new DbInstance(path);           
        }

        #endregion

        #region IDataAccess Member Implementation

        /// <summary>
        /// Maps the specified mapping function.
        /// </summary>
        /// <typeparam name="T">The type of the table in the data source</typeparam>
        /// <typeparam name="K">The type of the key selector in the table</typeparam>
        /// <param name="keyBuilder">The key builder.</param>
        public void Map<T, K>(Expression<Func<T, K>> keyBuilder) where T : class, new()
        {
            this._db.Map<T>().Automap(keyBuilder);
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            this._db.Initialize();
        }

        /// <summary>
        /// Deletes all data from the data table specified.
        /// </summary>
        /// <typeparam name="T">The type of the table object</typeparam>
        /// <returns>The number of records affected</returns>
        public int DeleteAllData<T>() where T : class
        {
            var items = this.GetAllEntries<T>();
            return this._db.Table<T>().Delete(items);
        }

        /// <summary>
        /// Deletes the by filter.
        /// </summary>
        /// <typeparam name="T">The type of the table object</typeparam>
        /// <param name="filter">The filter.</param>
        /// <returns>
        /// The number of records affected
        /// </returns>
        public int DeleteByFilter<T>(Func<T, bool> filter) where T : class
        {
            var items = this.GetFilteredEntries(filter);
            return this._db.Table<T>().Delete(items);
        }

        /// <summary>
        /// Gets all entries.
        /// </summary>
        /// <typeparam name="T">The type of the table in the data source</typeparam>
        /// <returns></returns>
        public IQueryable<T> GetAllEntries<T>() where T : class
        {
            IQueryable<T> entryQueryable = this._db.Table<T>().Select(s => s).AsQueryable();
            return entryQueryable;
        }

        /// <summary>
        /// Gets the filtered entries.
        /// </summary>
        /// <typeparam name="T">The type of the table in the data source</typeparam>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public IQueryable<T> GetFilteredEntries<T>(Func<T, bool> filter) where T : class
        {
            var results = this._db.Table<T>().Where(filter).AsQueryable();
            return results;
        }

        /// <summary>
        /// Gets the filtered entry.
        /// </summary>
        /// <typeparam name="T">The type of the table in the data source</typeparam>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public T GetFilteredEntry<T>(Func<T, bool> filter) where T : class
        {
            return this._db.Table<T>().SingleOrDefault(filter);
        }

        /// <summary>
        /// Inserts the or update table.
        /// </summary>
        /// <typeparam name="T">The type of the table in the data source</typeparam>
        /// <param name="dataEntry">The data entry.</param>
        public void InsertOrUpdateTable<T>(T dataEntry) where T : class
        {
            this._db.Table<T>().Save(dataEntry);
        }

        /// <summary>
        /// Inserts the or updates table and returns the newly inserted item.
        /// </summary>
        /// <typeparam name="T">The type of the table object</typeparam>
        /// <typeparam name="K">The type of the primary key value</typeparam>
        /// <param name="dataEntry">The data entry.</param>
        /// <param name="key">The key.</param>
        /// <returns>
        /// an instance of table type specified; otherwise null if nothing could be found
        /// </returns>
        public Task<T> InsertOrUpdateTable<T, K>(T dataEntry, K key) where T : class
        {
            this._db.Table<T>().Save(dataEntry);
            return this._db.Table<T>().LoadByKeyAsync<T, K>(key);
        }

        /// <summary>
        /// Saves all data.
        /// </summary>
        /// <typeparam name="T">The type of the table in the data source</typeparam>
        /// <param name="items">The items.</param>
        public void SaveAllData<T>(IEnumerable<T> items) where T : class
        {
            foreach (var item in items)
            {
                this._db.Table<T>().Save(item);
            }
        }

        #endregion

        #region IDisposable Implementation

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="DataAccessProvider" /> class.
        /// </summary>
        ~DataAccessProvider()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                this._db.Dispose();
            }
            // free native resources if there are any.
        }

        #endregion
    }
}