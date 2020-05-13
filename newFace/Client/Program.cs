using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Components;

namespace newFace.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient("newFace.ServerAPI",
                client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
                //.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("newFace.ServerAPI"));
            builder.Services.AddApiAuthorization();
            builder.Services.AddSingleton<MyService>();
            builder.Services.AddSingleton<WebAlert>();
            builder.Services.AddSingleton<LayoutLoading>();

            await builder.Build().RunAsync();
        }
    }
}
