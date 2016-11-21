using System.Threading.Tasks;

namespace Abp.Net.Sms
{
    /// <summary>
    /// Sms Sender
    /// </summary>
    public interface ISmsSender
    {
        /// <summary>
        /// Send Sms
        /// </summary>
        /// <param name="to">Receiver</param>
        /// <param name="templateCode">template code</param>
        /// <param name="templateParams">template parameters</param>
        void Send(string to, string templateCode, string templateParams);

        /// <summary>
        /// Send Sms
        /// </summary>
        /// <param name="to">Receiver</param>
        /// <param name="templateCode">template code</param>
        /// <param name="templateParams">template parameters</param>
        /// <returns></returns>
        Task SendAsync(string to, string templateCode, string templateParams);

        /// <summary>
        /// Send Sms
        /// </summary>
        /// <param name="to">Receiver</param>
        /// <param name="templateCode">template code</param>
        /// <param name="templateParams">template parameters</param>
        /// <param name="freeSignName">free sign name</param>
        void Send(string to, string templateCode, string templateParams, string freeSignName);

        /// <summary>
        /// Send Sms
        /// </summary>
        /// <param name="to">Receiver</param>
        /// <param name="templateCode">template code</param>
        /// <param name="templateParams">template parameters</param>
        /// <param name="freeSignName">free sign name</param>
        /// <returns></returns>
        Task SendAsync(string to, string templateCode, string templateParams, string freeSignName);

        /// <summary>
        /// Send Sms
        /// </summary>
        /// <param name="sms"></param>
        void Send(SmsMessage sms);

        /// <summary>
        /// Send Sms
        /// </summary>
        /// <param name="sms"></param>
        /// <returns></returns>
        Task SendAsync(SmsMessage sms);
    }
}
