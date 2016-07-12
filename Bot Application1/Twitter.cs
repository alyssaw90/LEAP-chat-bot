using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

// REST API
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

// STREAM API
using Tweetinvi.Streaming;
using Stream = Tweetinvi.Stream;

// Others
using Tweetinvi.Exceptions; // Handle Exceptions
using Tweetinvi.Core.Extensions; // Extension methods provided by Tweetinvi
using Tweetinvi.Models.DTO; // Data Transfer Objects for Serialization
using Tweetinvi.Json; // JSON static classes to get json from Twitter.

namespace Bot_Application1
{
    public class Twitter
    {
        public static async Task<string> GetStockPriceAsync(string symbol)
        {
            //TWITTER API CODE
            Auth.SetUserCredentials(TwitterKeys.CONSUMER_KEY, TwitterKeys.CONSUMER_SECRET, TwitterKeys.ACCESS_TOKEN, TwitterKeys.ACCESS_SECRET_TOKEN);
            TweetinviEvents.QueryBeforeExecute += (sender, args) =>
            {
                Console.WriteLine(args.QueryURL);
            };
        
            var authenticatedUser = User.GetAuthenticatedUser();

            if (string.IsNullOrWhiteSpace(symbol))
                return null;

            var searchParameters = new SearchTweetsParameters(symbol)
            {
                MaximumNumberOfResults = 2
            };

            IEnumerable<ITweet> tweets = await SearchAsync.SearchTweets(searchParameters);
            ITweet a = tweets.ToArray<ITweet>()[0];
            Console.WriteLine(tweets);
            return a.FullText;
            

            //CODE FROM EXAMPLE
            /*
            if (string.IsNullOrWhiteSpace(symbol))
                return null;

            string url = $"http://finance.yahoo.com/d/quotes.csv?s={symbol}&f=sl1";
            string csv;
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                csv = await client.DownloadStringTaskAsync(url).ConfigureAwait(false);
            }
            string line = csv.Split('\n')[0];
            string price = line.Split(',')[1];

            double result;
            if (double.TryParse(price, out result))
                return result;

            return null;
            */
        }
    }
}