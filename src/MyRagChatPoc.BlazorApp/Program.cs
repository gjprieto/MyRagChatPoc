using Microsoft.AspNetCore.ResponseCompression;
using MyRagChatPoc.BlazorApp.Components;
using Radzen;
using MyRagChatPoc.Kernel.Services.KnowledgeBase;
using MyRagChatPoc.Kernel.Services.FileCache;
using MyRagChatPoc.Kernel.Services.Chat;

namespace MyRagChatPoc.BlazorApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables();

            builder.Services.AddResponseCompression(opts => {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
            });

            builder.Services
                .AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services
                .AddFileCacheService()                
                .AddRadzenComponents();

            builder.Services.AddControllers();

            builder.Services                
                .AddKnowledgeBaseService()
                .AddChatService();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.MapControllers();

            app.Run();
        }
    }
}