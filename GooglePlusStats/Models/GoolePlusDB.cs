using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using GooglePlus; 

namespace GooglePlusStats.Models
{
    public class GooglePlusDB : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Connection> Connections { get; set; }
    }
}