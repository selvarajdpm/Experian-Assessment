using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experian.API.Helpers
{
    public class Configuration
    {
        public Url Url { get; set; }
    }

    public class Url
    {
        public string PhotoAlbumBase { get; set; }
        public string Photo { get; set; }
        public string Album { get; set; }
        public string AlbumByUser { get; set; }
    }
}
