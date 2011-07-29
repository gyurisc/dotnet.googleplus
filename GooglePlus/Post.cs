using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooglePlus
{
    public class Post
    {
        public string ID { get; set; }
        public string Subject { get; set; }
        public string Author { get; set; }
        public string Source { get; set; }

        public DateTime DataFetched { get; set; }
    }
}
