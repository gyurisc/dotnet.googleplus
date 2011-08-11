using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace GooglePlus
{
    public class GooglePlusDb : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Connection> Connections { get; set; }

        public GooglePlusDb(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }
    }
}
