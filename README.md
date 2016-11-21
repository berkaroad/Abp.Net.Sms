# Abp.Net.Sms
Sms interface, that based on [aspnet boilerplate]("http://www.aspnetboilerplate.com/", "aspnet boilerplate"), provider abstract sms service and sms configuration.

## Install
`Install-Package Abp.Net.Sms`

## How to use

### Create a sms sender service
For example, use AliDaYu

- 1. Install QifuTopSDK

`Install-Package QifuTopSDK`

- 2. Write sms sender adapter for AliDaYu
```
using Abp.Dependency;
using Abp.Net.Sms;
using Abp.UI;
using System;
using System.Threading.Tasks;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;

public class AliDayuSmsSender : SmsSenderBase, ITransientDependency
{
    private ITopClient client = null;
    public AliDayuSmsSender(ISmsSenderConfiguration configuration) : base(configuration)
    {
        client = new DefaultTopClient(configuration.GetServiceUrl(),
            configuration.GetAppKey(),
            configuration.GetAppSecret());
    }

    protected override void SendSms(SmsMessage sms)
    {
        var req = new AlibabaAliqinFcSmsNumSendRequest();
        req.SmsType = "normal";
        req.SmsFreeSignName = sms.FreeSignName;
        req.SmsParam = sms.TemplateParams;
        req.RecNum = sms.To;
        req.SmsTemplateCode = string.IsNullOrEmpty(sms.TemplateCode)
                ? _configuration.GetDefaultSmsTemplateCode()
                : sms.TemplateCode;
        AlibabaAliqinFcSmsNumSendResponse rsp = client.Execute(req);
        if (rsp.IsError)
        {
            throw new UserFriendlyException("Sms send fail",
                new Exception(string.Format("to:{0},errCode:{1},errMsg:{2}",
                    sms.To,
                    rsp.ErrCode,
                    rsp.ErrMsg)));
        }
        if (rsp.Result != null && !rsp.Result.Success)
        {
            throw new UserFriendlyException("Sms send fail",
                new Exception(string.Format("to:{0},result.errCode:{1},result.errMsg:{2}",
                    sms.To,
                    rsp.Result.ErrCode,
                    rsp.Result.Msg)));
        }
    }

    protected override Task SendSmsAsync(SmsMessage sms)
    {
        var req = new AlibabaAliqinFcSmsNumSendRequest();
        req.SmsType = "normal";
        req.SmsFreeSignName = sms.FreeSignName;
        req.SmsParam = sms.TemplateParams;
        req.RecNum = sms.To;
        req.SmsTemplateCode = string.IsNullOrEmpty(sms.TemplateCode)
            ? _configuration.GetDefaultSmsTemplateCode()
            : sms.TemplateCode;
        var task = new Task(()=> {
            AlibabaAliqinFcSmsNumSendResponse rsp = client.Execute(req);
            if (rsp.IsError)
            {
                throw new UserFriendlyException("Sms send fail",
                    new Exception(string.Format("to:{0},errCode:{1},errMsg:{2}",
                        sms.To,
                        rsp.ErrCode,
                        rsp.ErrMsg)));
            }
            if (rsp.Result != null && !rsp.Result.Success)
            {
                throw new UserFriendlyException("Sms send fail",
                    new Exception(string.Format("to:{0},result.errCode:{1},result.errMsg:{2}",
                        sms.To,
                        rsp.Result.ErrCode,
                        rsp.Result.Msg)));
            }
        });
        task.Start();
        return task;
    }
}
```

- 3. Write Setting Provider
```
using System.Collections.Generic;
using System.Configuration;
using Abp.Configuration;
using Abp.Net.Sms;

public class AppSettingProvider : SettingProvider
{
    public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
    {
        return new[]
            {
                // Sms config
                new SettingDefinition(SmsSettingNames.ServiceUrl,
                    ConfigurationManager.AppSettings[SmsSettingNames.ServiceUrl] ?? ""),
                new SettingDefinition(SmsSettingNames.AppKey,
                    ConfigurationManager.AppSettings[SmsSettingNames.AppKey] ?? ""),
                new SettingDefinition(SmsSettingNames.AppSecret,
                    ConfigurationManager.AppSettings[SmsSettingNames.AppSecret] ?? ""),
                new SettingDefinition(SmsSettingNames.DefaultFreeSignName,
                    ConfigurationManager.AppSettings[SmsSettingNames.DefaultFreeSignName] ?? ""),
                new SettingDefinition(SmsSettingNames.DefaultSmsTemplateCode,
                    ConfigurationManager.AppSettings[SmsSettingNames.DefaultSmsTemplateCode] ?? ""),  
        };
    }
}
```

- 4. Configurate default setting in Web.config
```
    <!--Production-->
    <add key="Abp.Net.Sms.ServiceUrl" value="http://gw.api.taobao.com/router/rest" />
    <!--<add key="Abp.Net.Sms.ServiceUrl" value="https://eco.taobao.com/router/rest" />-->
    <!--Sandbox-->
    <!--<add key="Abp.Net.Sms.ServiceUrl" value="http://gw.api.tbsandbox.com/router/rest" />-->
    <!--<add key="Abp.Net.Sms.ServiceUrl" value="https://gw.api.tbsandbox.com/router/rest" />-->
    <add key="Abp.Net.Sms.AppKey" value="XXXXX" />
    <add key="Abp.Net.Sms.AppSecret" value="XXXXX" />
    <add key="Abp.Net.Sms.DefaultSmsTemplateCode" value="SMS_XXXXX" />
    <add key="Abp.Net.Sms.DefaultFreeSignName" value="大鱼测试" />
```

### Use it in Asp.Net Identity or AbpZero
You should implement `Microsoft.AspNet.Identity.IIdentityMessageService`. Then you can send sms in userManager class.

```
using Abp.Dependency;
using Abp.Net.Sms;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

public class IdentitySmsMessageService : IIdentityMessageService, ITransientDependency
{
    private readonly ISmsSender _smsSender;

    public IdentitySmsMessageService(ISmsSender smsSender)
    {
        _smsSender = smsSender;
    }

    public virtual async Task SendAsync(IdentityMessage message)
    {
        if (message.Body != null)
        {
            var splitIndex = message.Body.IndexOf('|');// Split TemplateCode and TemplateParams by '|'.
            if (splitIndex >= 0 && splitIndex < message.Body.Length - 1)
            {
                if (splitIndex == 0)
                {
                    await _smsSender.SendAsync(message.Destination, "", message.Body.Substring(splitIndex + 1));
                }
                else
                {
                    await _smsSender.SendAsync(message.Destination, message.Body.Substring(0, splitIndex), message.Body.Substring(splitIndex + 1));
                }
            }
            else
            {
                await _smsSender.SendAsync(message.Destination, message.Body.Substring(0, splitIndex), "");
            }
        }
    }
}
```