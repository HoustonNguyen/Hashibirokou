using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Translation.V2;
using TwitterCommunications.Exceptions;
using TwitterBroadcastSystemModel.Extensions;

namespace TwitterCommunications
{
    public class Module
    {
        private static string CONSUMER_KEY = null;
        private static string CONSUMER_SECRET = null;
        private TwitterContext _TwitterContext = null;

        public Module() 
        {
            CONSUMER_KEY = Environment.GetEnvironmentVariable("TWITTERDEV_CONSUMERKEY", EnvironmentVariableTarget.User).DecodeFromBase64();
            CONSUMER_SECRET = Environment.GetEnvironmentVariable("TWITTERDEV_CONSUMERKEYSECRET", EnvironmentVariableTarget.User).DecodeFromBase64();

            if (string.IsNullOrWhiteSpace(CONSUMER_KEY) || string.IsNullOrWhiteSpace(CONSUMER_SECRET))
            {
                throw new Exception("Failed to get Consumer Key and/or Consumer Secret.");
            }
        }

        private TwitterContext TwitterContext {
            get
            {
                if (this._TwitterContext == null)
                {
                    Task<TwitterContext> task = Task.Run<TwitterContext>(async () => await AuthenticateApplicationAsync());
                    this._TwitterContext = task.Result;
                }
                return this._TwitterContext;
            }
        }

        private async System.Threading.Tasks.Task<TwitterContext> AuthenticateApplicationAsync()
        {
            try
            {
                var authorizer = new ApplicationOnlyAuthorizer
                {
                    CredentialStore = new InMemoryCredentialStore
                    {
                        ConsumerKey = CONSUMER_KEY,
                        ConsumerSecret = CONSUMER_SECRET
                    }
                };
                //auth = DoApplicationOnlyAuth();

                await authorizer.AuthorizeAsync();

                var twitterCtx = new TwitterContext(authorizer);

                return twitterCtx;
            }
            catch (Exception)
            {
                //Write to log
                return null;
            }
        }

        public async Task<List<Status>> DoSearchAsync(string searchTerm)
        {
            Search searchResponse =
                await
                this.TwitterContext.Search.Where(search => 
                    search.Type == SearchType.Search
                    && search.Query == searchTerm
                    && search.Count == 10
                    && search.IncludeEntities == true
                    && search.TweetMode == TweetMode.Extended)
                //.Select(search)
                .SingleOrDefaultAsync();

            if (searchResponse?.Statuses != null)
            {
                return searchResponse.Statuses.ToList();
            }
            else
            {
                return new List<Status>();
            }
        }

        public List<Status> DoSearchOfUserSync(ulong userID, string searchTerm, ulong? sincePostID, bool shouldTranslate = false)
        {

            var rateLimits = GetRateLimitStatusSync();
            var rateLimitSearchUserTimeLine = rateLimits.Where(entry => entry.Key.Equals("statuses")).FirstOrDefault().Value.Where(value => value.Resource.Equals("/statuses/user_timeline")).FirstOrDefault();
            //Console.WriteLine($"The application has {rateLimitEntityUserSearch.Remaining} searches left out of {rateLimitEntityUserSearch.Limit}: {((decimal)rateLimitEntityUserSearch.Remaining / (decimal)rateLimitEntityUserSearch.Limit) * 100}% timeline searches available.");

            if (rateLimitSearchUserTimeLine.Remaining > 0)
            {
                Task<List<Status>> task = Task.Run<List<Status>>(async () => await DoSearchOfUserAsync(userID, searchTerm, sincePostID, shouldTranslate));
                task.Wait();
                return task.Result;
            }
            else
            {
                throw new RateLimitException();
            }
            
        }
        
        public async Task<List<Status>> DoSearchOfUserAsync(
            ulong userID, 
            string searchTerm, 
            ulong? sincePostID = 0, 
            bool shouldTranslate = false)
        {
            var tweets =
                await
                TwitterContext.Status.Where(tweet =>
                    tweet.Type == StatusType.User
                    && tweet.UserID == userID
                    && tweet.Count == 5
                    && (sincePostID.HasValue == false || tweet.StatusID > sincePostID.Value)
                    && tweet.IncludeEntities == true
                    && tweet.Truncated == false
                    && tweet.IncludeUserEntities == true
                    && (string.IsNullOrEmpty(searchTerm) == true || ((string.IsNullOrEmpty(tweet.Text) == false && tweet.Text.Contains(searchTerm)) || (string.IsNullOrEmpty(tweet.FullText) == false && tweet.FullText.Contains(searchTerm))))
                )
                .ToListAsync();

            if (tweets != null)
            {
                if (shouldTranslate == true)
                {
                    List<Status> resultTweets = new List<Status>();
                    foreach (var tweet in tweets)
                    {
                        //if (string.IsNullOrEmpty(tweet.FullText) == false)
                        //{
                        //    tweet.FullText = TranslateText(tweet.FullText);
                        //}
                        if (string.IsNullOrEmpty(tweet.Text) == false)
                        {
                            tweet.Text = TranslateText(tweet.Text);
                        }
                        resultTweets.Add(tweet);
                    }
                    return resultTweets;
                }
                else
                {
                    return tweets;
                }

            }
            else
            {
                return new List<Status>();
            }
        }

        public Dictionary<string, List<RateLimits>> GetRateLimitStatusSync()
        {
            Task<Dictionary<string, List<RateLimits>>> task = Task.Run(async () => await GetRateLimitStatusAsync());
            task.Wait();
            return task.Result;
        }

        public async Task<Dictionary<string, List<RateLimits>>> GetRateLimitStatusAsync()
        {
            var helpResponse =
                await
                    (from help in this.TwitterContext.Help
                     where help.Type == HelpType.RateLimits
                     select help)
                    .SingleOrDefaultAsync();

            if (helpResponse != null && helpResponse.RateLimits != null)
            {
                return helpResponse.RateLimits;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Translates a given text to a target language.
        /// </summary>
        /// <param name="text">The content to translate.</param>
        /// <param name="targetLanguage">Required. Target language code.</param>
        public string TranslateText(string text = "[TEXT_TO_TRANSLATE]", string targetLanguage = "en")
        {
            Google.Cloud.Translation.V2.TranslationClient translationServiceClient = Google.Cloud.Translation.V2.TranslationClient.Create();
            TranslationResult response = translationServiceClient.TranslateText(text, targetLanguage, "ja");
            return response.TranslatedText;
        }
    }
}
