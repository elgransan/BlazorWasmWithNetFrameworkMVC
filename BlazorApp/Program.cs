//#define withLayout

using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
#if withLayout
            builder.RootComponents.Add<AppDebug>("#app");
#else
            builder.RootComponents.Add<App>("#app");
#endif
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddSingleton<INavigationInterception>(new DisabledNavigationInterception());

            await builder.Build().RunAsync();
        }

        public class DisabledNavigationInterception : INavigationInterception
        {
            public Task EnableNavigationInterceptionAsync() => Task.CompletedTask;
        }

    }
}
