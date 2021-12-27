using Fixture.Core.Models;
using System.Threading.Tasks;

namespace Fixture.Business
{
    public interface IEventBusiness
    {
        Task<object> GetResult();
        Task<bool> UpdateMarket(Event newEvent);

        Task<bool> CreateEvent(Event newEvent);


    }
}
