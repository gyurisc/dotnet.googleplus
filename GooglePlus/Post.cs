using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace GooglePlus
{
    public class Post
    {
        [Key]
        public string ID { get; set; }        
        public string Subject { get; set; }
        public string Author { get; set; }
        public string AuthorId { get; set; }
        public string Source { get; set; }
        
        public DateTime DataCreated { get; set; }
        public DateTime DataUpdated { get; set; }
    }
}
