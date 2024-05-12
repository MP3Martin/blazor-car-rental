using blazor_car_rental.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MudExtensions.Services;

namespace blazor_car_rental {
    public static class Program {
        public static async Task Main(string[] args) {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddMudServices();
            builder.Services.AddMudExtensions();
            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddScoped<PageNameService>();
            builder.Services.AddScoped<StateService>();

            await builder.Build().RunAsync();
        }
    }
}
