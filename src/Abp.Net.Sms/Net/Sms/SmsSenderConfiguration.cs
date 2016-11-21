using Abp.Configuration;
using Abp.Dependency;

namespace Abp.Net.Sms
{
    /// <summary>
    /// SmsSender Configuration
    /// </summary>
    public class SmsSenderConfiguration : ISmsSenderConfiguration, ITransientDependency
    {
        private ISettingManager _settingManager = null;

        /// <summary>
        /// SmsSender Configuration
        /// </summary>
        /// <param name="settingManager"></param>
        public SmsSenderConfiguration(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }

        /// <summary>
        /// AppKey
        /// </summary>
        /// <returns></returns>
        public string GetAppKey()
        {
            return _settingManager.GetSettingValueForApplication(SmsSettingNames.AppKey);
        }

        /// <summary>
        /// AppSecret
        /// </summary>
        /// <returns></returns>
        public string GetAppSecret()
        {
            return _settingManager.GetSettingValueForApplication(SmsSettingNames.AppSecret);
        }

        /// <summary>
        /// DefaultFreeSignName
        /// </summary>
        /// <returns></returns>
        public string GetDefaultFreeSignName()
        {
            return _settingManager.GetSettingValueForApplication(SmsSettingNames.DefaultFreeSignName);
        }

        /// <summary>
        /// Default SmsTemplateCode
        /// </summary>
        /// <returns></returns>
        public string GetDefaultSmsTemplateCode()
        {
            return _settingManager.GetSettingValueForApplication(SmsSettingNames.DefaultSmsTemplateCode);
        }

        /// <summary>
        /// ServiceUrl
        /// </summary>
        /// <returns></returns>
        public string GetServiceUrl()
        {
            return _settingManager.GetSettingValueForApplication(SmsSettingNames.ServiceUrl);
        }
    }
}
