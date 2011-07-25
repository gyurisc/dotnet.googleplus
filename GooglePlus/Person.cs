using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GooglePlus
{
    public class Person
    {        
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureUrl { get; set; }
        public string ProfileUrl { get; set; }
        public string Introduction { get; set; }
        public DateTime DataFetched { get; set; }
    }
}
