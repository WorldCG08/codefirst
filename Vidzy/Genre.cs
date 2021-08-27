using System.Collections.Generic;
using System.Security.AccessControl;

namespace Vidzy
{
    public class Genre
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public IList<Video> Videos { get; set; }
    }
}