using Storm.JWTHelper.Generate.Models;

namespace Fintranet.TaxCalculation.Api.Base.Models
{
    public static class AppSettingInfo
    {
        static AppSettingInfo()
        {
            var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appSettings.json")
                    .Build();

            AppSettingsInfo = config.Get<AppsettingFild>();
        }
        public static AppsettingFild AppSettingsInfo { get; set; }
    }

    public class AppsettingFild
    {
        public bool HostRunAsConsole { get; set; }
        public string HostAddress { get; set; }
        public JwtIssuerOptions JwtIssuerOptions { get; set; }
    }
}
