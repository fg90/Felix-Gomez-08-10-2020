using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Albums.Models
{
    public class Photo
    {
        public int AlbumId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }

        public string Url { get; set; }

        public string ThumnailUrl { get; set; }

        public List<Comment> Comments { get; set; }

        public Photo()
        {
            this.Comments = new List<Comment>() { };
        }

    }
}
