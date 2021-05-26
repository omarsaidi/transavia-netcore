using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Transavia
{
    public class QueryParams : Dictionary<string, string>
    {
        protected QueryParams() { }

        /// <summary>
        /// Initializes a new Param map with an initial key/value pair.
        /// </summary>
        /// <param name="key">the key for the parameter to send to the API</param>
        /// <param name="value">value the value for the given key</param>
        /// <returns>the Param object, allowing for convenient chaining</returns>
        public static QueryParams With(string key, string value)
        {
            return new QueryParams().And(key, value);
        }
        /// <summary>
        /// Adds another key/value pair to the Params map.
        /// </summary>
        /// <param name="key">the key for the parameter to send to the API</param>
        /// <param name="value">value the value for the given key</param>
        /// <returns>the Param object, allowing for convenient chaining</returns>
        public QueryParams And(string key, string value)
        {
            this[key] = value;
            return this;
        }
        /// <summary>
        /// Converts params into a HTTP query string.
        /// </summary>
        /// <returns></returns>
        public string ToQueryString()
        {
            StringBuilder query = new StringBuilder();
            bool first = true;
            foreach (KeyValuePair<string, string> entry in this)
            {
                if (first)
                    query.Append("?");
                else
                    query.Append("&");
                first = false;

                query.Append(WebUtility.UrlEncode(entry.Key));
                query.Append("=");
                query.Append(WebUtility.UrlEncode(entry.Value));
            }

            return query.ToString();
        }
    }
}
