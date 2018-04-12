using static System.Environment;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using Microsoft.Azure.WebJobs.Host;

namespace Remember.Your.Id
{
    public static class Sms
    {
        public static void Send(string phoneNumber, string message, TraceWriter log)
        {
            var accountSid = GetEnvironmentVariable("Twilio:AccountSid");
            var authToken = GetEnvironmentVariable("Twilio:AuthToken");
            var twilioPhoneNumber = GetEnvironmentVariable("Twilio:PhoneNumber");
            log.Info($"{accountSid} - {authToken} - {twilioPhoneNumber}");
            TwilioClient.Init(accountSid, authToken);

            var twilioMessage = MessageResource.Create(
                new PhoneNumber(phoneNumber),
                from: new PhoneNumber(twilioPhoneNumber),
                body: message);

            log.Info(twilioMessage.Sid);
        }
    }
}