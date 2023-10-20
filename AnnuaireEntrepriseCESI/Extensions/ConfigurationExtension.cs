using AnnuaireEntrepriseCESI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnuaireEntrepriseCESI.Extensions
{
    public static class ConfigurationExtension
    {
        public static void AddConfiguration(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddOptions<AppSettings>()
                .Bind(configuration.GetSection("params"));
        }
    }
}
