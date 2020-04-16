using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TwitterBroadcastSystemModel.Models;

namespace TwitterBroadcastSystemModel
{
    class TBSContextInitializer : CreateDatabaseIfNotExists<TBSContext>
    {
        protected override void Seed(TBSContext context)
        {
            context.PriorityLevel.Add(Constants.PriorityLevel.ONCEPER12HOURS);
            context.PriorityLevel.Add(Constants.PriorityLevel.ONCEPER15MINUTES);
            context.PriorityLevel.Add(Constants.PriorityLevel.ONCEPER15SECONDS);
            context.PriorityLevel.Add(Constants.PriorityLevel.ONCEPER1HOUR);
            context.PriorityLevel.Add(Constants.PriorityLevel.ONCEPER1MINUTE);
            context.PriorityLevel.Add(Constants.PriorityLevel.ONCEPER2HOURS);
            context.PriorityLevel.Add(Constants.PriorityLevel.ONCEPER2MINUTES);
            context.PriorityLevel.Add(Constants.PriorityLevel.ONCEPER30MINUTES);
            context.PriorityLevel.Add(Constants.PriorityLevel.ONCEPER30SECONDS);
            context.PriorityLevel.Add(Constants.PriorityLevel.ONCEPER3HOURS);
            context.PriorityLevel.Add(Constants.PriorityLevel.ONCEPER3MINUTES);
            context.PriorityLevel.Add(Constants.PriorityLevel.ONCEPER5MINUTES);
            context.PriorityLevel.Add(Constants.PriorityLevel.ONCEPER6HOURS);
            context.PriorityLevel.Add(Constants.PriorityLevel.ONCEPERDAY);

            IList<DestinationType> defaultDestinationType = new List<DestinationType>();

            defaultDestinationType.Add(new DestinationType() { Description = "Discord" });
            context.DestinationType.AddRange(defaultDestinationType);

            Models.Action action = new Models.Action()
            {
                PrimaryKey = Guid.NewGuid(),
                Description = "Check latest from @iRis_Official_",
                LastTimeChecked = Convert.ToDateTime("2020-03-13 04:39:37.013"),
                LastFoundPostID = "1238315309997502466",
                LastModified = DateTime.UtcNow,
                Active = false,
                PriorityLevel = Constants.PriorityLevel.ONCEPER30SECONDS,
                Destinations = new List<Destination>()
                {
                    new Destination()
                    {
                        DestinationType = defaultDestinationType.FirstOrDefault(),
                        Description = "TestURL - Hue's Server #test",
                        Webhook = "https://discordapp.com/api/webhooks/685263828027441266/eR05hV78N22tcZrvHDqqQ6DwhNvxodSOH6NgpEKcD2SWVXjJmN_c3U9yFtI51FfrRv6I"
                    }
                },
                Queries = new List<Query>()
                {
                    new Query()
                    {
                        //QueryType = Constants.QueryType.TIMELINESEARCH,
                        ShouldTranslate = true,
                        TargetedUsers = new List<QueryTargetUser>()
                        {
                            new QueryTargetUser()
                            {
                                User = new User()
                                {
                                    UserID = "779723154",
                                    UserName = "iris_official_"
                                }
                            }
                        }
                    }
                }
            };
            context.Action.Add(action);

            context.SaveChanges();
        }
    }
}