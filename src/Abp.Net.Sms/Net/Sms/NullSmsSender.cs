using Castle.Core.Logging;
using System.Threading.Tasks;

namespace Abp.Net.Sms
{
    /// <summary>
    /// Null SmsSender
    /// </summary>
    public class NullSmsSender : SmsSenderBase
    {
        /// <summary>
        /// Null instance
        /// </summary>
        public static NullSmsSender Null;

        /// <summary>
        /// Logger
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Creates a new <see cref="NullSmsSender"/> object.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        public NullSmsSender(ISmsSenderConfiguration configuration)
            : base(configuration)
        {
            Logger = NullLogger.Instance;
        }

        /// <summary>
        /// Send Sms
        /// </summary>
        /// <param name="sms"></param>
        protected override void SendSms(SmsMessage sms)
        {
            Logger.Warn("USING NullSmsSender!");
            Logger.Debug("SendSms:");
            LogSms(sms);
        }

        /// <summary>
        /// Send Sms
        /// </summary>
        /// <param name="sms"></param>
        /// <returns></returns>
        protected override Task SendSmsAsync(SmsMessage sms)
        {
            Logger.Warn("USING NullSmsSender!");
            Logger.Debug("SendSmsAsync:");
            LogSms(sms);
            return Task.FromResult(0);
        }

        private void LogSms(SmsMessage sms)
        {
            Logger.Debug(sms.To);
            Logger.Debug(sms.TemplateCode);
            Logger.Debug(sms.TemplateParams);
            Logger.Debug(sms.FreeSignName);
        }
    }
}
