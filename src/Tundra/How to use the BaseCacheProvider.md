# BaseCacheProvider

## Introduction

The following describes what the purpose of the `BaseCacheProvider` is, as well as how to implement it. First some background as to why I've implemented this base class. One of the things I find myself doing on a regular bases is caching data. This caching of the data might be persisted during the course of the application or maybe just for a short while, like when a transaction is taking place. Either way, I was looking for an easier way to cache the data as well as retrieve it.

To start of, the `BaseCacheProvider` makes use of an interface called `IDataAccess`. This interface is also defined in Tundra. I've made use of Ninject to inject the IDataAccess implementation into `BaseCacheProvider`. The interface exposes various members for interacting with a local db. In this case, I'm making use of [Lex.Db](https://github.com/demigor/lex.db), which is a lightweight, super-fast, in-process database engine, completely written in Any CPU C#. 

So the question might be, how did the `IDataAccess `get a value? We'll the following explains how the scaffolding was put in place before the actual cache provider was used. I've made use of an implementation based of the `BootstrapperBase `as well as the `DataAccessModule `to perform the injection (both these classes are defined in Tundra):
+ BootstrapperBase:
 + A base implementation of a bootstrapper. More information can be found [here](https://github.com/Programm3r/Tundra/wiki/How-to-use:-BootstrapperBase).
+  DataAccessModule:
 + An implementation of a `NinjectModule` which contains mappings between data access providers. This removes unnecessary scaffolding. It also contains a virtual method called `CreateDatabaseMappings`, which can be used to define your own table mappings. 
```csharp
    public class MyDataAccessModule : DataAccessModule
    {
        public MyDataAccessModule(string path)
            : base(path)
        {
        }

        public override void CreateDatabaseMappings()
        {
            // the actual registration on IDataAccess occurred in the base class
            // thus, we can simple use the Get
            var dbInstance = base.Kernel.Get<IDataAccess>();
            // map which ever tables you require
            dbInstance.Map<CacheTable, string>(x => x.Key);
            dbInstance.Map<PersonModel, Guid>(x => x.Id);
            // initialize the data access provider
            dbInstance.Initialize();
        }
    }

    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected void Initialize()
        {
            // initialize the modules
            this.Modules.Add(new MyDataAccessModule("testDatabase"));
        }
    }
```
### BaseCacheProvider Members:
The `BaseCacheProvider `exposes two methods that forms the core of the entire caching provider. The signatures are as follows:

```csharp
// Please note that the attribute CallerMemberName is being used for the key name
// Thus the name of who ever calls this method, will be used as the key name in storing the cached data
void StoreCacheData<TValue>(TValue value, CacheTable.CacheLifetime lifetime, [CallerMemberName] string keyName = "");
```

The above method also takes in an enumeration of type `CacheLifetime`. This enumeration defines the lifetime of the cached value. The possible values are:
```csharp
        public enum CacheLifetime
        {
            ClearOnHome,
            ClearOnLogout,
            ClearOnExit,
            ClearOnStart,
            None
        }
```

Thus, when you define a cache value, you should also specify the lifetime duration. You'll be able to make use of the `ClearCache `method to eliminate cached data at various stages.

```csharp
// Please note that the attribute CallerMemberName is being used for the key name
// Thus the name of who ever calls this method, will be used as the key name when retrieving the cached data
TResult GetCacheData<TResult>([CallerMemberName] string keyName = "");
```

### Implementation:
Ok, so let's have a look at the base implementation and what is the proper way to use this provider. You'll notice that the `PersonData `property makes use of the base methods to store and retrieve the value. This also allows a unique key to be used when making use of the cache provider.
> I would suggest that you only make use of properties to get and set the values. Reason being, if you make use of methods, the `CallerMemberName `attribute will make use of the method name, and this might change from call to call, thus resulting in a null.

```csharp
    public class MyCacheProvider : BaseCacheProvider
    {
        public PersonModel PersonData
        {
            get
            {
                // GetCacheData will make use of PersonData as the key value
                return base.GetCacheData<PersonModel>();
            }
            set
            {
                // StoreCacheData will make use of PersonData as the key value and specify the lifetime 
                base.StoreCacheData(value, CacheTable.CacheLifetime.None);
            }
        }

        public MyCacheProvider(IDataAccess dataStore)
            : base(dataStore)
        {
            // I've made use of Ninject to inject the IDataAccess implementation into this class
        }
    }
```