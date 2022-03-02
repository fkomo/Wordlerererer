using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Ujeby.Wordlerererer.App;
using Ujeby.Blazor.Base;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

builder.Services.AddScoped<IApplicationState, ApplicationState>();
builder.Services.AddScoped<IWordleApplicationState, ApplicationState>();

builder.Services.AddSingleton((_) =>
	builder.Configuration.GetSection(nameof(ApplicationSettings)).Get<ApplicationSettings>());

//builder.Services.AddLocalization();

var host = builder.Build();

//await host.SetDefaultCulture();

await host.RunAsync();