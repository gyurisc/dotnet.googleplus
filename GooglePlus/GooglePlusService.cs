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
        private const string getFollowerUrl = "https://plus.google.com/_/socialgraph/lookup/incoming/?o=%5Bnull%2Cnull%2C%22{0}%22%5D&n=1000";
        private const string getPostsUrl = "https://plus.google.com/_/stream/getactivities/{0}/?sp=%5B1%2C2%2C%22{0}%22%2Cnull%2Cnull%2Cnull%2Cnull%2C%22social.google.com%22%2C%5B%5D%5D&rt=j";
       
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

        private List<string> ParseListOfUserRefs(JArray jsonArray)
        {
            List<string> result = new List<string>();
            JArray peopleRefs = GoogleUtils.getFromArray(jsonArray, 2).Value<JArray>();

            for (int i = 0; i < peopleRefs.Count; i++)
            {
                JArray personArray = (JArray)peopleRefs[i];
                string followed_id = GoogleUtils.getFromArray(personArray, 0, 2).Value<string>();

                result.Add(followed_id);
            }

            return result;
        }

        private List<Post> ParseListOfPosts(JArray jsonArray)
        {
            JArray postList = GoogleUtils.getFromArray(jsonArray, 0, 0, 1, 0).Value<JArray>();
            List<Post> results = new List<Post>();

            for (int i = 0; i < postList.Count; i++)
            {
                JArray post = (JArray)postList[i];
                Post p = ParsePost(post);

                results.Add(p);
            }

            return results; 
        }

        private Post ParsePost(JArray postJson)
        {
            Post post = new Post();

            post.ID = GoogleUtils.getFromArray(postJson, 21).Value<string>();
            post.Subject = GoogleUtils.getFromArray(postJson, 4).Value<string>();
            post.Author = GoogleUtils.getFromArray(postJson, 3).Value<string>();
            post.Source = GoogleUtils.getFromArray(postJson, 2).Value<string>();           
            
            post.DataFetched = DateTime.Now; 
            return post; 
        }

        /// <summary>
        /// Returns all ids that are followed by the input id. 
        /// </summary>
        /// <param name="id">google plus user id</param>
        /// <returns>list of ids that is followed by id</returns>
        public List<string> GetFollowedIDs(string id)
        {
            string requestUri = string.Format(getFollowingUrl, id);
            string json = GoogleUtils.FetchGoogleJSON(requestUri);

            JArray jsonArray = JArray.Parse(json);
            return ParseListOfUserRefs(jsonArray);
        }
    
        public List<string> GetFollowerIDs(string id)
        {
            List<string> result = new List<string>();
            string requestUri = string.Format(getFollowerUrl, id);
            string json = GoogleUtils.FetchGoogleJSON(requestUri);

            JArray jsonArray = JArray.Parse(json);

            return ParseListOfUserRefs(jsonArray);
        }

        public List<Post> GetPosts(string id)
        {
            List<Post> result;

            string requestUri = string.Format(getPostsUrl, id);
            string json = GoogleUtils.FetchGoogleJSON(requestUri);

            JArray jsonArray = JArray.Parse(json);
            result = ParseListOfPosts(jsonArray);

            return result; 
        }
    }
}
