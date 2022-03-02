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

if (Ujeby.WordleGen.WordGenerator.WordDictionary == null)
{
	var response = await new HttpClient().GetStringAsync("https://localhost:8030/content/dictionary.txt");
	Ujeby.WordleGen.WordGenerator.WordDictionary = response.Split('\n', StringSplitOptions.RemoveEmptyEntries)
		.Where(w => w.Length == Ujeby.WordleGen.WordleMap.WordLength).ToArray();
}

var host = builder.Build();

//await host.SetDefaultCulture();

await host.RunAsync();