using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GooglePlus;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace GooglePlusCrawler
{
    public class GooglePlusCrawler
    {
        private List<string> visitedIDList = new List<string>();
        private Queue<string> visitingIDList = new Queue<string>();

        private GooglePlusDb db;

        public GooglePlusCrawler(GooglePlusDb _db)
        {
            db = _db;
        }

        /// <summary>
        /// Crawls a person starting from the start id
        /// </summary>
        /// <param name="start_id"></param>
        /// <param name="counter"></param>
        public void CrawlPerson(string start_id, int numberOfPersons, bool checkConnections)
        {
            string current_id;

            GooglePlusService gpsvc = new GooglePlusService();

            InitializeVisitLists();

            Console.WriteLine("Lists are initialized visited:{0}, visiting:{1}.", visitedIDList.Count, visitingIDList.Count);

            if (visitingIDList.Count == 0)
            {
                // seed the queue with first id 
                visitingIDList.Enqueue(start_id);
            }

            while (numberOfPersons > 0)
            {
                if (visitingIDList.Count == 0)
                {
                    break;
                }

                current_id = visitingIDList.Dequeue();

                // did we see this user already
                if (visitedIDList.Contains(current_id))
                {
                    continue;
                }

                if (PersonInDb(current_id))
                {
                    if (!visitedIDList.Contains(current_id))
                    {
                        visitedIDList.Add(current_id);
                    }

                    continue;
                }
                else
                {
                    Stopwatch sp = new Stopwatch();
                    sp.Start();

                    Person p = gpsvc.GetPerson(current_id);

                    List<Connection> followed = gpsvc.GetFollowedConnections(current_id);
                    List<Connection> followers = gpsvc.GetFollowerConnections(current_id);

                    if (checkConnections)
                    {
                        p.FollowersCount = followers.Count;
                        p.FollowedByCount = followed.Count;
                    }


                    db.Persons.Add(p);
                    db.SaveChanges();

                    if (!visitedIDList.Contains(current_id))
                    {
                        visitedIDList.Add(current_id);
                    }

                    if (!checkConnections)
                    {
                        sp.Stop();
                        Console.WriteLine("{0}. {1} added. Followed By {2} users. Following {3} users. {4} ms", numberOfPersons, p.FirstName + " " + p.LastName, followers.Count, followed.Count, sp.ElapsedMilliseconds);
                        numberOfPersons--;
                        continue;
                    }

                    ProcessConnections(followed);
                    ProcessConnections(followers);

                    sp.Stop();
                    Console.WriteLine("{0}. {1} added. Followed By {2} users. Following {3} users. {4} ms", numberOfPersons, p.FirstName + " " + p.LastName, followers.Count, followed.Count, sp.ElapsedMilliseconds);
                    numberOfPersons--;

                }
            }
        }

        private void InitializeVisitLists()
        {
            List<Person> persons = db.Persons.ToList();

            foreach (Person p in persons)
            {
                visitedIDList.Add(p.ID);
            }

            List<Connection> connections = db.Connections.ToList();

            foreach (Connection connection in connections)
            {
                if (!visitedIDList.Contains(connection.ToID))
                {
                    visitingIDList.Enqueue(connection.ToID);
                }

                if (!visitedIDList.Contains(connection.FromID))
                {
                    visitingIDList.Enqueue(connection.FromID);
                }
            }
        }

        private void ProcessConnections(List<Connection> connections)
        {
            if (connections.Count < 1)
            {
                return;
            }

            foreach (Connection con in connections)
            {
                string id = con.ToID;
                RegisterID(id);

                if (!ConnectionExists(con))
                {
                    db.Connections.Add(con);
                }
            }

            db.SaveChanges();
        }

        private bool ConnectionExists(Connection con)
        {
            if (!visitingIDList.Contains(con.ToID))
            {
                return false;
            }

            if (visitedIDList.Contains(con.FromID))
            {
                return true;
            }

            if (visitingIDList.Contains(con.FromID))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Registers an id for further crawling if needed 
        /// </summary>
        /// <param name="id"></param>
        private void RegisterID(string id)
        {
            if (!visitedIDList.Contains(id))
            {
                visitingIDList.Enqueue(id);
            }
        }

        private bool PersonInDb(string plus_id)
        {
            Person person = db.Persons.Find(plus_id);
            return (person == null) ? false : true;
        }

        private bool ConnectionInDb(Connection connection)
        {
            int count = (from c in db.Connections
                         where (c.ToID == connection.ToID && c.FromID == connection.FromID)
                         select c).ToList().Count;

            return count == 0 ? false : true;
        }
    }
}
