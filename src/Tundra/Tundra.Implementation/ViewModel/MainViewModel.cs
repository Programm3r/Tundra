using System.Diagnostics;
using GalaSoft.MvvmLight.Command;
using Tundra.Implementation.Model;
using Tundra.Implementation.Provider.Interfaces;
using Tundra.Implementation.ViewModel.NavigationKeys;
using Tundra.Interfaces.Data;
using Tundra.Models.Tables;
using Tundra.MVVM.ViewModel;

namespace Tundra.Implementation.ViewModel
{
    public class MainViewModel : TundraBaseViewModel
    {
        private string _foo;
        private PersonModel _personModel;

        public PersonModel PersonModel
        {
            get
            {
                return this._personModel;
            }
            set
            {
                base.Set(ref this._personModel, value);
                this.Cache.PersonData = this._personModel;

                Debug.WriteLine("{0} {1}", this.Cache.PersonData.FullName, this.Cache.PersonData.Age);
            }
        }

        public IMyCacheProvider Cache { get; private set; }

        public string Foo
        {
            get
            {
                return _foo;
            }
            set
            {
                base.Set(ref this._foo, value);
            }
        }

        public RelayCommand PerformRegistration { get; private set; }

        public MainViewModel(IDataAccess data, IMyCacheProvider cache)
            : base(data)
        {
            Cache = cache;
            this.PersonModel = new PersonModel
            {
                Age = 35, FullName = "John Doe"
            };

            this.Foo = "this is some random string value";

            this.PerformRegistration = new RelayCommand(this.ExecutePerformRegistration);

            base.DataStore.InsertOrUpdateTable(new CacheTable
            {
                Key = "Foo", Lifetime = CacheTable.CacheLifetime.None, Value = SerializeJsonAsync(this.PersonModel).Result
            });

            var cacheTables = base.DataStore.GetAllEntries<CacheTable>();
            foreach (var cacheTable in cacheTables)
            {
                Debug.WriteLine(DeserializeJsonAsync<CacheTable>(cacheTable.Value).Result);
            }            
        }

        private void ExecutePerformRegistration()
        {
            this.NavigationService.NavigateTo(ViewModelPageKeys.RegistrationViewModelKey);
        }
    }
}