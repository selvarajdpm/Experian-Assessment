using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Experian.API.Entities
{
    public class Album
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("photos")]
        public IEnumerable<Photo> Photos { get; set; }

    }
}
