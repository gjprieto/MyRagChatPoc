using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRagChatPoc.Kernel.Services.FileCache
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddFileCacheService(this IServiceCollection services) 
        {
            services.AddSingleton<IFileCacheService, FileCacheService>();
            return services;
        }
    }
}