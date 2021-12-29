using Fixture.Core.Models;
using System.Threading.Tasks;

namespace Fixture.Business
{
    public interface IEventBusiness
    {
        Task<object> GetResult(int versionID);
        Task<bool> UpdateMarket(Event newEvent);

        Task<Event> CreateEvent(Event newEvent);


    }
}
