using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace GooglePlus
{
    public class Connection
    {
        [Key]
        public int Id { get; set; }
        public string FromID { get; set; }
        public string ToID { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public Connection()
        {
            DateCreated = DateTime.Now;
            DateUpdated = DateTime.Now; 
        }

        public Connection(string from_id, string to_id)
        {
            DateCreated = DateTime.Now;
            DateUpdated = DateTime.Now;

            FromID = from_id;
            ToID = to_id;
        }
    }
}
