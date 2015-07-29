using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Tundra.Interfaces.Data
{
    /// <summary>
    /// Data Access Interface
    /// </summary>
    public interface IDataAccess : IDisposable
    {
        /// <summary>
        /// Maps the specified mapping function.
        /// </summary>
        /// <typeparam name="T">The table type</typeparam>
        /// <typeparam name="K">The type of the key selector in the table</typeparam>
        /// <param name="keyBuilder">The key builder.</param>
        void Map<T, K>(Expression<Func<T, K>> keyBuilder) where T : class, new();

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Deletes all data from the data table specified.
        /// </summary>
        /// <typeparam name="T">The type of the table object</typeparam>
        /// <returns>The number of records affected</returns>
        int DeleteAllData<T>() where T : class;

        /// <summary>
        /// Deletes the by filter.
        /// </summary>
        /// <typeparam name="T">The type of the table object</typeparam>
        /// <param name="filter">The filter.</param>
        /// <returns>
        /// The number of records affected
        /// </returns>
        int DeleteByFilter<T>(Func<T, bool> filter) where T : class;

        /// <summary>
        /// Gets all entries from the data table specified.
        /// </summary>
        /// <typeparam name="T">The type of the table object</typeparam>
        /// <returns></returns>
        IQueryable<T> GetAllEntries<T>() where T : class;

        /// <summary>
        /// Gets the filtered entries based on the filter expression specified.
        /// </summary>
        /// <typeparam name="T">The type of the table object</typeparam>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        IQueryable<T> GetFilteredEntries<T>(Func<T, bool> filter) where T : class;

        /// <summary>
        /// Gets the filtered entry based on the filter expression specified.
        /// </summary>
        /// <typeparam name="T">The type of the table object</typeparam>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        T GetFilteredEntry<T>(Func<T, bool> filter) where T : class;

        /// <summary>
        /// Inserts the or updates table.
        /// </summary>
        /// <typeparam name="T">The type of the table object</typeparam>
        /// <param name="dataEntry">The data entry.</param>
        void InsertOrUpdateTable<T>(T dataEntry) where T : class;

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
        Task<T> InsertOrUpdateTable<T, K>(T dataEntry, K key) where T : class;

        /// <summary>
        /// Saves all data to the data table specified.
        /// </summary>
        /// <typeparam name="T">The type of the table object</typeparam>
        /// <param name="items">The items.</param>
        void SaveAllData<T>(IEnumerable<T> items) where T : class;
    }
}