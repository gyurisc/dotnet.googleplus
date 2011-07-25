using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace GooglePlus
{
    public class GooglePlusService
    {
        private const string getProfileUrl = "https://plus.google.com/_/profiles/get/{0}";
        private const string getFollowingUrl = "https://plus.google.com/_/socialgraph/lookup/visible/?o=%5Bnull%2Cnull%2C%22{0}%22%5D"; 
       
        public Person GetPerson(string id)
        {
            string requestUri = string.Format(getProfileUrl, id);
            string json = GoogleUtils.FetchGoogleJSON(requestUri);

            JArray jsonArray = JArray.Parse(json);

            // Parse person 
            Person p = new Person();
            p.ID = GoogleUtils.getFromArray(jsonArray, 1, 0).Value<string>();
            p.FirstName = GoogleUtils.getFromArray(jsonArray, 1, 2, 4, 1).Value<string>();
            p.LastName = GoogleUtils.getFromArray(jsonArray, 1, 2, 4, 2).Value<string>();
            p.PictureUrl = GoogleUtils.getFromArray(jsonArray, 1, 2, 3).Value<string>();
            p.ProfileUrl = GoogleUtils.getFromArray(jsonArray, 1, 2, 2).Value<string>();
            p.Introduction = GoogleUtils.getFromArray(jsonArray, 1, 2, 14, 1).Value<string>();
            p.DataFetched = DateTime.Now; 
	
            return p;
        }

        /// <summary>
        /// Returns the followed ids. 
        /// </summary>
        /// <param name="id">google plus user id</param>
        /// <returns>list of ids that is followed by id</returns>
        public List<string> GetFollowedIDs(string id)
        {
            List<string> result = new List<string>();
            string requestUri = string.Format(getFollowingUrl, id);
            string json = GoogleUtils.FetchGoogleJSON(requestUri);

            JArray jsonArray = JArray.Parse(json);
            JArray peopleRefs = GoogleUtils.getFromArray(jsonArray, 2).Value<JArray>();

            for (int i = 0; i < peopleRefs.Count; i++)
            {
                JArray personArray = (JArray)peopleRefs[i];
                string followed_id = GoogleUtils.getFromArray(personArray, 0, 2).Value<string>();

                result.Add(followed_id);
            }

            return result;
        }

        public List<Post> GetPosts(string id)
        {
            throw new NotImplementedException();
        }
    }
}
