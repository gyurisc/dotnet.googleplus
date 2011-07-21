using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooglePlus;

namespace GooglePlusDotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            string GPLUS_ID = "116710411414066669240";

            GooglePlusService gpsvc = new GooglePlusService();

            Person me = gpsvc.GetPerson(GPLUS_ID);

            Console.WriteLine(string.Format("Name: {0} {1}", me.FirstName, me.LastName));
            Console.WriteLine(string.Format("Profile: {0}", me.ProfileUrl));            
        }

    }
}