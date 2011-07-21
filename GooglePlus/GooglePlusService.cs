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
        private const string getUrlProfile = "https://plus.google.com/_/profiles/get/{0}";
       
        public Person GetPerson(string id)
        {
            string requestUri = string.Format(getUrlProfile, id);
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

        public List<Person> GetFollowing(string id)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetPosts(string id)
        {
            throw new NotImplementedException();
        }
    }
}
