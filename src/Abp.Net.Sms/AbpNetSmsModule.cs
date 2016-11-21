using Abp.Modules;
using System.Reflection;

namespace Abp
{
    public class AbpNetSmsModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
