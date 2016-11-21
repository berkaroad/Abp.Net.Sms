using System.Threading.Tasks;

namespace Abp.Net.Sms
{
    /// <summary>
    /// Send Sms
    /// </summary>
    public abstract class SmsSenderBase : ISmsSender
    {
        /// <summary>
        /// SmsSender Configuration
        /// </summary>
        protected ISmsSenderConfiguration _configuration = null;

        /// <summary>
        /// Send Sms
        /// </summary>
        /// <param name="configuration"></param>
        protected SmsSenderBase(ISmsSenderConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Send Sms
        /// </summary>
        /// <param name="to">Receiver</param>
        /// <param name="templateCode">template code</param>
        /// <param name="templateParams">template parameters</param>
        public void Send(string to, string templateCode, string templateParams)
        {
            SendSms(new SmsMessage(to, templateCode, templateParams, _configuration.GetDefaultFreeSignName()));
        }

        /// <summary>
        /// Send Sms
        /// </summary>
        /// <param name="to">Receiver</param>
        /// <param name="templateCode">template code</param>
        /// <param name="templateParams">template parameters</param>
        /// <returns></returns>
        public async Task SendAsync(string to, string templateCode, string templateParams)
        {
            await SendSmsAsync(new SmsMessage(to, templateCode, templateParams, _configuration.GetDefaultFreeSignName()));
        }

        /// <summary>
        /// Send Sms
        /// </summary>
        /// <param name="to">Receiver</param>
        /// <param name="templateCode">template code</param>
        /// <param name="templateParams">template parameters</param>
        /// <param name="freeSignName">free sign name</param>
        public void Send(string to, string templateCode, string templateParams, string freeSignName)
        {
            SendSms(new SmsMessage(to, templateCode, templateParams, freeSignName));
        }

        /// <summary>
        /// Send Sms
        /// </summary>
        /// <param name="to">Receiver</param>
        /// <param name="templateCode">template code</param>
        /// <param name="templateParams">template parameters</param>
        /// <param name="freeSignName">free sign name</param>
        /// <returns></returns>
        public async Task SendAsync(string to, string templateCode, string templateParams, string freeSignName)
        {
            await SendSmsAsync(new SmsMessage(to, templateCode, templateParams, freeSignName));
        }

        /// <summary>
        /// Send Sms
        /// </summary>
        /// <param name="sms"></param>
        /// <returns></returns>
        protected abstract Task SendSmsAsync(SmsMessage sms);

        /// <summary>
        /// Send Sms
        /// </summary>
        /// <param name="sms"></param>
        protected abstract void SendSms(SmsMessage sms);

        /// <summary>
        /// Send Sms
        /// </summary>
        /// <param name="sms"></param>
        public void Send(SmsMessage sms)
        {
            SendSms(sms);
        }

        /// <summary>
        /// Send Sms
        /// </summary>
        /// <param name="sms"></param>
        /// <returns></returns>
        public async Task SendAsync(SmsMessage sms)
        {
            await SendSmsAsync(sms);
        }
    }
}
