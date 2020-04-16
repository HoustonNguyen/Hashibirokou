using TwitterCommunicationsTester.Test;
using System.Configuration;

namespace TwitterCommunicationsTester
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test Comms
            //TestTwitterCommunication test1 = new TestTwitterCommunication();
            //test1.RunTest();

            GetAllRateLimits test1 = new GetAllRateLimits();
            test1.RunTest();

            //Test Comms
            //TestTwitterSearchUserAccount test1 = new TestTwitterSearchUserAccount();
            //test1.RunTest();

            //TestDatabaseCreation test1 = new TestDatabaseCreation();
            //test1.RunTest();
        }
    }
}
