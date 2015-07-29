using Tundra.Implementation.Model;

namespace Tundra.Implementation.Provider.Interfaces
{
    public interface IMyCacheProvider
    {
        PersonModel PersonData { get; set; }
    }
}