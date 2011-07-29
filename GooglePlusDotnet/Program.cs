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
            string ME_ID = "116710411414066669240";
            string SCOBLE_ID = "111091089527727420853";

            GooglePlusService gpsvc = new GooglePlusService();

            Person me = gpsvc.GetPerson(ME_ID);

            Console.WriteLine(string.Format("Name: {0} {1}", me.FirstName, me.LastName));
            Console.WriteLine(string.Format("Profile: {0}", me.ProfileUrl));

            // Post by me 
            List<Post> posts = gpsvc.GetPosts(ME_ID);

            foreach (Post post in posts)
            {
                Console.WriteLine(post.Subject + " by " + post.Author + " (" + post.Source + ").");
            }

            // Users following me 
            List<string> followers = gpsvc.GetFollowerIDs(ME_ID);

            Console.WriteLine(me.FirstName + " is followed by " + followers.Count + " other google+ users.");
            foreach (string id in followers)
            {
                Person p = gpsvc.GetPerson(id);
                Console.Write(string.Format(" {0} {1} ", p.FirstName, p.LastName));
            }

            // Users I follow 
            List<string> followed = gpsvc.GetFollowedIDs(ME_ID);
            
            Console.WriteLine(me.FirstName + " follows " + followed.Count + " other google+ users.");
            foreach (string id in followed)
            {
                Person p = gpsvc.GetPerson(id);
                Console.WriteLine(string.Format(" {0} {1}, ", p.FirstName, p.LastName));
            }


            Console.WriteLine("Hit any key to finish!");
            Console.ReadKey();
        }

    }
}