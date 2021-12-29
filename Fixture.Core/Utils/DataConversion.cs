using System.Text.Json;
using System.Threading.Tasks;

namespace Fixture.Core.Utils
{
    public static class DataConversion
    {
        /// <summary>
        /// Convert the json to entity model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static async Task<T> JsonToEntity<T>(JsonElement json)
        {
            return await Task.FromResult( JsonSerializer.Deserialize<T>(json.GetRawText()));
        }
    }
}
