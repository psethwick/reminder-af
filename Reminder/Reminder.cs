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
            var now = DateTime.UtcNow.ToBritishTime();
            log.Info($"C# Timer trigger function executed at: {now}");

            if (now.DayOfWeek != DayOfWeek.Friday)
            {
                log.Info("It's not Friday");
                return;
            }
            var tooEarly = new TimeSpan(6, 44, 0);
            if (now.TimeOfDay < tooEarly)
            {
                log.Info("It's too early");
                return;
            }
            var tooLate = new TimeSpan(8, 0, 0);
            if (now.TimeOfDay > tooLate)
            {
                log.Info("It's too late");
                return;
            }

            var message = GetEnvironmentVariable("Message");
            var numbers = GetEnvironmentVariable("PhoneNumbers").Split(',');
            foreach (var number in numbers)
            {
                log.Info(number);
                Sms.Send(number, message, log);
            }
        }
    }
}
