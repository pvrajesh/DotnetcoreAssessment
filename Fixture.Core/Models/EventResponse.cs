using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Fixture.Core.Models
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class Winner
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }

    public class ResponsePayload
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("winners")]
        public List<Winner> Winners { get; set; }
    }

    public class ResponseEvent
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("version")]
        public int Version { get; set; }

        [JsonPropertyName("payload")]
        public ResponsePayload Payload { get; set; }
    }


}
