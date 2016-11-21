namespace Abp.Net.Sms
{
    /// <summary>
    /// Sms Message
    /// </summary>
    public class SmsMessage
    {
        /// <summary>
        /// Sms Message
        /// </summary>
        /// <param name="to">Receiver</param>
        /// <param name="templateCode">template code</param>
        /// <param name="templateParams">template parameters</param>
        /// <param name="freeSignName">free sign name</param>
        public SmsMessage(string to, string templateCode, string templateParams, string freeSignName = "")
        {
            To = to;
            TemplateCode = templateCode;
            TemplateParams = templateParams;
            FreeSignName = freeSignName;
        }

        /// <summary>
        /// Receiver
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Template code or content
        /// </summary>
        public string TemplateCode { get; set; }

        /// <summary>
        /// Template parameters
        /// </summary>
        public string TemplateParams { get; set; }

        /// <summary>
        /// Free SignName
        /// </summary>
        public string FreeSignName { get; set; }
    }
}
