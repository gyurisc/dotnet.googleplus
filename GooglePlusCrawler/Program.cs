using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooglePlus;

namespace GooglePlusCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            string seed_id = "116710411414066669240";            

            // string connection_db = @"Data Source=C:\Users\Cris\dev\dotnet.googleplus\GooglePlusCrawler\Data\GooglePlus.sdf";
            string connection_db = @"Data Source=t61\sqlexpress;Initial Catalog=GooglePlusStats.Models.GooglePlusDB;Integrated Security=True";
            GooglePlusDb db = new GooglePlusDb(connection_db);
            GooglePlusCrawler crawler = new GooglePlusCrawler(db);
            crawler.CrawlPerson(seed_id, 50, true);
        }
    }
}
