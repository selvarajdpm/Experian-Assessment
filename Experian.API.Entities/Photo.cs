using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Experian.API.Entities
{
    public class Photo
    {
        [JsonPropertyName("albumId")]
        public int AlbumId { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("thumbnailUrl")]
        public string ThumbnailUrl { get; set; }
    }
}
