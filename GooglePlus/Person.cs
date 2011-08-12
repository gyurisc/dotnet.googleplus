using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GooglePlus
{
    public class Person
    {
        [Key]
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureUrl { get; set; }
        public string ProfileUrl { get; set; }
        public string Introduction { get; set; }
        public string SubHead { get; set; }
        public int FollowersCount { get; set; }
        public int FollowedByCount { get; set; }

        public DateTime DataCreated { get; set; }
        public DateTime DataUpdated { get; set; }
    }
}
