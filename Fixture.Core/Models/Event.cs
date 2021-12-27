using System;
using System.Collections.Generic;
using System.Text.Json;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Fixture.Core.Models
{
    public class Event
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("version")]
        [Key]
        public int Version { get; set; }

        [JsonPropertyName("payload")]
        public JsonElement Payload { get; set; }
    }

    public class Market
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }
    }

    public class Sport
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Location
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Timing
    {
        [JsonPropertyName("scheduled_begin")]
        public DateTime ScheduledBegin { get; set; }

        [JsonPropertyName("expected_duration")]
        public string ExpectedDuration { get; set; }
    }

    public class Child
    {
        [JsonPropertyName("competitor_id")]
        public int CompetitorId { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Competitor
    {
        [JsonPropertyName("competitor_id")]
        public int CompetitorId { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("children")]
        public List<Child> Children { get; set; }
    }

    public class Metadata
    {
        [JsonPropertyName("sport")]
        public Sport Sport { get; set; }

        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("timing")]
        public Timing Timing { get; set; }

        [JsonPropertyName("competitors")]
        public List<Competitor> Competitors { get; set; }
    }

    public class Payload
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("markets")]
        public IEnumerable<Market> Markets { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("metadata")]
        public Metadata Metadata { get; set; }

    }

    public class Winner
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }




}


//namespace Fixture.Core.Models

//{
//    [Table("Events", Schema = "dbo")]
//    public class Event
//    {
//        public string type { get; set; }
//        [Key]
//        public int version { get; set; }
//        public JsonElement payload { get; set; }
//    }

//    public class Payload
//    {

//        public int id { get; set; }
//        public string name { get; set; }
//        public List<Market> markets { get; set; }

//        [NotMapped]
//        public Metadata metadata { get; set; }
//    }

//    public class Metadata
//    {
//        public Sport sport { get; set; }
//        public Location location { get; set; }
//        public Timing timing { get; set; }
//        public Competitor[] competitors { get; set; }
//    }

//    public class Sport
//    {
//        public int id { get; set; }
//        public string name { get; set; }
//    }

//    public class Location
//    {
//        public int id { get; set; }
//        public string type { get; set; }
//        public string name { get; set; }
//    }

//    public class Timing
//    {
//        public DateTime scheduled_begin { get; set; }
//        public string expected_duration { get; set; }
//    }

//    public class Competitor
//    {
//        public int competitor_id { get; set; }
//        public string type { get; set; }
//        public string name { get; set; }
//        public Child[] children { get; set; }
//    }

//    public class Child
//    {
//        public int competitor_id { get; set; }
//        public string type { get; set; }
//        public string name { get; set; }
//    }

//    public class Market
//    {
//        public int id { get; set; }
//        public string title { get; set; }
//        public float price { get; set; }
//    }

//}