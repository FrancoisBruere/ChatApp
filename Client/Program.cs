using Blazored.LocalStorage;
using Blazored.Toast;
using ChatApp.Client.Logging;
using ChatApp.Client.Service;
using ChatApp.Client.Service.IService;
using ChatApp.Client.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddBlazoredToast();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            //builder.Services.AddLogging(logging =>
            //{
            //    var httpClient = builder.Services.BuildServiceProvider().GetRequiredService<HttpClient>();
            //    logging.SetMinimumLevel(LogLevel.Error);
            //    logging.AddProvider(new ApplicationLoggerProvider(httpClient));
            //});

            builder.Services.AddMudServices();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddScoped<IProfileViewModel, ProfileViewModel>();
            builder.Services.AddScoped<IUserSearchViewModel, UserSearchViewModel>();
            builder.Services.AddScoped<ILoginViewModel, LoginViewModel>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ISignUpViewModel, SignUpViewModel>();
            builder.Services.AddScoped<IChatHistoryViewModel, ChatHistoryViewModel>();
            builder.Services.AddScoped<IActivityViewModel, ActivityViewModel>();
            builder.Services.AddScoped<IContactViewModel, ContactViewModel>();
            


            
            await builder.Build().RunAsync();
        }
    }
}
