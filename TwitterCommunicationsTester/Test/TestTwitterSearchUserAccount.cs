using Discord.Webhook;
using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterCommunicationsTester.Test
{
    public class TestTwitterSearchUserAccount
    {
        public TestTwitterSearchUserAccount()
        {

        }

        public void RunTest ()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Starting Test");

            TwitterCommunications.Module module = new TwitterCommunications.Module();
            Console.WriteLine("Module Created");


            ulong iris_Official = 779723154;
            ulong iris_miyu = 2384783184;
            ulong iris_saki = 2384780834;
            string searchTerm = "ネットサイン会";
            Console.WriteLine($"Searching for \"{searchTerm}\"");
            Console.WriteLine($"================================================================================");
            var result = module.DoSearchOfUserSync(iris_saki, null, null, false);
            Console.WriteLine($"================================================================================");
            Console.WriteLine($"Found {result.Count} tweets");
            Console.WriteLine($"================================================================================");

            foreach (var tweet in result)
            {
                //Console.WriteLine($"Tweet by {tweet.User.Name}: " +
                //    $"{tweet.Text}. " +
                //    $"{((tweet.Entities.MediaEntities.Any()) ? ("Contains an image.") : (""))}");
                //Console.WriteLine($"================================================================================");

                string message = "";
                //message += "============================================================\n";
                message += "====================Auto Translate Test=====================\n";
                //message += $"{((tweet.Entities.MediaEntities.Any()) ? ("This tweet contains an image.\n") : (""))}";
                message += $"{((tweet.Text?.Contains("ネットサイン会") ?? false) || (tweet.FullText?.ToUpper().Contains("NETSIGN") ?? false) ? "**NETSIGN ALERT**\n" : "")}";
                message += $"{((tweet.Text?.Contains("抽選") ?? false) || (tweet.FullText?.Contains("抽選") ?? false) ? "**The tweet mentions a lottery**\n" : "")}";
                //message += $"-The following was Auto-Translated by Google API-\n";

                foreach (string ling in tweet.Text.Split("\n"))
                {
                    message += $"> {ling}\n";
                }
                message += $"\n";

                message += $"Link: https://twitter.com/{tweet.User.ScreenNameResponse}/status/{tweet.StatusID}\n";
                //message += $"Tweeted at {tweet.CreatedAt} (Local to User)\n";//(Service is {TimeZoneInfo.Local.DisplayName})
                message += $"============================================================";

                DiscordCommunications.Module.MessageOptions messageParams = new DiscordCommunications.Module.MessageOptions()
                {
                    WebHookURL = "https://discordapp.com/api/webhooks/687875775084626059/tjVuIQqf4k_rIP5KXmE7dbuYWIJs7SdFPIgZ26O1YgCPocwY0NFYGPdBDAQsqYk7te7M",
                    Text = message,
                    Username = tweet.User.Name,
                    AvatarUrl = tweet.User.ProfileImageUrl
                };

                //DiscordCommunications.Module.SendMessage(messageParams);
            }

            Console.Write("Any key to end");
            var name = Console.ReadLine();
        }
    }
}
