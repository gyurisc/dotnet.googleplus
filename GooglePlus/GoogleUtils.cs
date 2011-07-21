using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace GooglePlus
{
    public class GoogleUtils
    {
        public static string FetchGoogleJSON(string uri)
        {
            string content = string.Empty; 
            HttpWebRequest channel = (HttpWebRequest)WebRequest.Create(uri);

            using (var response = (HttpWebResponse)channel.GetResponse())
            {
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    content = sr.ReadToEnd();
                }
            }

            // Sleep some
            Thread.Sleep(200);

            // Removign the garbage from the beginning
            return CleanupGoogleJSON(content);
        }

        /// <summary>
        /// Cleaning up the JSON provided by Google Plus.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        /// <remarks>
        /// Source: https://github.com/jmstriegel/php.googleplusapi/blob/master/lib/GooglePlus/GoogleUtil.php
        /// </remarks>
        public static string CleanupGoogleJSON(string json)
        {
            string tmp = json; 

            // Removing the garbage from the beginning
            if (tmp.Length > 5)
            {
                tmp = tmp.Substring(5);
            }

            char c, lastchar = ' ';
            bool instring = false;
            bool inescape = false;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < tmp.Length; i++)
            {
                c = tmp[i]; 

                if (Char.IsWhiteSpace(c) && !instring)
                    continue;

                // handle strings
                if (instring)
                {
                    if (inescape)
                    {
                        sb.Append(c);
                        inescape = false;
                    }
                    else if(c == '\\')
                    {
                        sb.Append(c);
                        inescape = true; 
                    }
                    else if (c == '"')
                    {
                        sb.Append(c);
                        instring = false;
                    }
                    else 
                    {
                        sb.Append(c);
                    }

                    lastchar = c;
                    continue; 
                }

                switch (c)
                {
                    case '"':
                            sb.Append(c);
                            instring = true;
                        break;
                    case ',':
                        if (lastchar == ',' || lastchar == '[' || lastchar == '{')
                        {
                            sb.Append("null");
                        }

                        sb.Append(c);
                        break;

                    case ']':
                    case '}':
                        if (lastchar == ',')
                        {
                            sb.Append("null");
                        }

                        sb.Append(c);
                        break;
                    default:
                        sb.Append(c);
                        break;
                }

                lastchar = c; 
            }

            return sb.ToString();
        }

        internal static JToken getFromArray(JArray jsonArray, params int[] indexes)
        {
            int i = indexes[0];
            JToken jtok = jsonArray[i];

            if (jtok.Type == JTokenType.Array)
            {
                if (indexes.Length == 1)
                {
                    return jtok;
                }

                int[] nextIndexes = new int[indexes.Length-1];
                Array.Copy(indexes, 1, nextIndexes, 0, nextIndexes.Length);

                return getFromArray((JArray)jtok, nextIndexes);
            }
           
            return jtok;
        }
    }
}
