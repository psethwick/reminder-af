using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using static System.Environment;

namespace Remember.Your.Id
{
    public static class Reminder
    {
        [FunctionName("Reminder")]
        public static void Run([TimerTrigger("0 */15 * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

            if (DateTime.Now.DayOfWeek != DayOfWeek.Friday)
            {
                log.Info("It's not Friday");
                return;
            }
            var tooEarly = new TimeSpan(6, 44, 0);
            if (DateTime.Now.TimeOfDay < tooEarly)
            {
                log.Info("It's too early");
                return;
            }
            var tooLate = new TimeSpan(9, 0, 0);
            if (DateTime.Now.TimeOfDay > tooLate)
            {
                log.Info("It's too late");
                return;
            }

            const string message = "Remember your id...";
            var number = GetEnvironmentVariable("PhoneNumber");
            log.Info(number);
            Sms.Send(number, message, log);
        }
    }
}
