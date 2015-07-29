using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tundra.Interfaces.Provider
{
    /// <summary>
    /// Calendar Sync Interface
    /// </summary>
    public interface ICalendarProvider
    {
        /// <summary>
        /// Saves the appointments.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="bookingModels">The booking models.</param>
        void SaveAppointments<TModel>(IEnumerable<TModel> bookingModels) where TModel : class;
        /// <summary>
        /// Saves the appointments asynchronous.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="bookingModels">The booking models.</param>
        /// <returns></returns>
        Task SaveAppointmentsAsync<TModel>(IEnumerable<TModel> bookingModels) where TModel : class;
    }
}