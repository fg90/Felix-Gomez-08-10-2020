using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Albums.Models
{
    public class Album
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }

        public List<Photo> Photos { get; set; }
        public Album()
        {
            this.Photos = new List<Photo>() { };   
        }
    }
}
