using Blazored.LocalStorage;
using Blazored.Toast;
using ChatApp.Client.Handlers;
using ChatApp.Client.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using System;
using System.Net.Http;
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
            

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});

            
            //builder.Services.AddLogging(logging =>
            //{
            //    var httpClient = builder.Services.BuildServiceProvider().GetRequiredService<HttpClient>();
            //    logging.SetMinimumLevel(LogLevel.Error);
            //    logging.AddProvider(new ApplicationLoggerProvider(httpClient));
            //});

            builder.Services.AddMudServices();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddScoped<CustomAuthorizationHandler>();
            
            //builder.Services.AddScoped<IProfileViewModel, ProfileViewModel>();
            //builder.Services.AddScoped<IUserSearchViewModel, UserSearchViewModel>();
            builder.Services.AddScoped<ILoginViewModel, LoginViewModel>();
            
            builder.Services.AddScoped<ISignUpViewModel, SignUpViewModel>();
            //builder.Services.AddScoped<IChatHistoryViewModel, ChatHistoryViewModel>();
            //builder.Services.AddScoped<IActivityViewModel, ActivityViewModel>();
            //builder.Services.AddScoped<IContactViewModel, ContactViewModel>();

            AddHttpClients(builder);

            
            await builder.Build().RunAsync();

           




        }

        public static void AddHttpClients(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddHttpClient<IProfileViewModel, ProfileViewModel>
                ("ProfileViewModelClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<CustomAuthorizationHandler>();

            builder.Services.AddHttpClient<IUserSearchViewModel, UserSearchViewModel>
               ("UserSearchViewModelClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
               .AddHttpMessageHandler<CustomAuthorizationHandler>();

            builder.Services.AddHttpClient<IContactViewModel, ContactViewModel>
              ("ContactViewModelClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
              .AddHttpMessageHandler<CustomAuthorizationHandler>();

            builder.Services.AddHttpClient<IActivityViewModel, ActivityViewModel>
             ("ActivityViewModelClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
             .AddHttpMessageHandler<CustomAuthorizationHandler>();

            builder.Services.AddHttpClient<IChatHistoryViewModel, ChatHistoryViewModel>
             ("ChatHistoryViewModelClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
             .AddHttpMessageHandler<CustomAuthorizationHandler>();

        }
    }
}
