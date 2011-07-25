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

            List<string> followed = gpsvc.GetFollowedIDs(GPLUS_ID);
            
            Console.WriteLine(me.FirstName + " follows " + followed.Count + " other google+ users.");
            foreach (string id in followed)
            {
                Person p = gpsvc.GetPerson(id);
                Console.WriteLine(string.Format("Name: {0} {1}", p.FirstName, p.LastName));
            }

            Console.WriteLine("Hit any key to finish!");
            Console.ReadKey();
        }

    }
}