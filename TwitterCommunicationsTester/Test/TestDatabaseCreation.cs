using System;
using TwitterBroadcastSystemModel;

namespace TwitterCommunicationsTester.Test
{
    public class TestDatabaseCreation
    {
        public TestDatabaseCreation ()
        {

        }

        public void RunTest()
        {
            using (var _Entities = new TBSContext("TBSContext"))
            {
                var action = new TwitterBroadcastSystemModel.Models.Query()
                { PrimaryKey = Guid.NewGuid() };
                _Entities.Query.Add(action);
                _Entities.SaveChanges();
            }
        }
    }
}
