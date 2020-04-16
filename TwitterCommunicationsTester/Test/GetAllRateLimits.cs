using Discord.Webhook;
using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterBroadcastSystemModel.Models;
using TwitterBroadcastSystemModel.Extensions;
using TwitterBroadcastSystemModel;

namespace TwitterCommunicationsTester.Test
{
    public class GetAllRateLimits
    {
        public GetAllRateLimits()
        {

        }

        public void RunTest ()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Starting Test");

            TwitterCommunications.Module module = new TwitterCommunications.Module();
            Console.WriteLine("Module Created");

            Task<Dictionary<string, List<RateLimits>>> task2 = Task.Run(async () => await module.GetRateLimitStatusAsync());
            task2.Wait();
            var result2 = task2.Result;

            foreach(var keyvaluepair in result2)
            {
                ProcedureCategory procedureCategory = new ProcedureCategory()
                {
                    Description = keyvaluepair.Key
                };

                foreach(var rateLimit in keyvaluepair.Value)
                {
                    Procedure procedure = new Procedure()
                    {
                        Description = rateLimit.Resource,
                        ProcedureCategory = procedureCategory,
                        RateLimit = new RateLimit()
                        {
                            LastChecked = DateTime.UtcNow,
                            Allowance = rateLimit.Limit,
                            Remaining = rateLimit.Remaining,
                            AllowanceReset = rateLimit.Reset.ConvertUnixToDateTime(),
                        }
                    };

                    using (var context = new TBSContext())
                    {
                        context.Procedure.Add(procedure);
                        context.SaveChanges();
                    }
                }

            }

            Console.Write("Any key to end");
            var name = Console.ReadLine();
        }
    }
}
