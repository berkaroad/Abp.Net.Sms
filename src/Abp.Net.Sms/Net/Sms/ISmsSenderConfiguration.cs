using Abp.Dependency;

namespace Abp.Net.Sms
{
    /// <summary>
    /// Sms sender configuration
    /// </summary>
    public interface ISmsSenderConfiguration : ITransientDependency
    {
        /// <summary>
        /// Service Url
        /// </summary>
        /// <returns></returns>
        string GetServiceUrl();

        /// <summary>
        /// AppKey or userName
        /// </summary>
        /// <returns></returns>
        string GetAppKey();

        /// <summary>
        /// AppSecret or password
        /// </summary>
        /// <returns></returns>
        string GetAppSecret();

        /// <summary>
        /// Default FreeSignName
        /// </summary>
        /// <returns></returns>
        string GetDefaultFreeSignName();

        /// <summary>
        /// Default SmsTemplateCode
        /// </summary>
        /// <returns></returns>
        string GetDefaultSmsTemplateCode();
    }
}
