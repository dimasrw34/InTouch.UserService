using Microsoft.Extensions.Configuration;

namespace InTouch.UserService.Core;

public static class ConfigurationExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    /// <typeparam name="TOptions"></typeparam>
    /// <returns></returns>
    public static TOptions GetOptions<TOptions>(this IConfiguration configuration)
        where TOptions : class, IAppOptions
    {
        return configuration
            .GetRequiredSection(TOptions.ConfigSectionPath)
            .Get<TOptions>(options => options.BindNonPublicProperties = true);
    }
}