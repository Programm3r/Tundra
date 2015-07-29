using System;
using Tundra.Implementation.Model;
using Tundra.Models.Tables;
using Tundra.Modules;

namespace Tundra.Implementation.Modules
{
    public class MyDataAccessModule : DataAccessModule
    {
        public MyDataAccessModule(string path)
            : base(path)
        {
        }

        public override void CreateDatabaseMappings()
        {
            // map the tables
            base.DataAccessProvider.Map<CacheTable, string>(x => x.Key);
            base.DataAccessProvider.Map<PersonModel, Guid>(x => x.Id);
            // initialize the data access provider
            base.DataAccessProvider.Initialize();
        }
    }
}