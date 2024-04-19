using System.Collections.Generic;

namespace Api.Models
{
    public class ArtistAlbumBridge
    {
        public int AlbumId { get; set; }

        public Album Album { get; set;}

        public int ArtistId { get; set; }

        public Artists Artist { get; set; }

    }
}
