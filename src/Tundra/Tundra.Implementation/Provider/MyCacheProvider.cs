using Tundra.Implementation.Model;
using Tundra.Implementation.Provider.Interfaces;
using Tundra.Interfaces.Data;
using Tundra.Models.Tables;
using Tundra.Providers;

namespace Tundra.Implementation.Provider
{
    public class MyCacheProvider : BaseCacheProvider, IMyCacheProvider
    {
        public PersonModel PersonData
        {
            get
            {
                return base.GetCacheData<PersonModel>();
            }
            set
            {
                base.StoreCacheData(value, CacheTable.CacheLifetime.None);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyCacheProvider"/> class.
        /// </summary>
        /// <param name="dataStore">The data store.</param>
        public MyCacheProvider(IDataAccess dataStore)
            : base(dataStore)
        {            
        }
    }
}