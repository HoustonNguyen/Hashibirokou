using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using TwitterBroadcastSystemModel.Extensions;
using TwitterBroadcastSystemModel.Models;

namespace Hashibirokou.Task
{
    /*
     * Then, in order to cancel further execution simply call worker.CancelAsync(). 
     * Note that this is completely user-handled cancellation mechanism (it does not support thread aborting or anything like that out-of-the-box).
     */
    public class ActionHandler : Handler
    {
        private TwitterBroadcastSystemModel.Models.Action _Action;
        private TwitterCommunications.Module _CommsModule;

        public ActionHandler(Guid actionGUID)
        {
            //Get Action Entity
            var action = _TBSEntities.Action.FirstOrDefault(a => a.PrimaryKey == actionGUID);
            _Action = action;

            _worker = new BackgroundWorker()
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true
            };

            _CommsModule = new TwitterCommunications.Module();

            _worker.DoWork += Worker_DoWork;
            _worker.ProgressChanged += worker_ProgressChanged;
            _worker.RunWorkerCompleted += worker_RunWorkerCompleted;

            _timer = new System.Timers.Timer(_Action.PriorityLevel.Delay);
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
            WriteLog(Log.LogLevels.Information, $"ActionHandler-{actionGUID}", "ActionHandler Started", $"ActionHandler for action {action.Description} started at {DateTime.UtcNow}");
        }

        public void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            bool hasRun = false;
            while (hasRun == false)
            {
                //Check if cancellation was requested
                if (worker.CancellationPending)
                {
                    //take any necessary action upon cancelling (rollback, etc.)

                    //notify the RunWorkerCompleted event handler
                    //that the operation was cancelled
                    e.Cancel = true;
                    return;
                }

                _Action = _TBSEntities.Action.FirstOrDefault(action => action.PrimaryKey == _Action.PrimaryKey);
                //report progress; this method has an overload which can also take
                //custom object (usually representing state) as an argument
                //worker.ReportProgress(/*percentage*/);

                if(_Action.Active == false)
                {
                    e.Cancel = true;
                    return;
                }

                //Action
                if (_Action.Destinations == null)
                {
                    //There is no destination, therefore, skip
                    return;
                }

                List<Status> tweets = new List<Status>();
                try
                {
                    ulong userID = (ulong)_Action.Queries.FirstOrDefault()?.TargetedUsers.FirstOrDefault()?.User.UserID.ToInt64();
                    tweets = _CommsModule.DoSearchOfUserSync(
                        userID, 
                        null, 
                        _Action.LastFoundPostID.ToInt64(), true);
                    //WriteLog(Log.LogLevels.Information, $"ActionHandler-{_Action.PrimaryKey}", "ActionHandler Run", $"ActionHandler for action {_Action.Description} ran at {DateTime.UtcNow}");

                }
                catch (TwitterCommunications.Exceptions.RateLimitException ex)
                {
                    WriteException(ex, "");
                    Thread.Sleep(900000);
                    return;
                }
                catch(Exception ex)
                {
                    //Log
                    WriteException(ex, $"ActionHandler - {_Action.PrimaryKey.ToString()}:{_Action.Description}");
                    return;
                }

                foreach (var tweet in tweets)
                {
                    string message = parseMessage(tweet);

                    foreach (Destination destination in _Action.Destinations)
                    {
                        DiscordCommunications.Module.MessageOptions messageParams = new DiscordCommunications.Module.MessageOptions()
                        {
                            WebHookURL = destination.Webhook,
                            Text = message,
                            Username = tweet.User.Name,
                            AvatarUrl = tweet.User.ProfileImageUrl
                        };

                        DiscordCommunications.Module.SendMessage(messageParams);
                    }
                }

                if (tweets.Any() == true)
                {
                    string latestTweetID = tweets.Max(tweet => tweet.StatusID).ToString();
                    _Action.LastFoundPostID = latestTweetID.ToString();
                }
               
                _Action.LastTimeChecked = DateTime.UtcNow;
                _TBSEntities.SaveChanges();
                hasRun = true;
            }
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //display the progress using e.ProgressPercentage and/or e.UserState
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                //Thread.CurrentThread.Abort();
                //do something
            }
            else
            {
                //do something else
            }
        }

        private string parseMessage(Status tweet)
        {
            string message = "";
            //message += "============================================================\n";
            message += "====================Auto Translate Test=====================\n";
            //message += $"{((tweet.Entities.MediaEntities.Any()) ? ("This tweet contains an image.\n") : (""))}";
            message += $"{((tweet.Text?.Contains("ネットサイン会") ?? false) || (tweet.FullText?.ToUpper().Contains("NETSIGN") ?? false) ? "**NETSIGN ALERT**\n" : "")}";
            message += $"{((tweet.Text?.Contains("抽選") ?? false) || (tweet.FullText?.Contains("抽選") ?? false) ? "**The tweet mentions a lottery**\n" : "")}";
            //message += $"-The following was Auto-Translated by Google API-\n";

            foreach (string ling in tweet.Text.Split('\n'))
            {
                message += $"> {ling}\n";
            }
            message += $"\n";

            //message += $"Link: https://twitter.com/{tweet.User.ScreenNameResponse}/status/{tweet.StatusID}\n";
            //message += $"Tweeted at {tweet.CreatedAt} (Local to User)\n";//(Service is {TimeZoneInfo.Local.DisplayName})
            message += $"============================================================";

            return message;
        }
    }
}
