using System;
using System.ComponentModel;
using System.Timers;
using TwitterBroadcastSystemModel.Models;

namespace Hashibirokou.Task
{
    public class Handler
    {
        protected Timer _timer;
        protected BackgroundWorker _worker;
        protected TwitterBroadcastSystemModel.TBSEntities _TBSEntities;

        public Handler()
        {
            _TBSEntities = new TwitterBroadcastSystemModel.TBSEntities();
        }

        public void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_worker.IsBusy)
                _worker.RunWorkerAsync();
        }

        public void Stop()
        {
            _timer.Stop();
            _worker.CancelAsync();
        }

        public void WriteException(Exception ex, string location)
        {
            WriteLog(TwitterBroadcastSystemModel.Models.Log.LogLevels.Error, location, ex.Message, ex.InnerException?.Message);
        }

        public void WriteLog(TwitterBroadcastSystemModel.Models.Log.LogLevels level, string location, string message, string messageDetail)
        {
            _TBSEntities.Log.Add(new Log()
            {
                LogLevel = (int)level,
                LogDateTime = DateTime.UtcNow,
                LogMessage = message,
                LogMessageDetail = messageDetail
            });
            _TBSEntities.SaveChangesAsync();
        }
    }
}
