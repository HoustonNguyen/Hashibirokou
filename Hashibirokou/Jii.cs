using Hashibirokou.Task;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using TwitterBroadcastSystemModel;

namespace Hashibirokou
{
    public class Jii : ServiceBase
    {
        //protected static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private List<Handler> _Threads;

        public Jii()
        {
            //Log.Info("Practica Service is initializing ........");
            _Threads = new List<Handler>();
        }

        protected override void OnStart(string[] args)
        {
            //Log.Info("Practica Service is starting ........");

            //Read in the actions
            using (TBSEntities tbsEntities = new TBSEntities())
            {
                var actionGUIDs = tbsEntities.Action.AsNoTracking().Where(action => action.Active == true).Select(action => action.PrimaryKey).ToList();

                foreach (Guid actionGUID in actionGUIDs)
                {
                    //Add a thread
                    _Threads.Add(new ActionHandler(actionGUID));
                }
            }
        }

        protected override void OnStop()
        {
            //Log.Info("Practica Service is stopping ........");

            foreach (var handler in _Threads)
            {
                handler.Stop();
            }
        }

        protected static bool ReadConfigAsBool(string Key, bool DefaultValue)
        {
            bool ret = false;
            string sValue = ConfigurationManager.AppSettings[Key].ToString();
            if (!bool.TryParse(sValue, out ret))
                return DefaultValue;
            return ret;
        }

        protected static int ReadConfigAsInt(string Key, int DefaultValue)
        {
            int ret = 0;
            try
            {
                string sValue = ConfigurationManager.AppSettings[Key].ToString();
                if (!int.TryParse(sValue, out ret))
                    return DefaultValue;

                return ret;
            }
            catch (Exception)
            {
                return DefaultValue;
            }
        }

        protected static string ReadConfigAsString(string Key, string DefaultValue)
        {
            string sValue = ConfigurationManager.AppSettings[Key].ToString();

            return sValue == null ? DefaultValue : sValue;
        }
    }
}
