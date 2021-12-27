using System.Text.Json;
using System.Threading.Tasks;

namespace Fixture.Core.Utils
{
    public static class DataConversion
    {

        public static Task<T> JsonToEntity<T>(JsonElement json)
        {
            return Task.FromResult(JsonSerializer.Deserialize<T>(json.GetRawText()));
        }
    }
}
