using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Ujeby.Blazor.Base;
using Ujeby.Blazor.Base.Components;
using Ujeby.WordleGen;
using Ujeby.Wordlerererer.App.ViewModels;

namespace Ujeby.Wordlerererer.App.Components
{
	public partial class WordlesColumnComponent : ComponentBase<WordlesColumnViewModel, IWordleApplicationState, ApplicationSettings>
	{
		[Parameter]
		public string Words { get; set; }

		protected override Task OnLoadDataAsync()
		{
			ViewModel.Words = Words?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();

			return base.OnLoadDataAsync();
		}

		protected override void OnInitialized()
		{
			AppState.OnChange += OnWordlesChangedAsync;

			base.OnInitialized();
		}

		private async void OnWordlesChangedAsync()
		{
			IsBusy = true;

			ViewModel.Words = AppState.Wordles.Values.Select(w => new string(w.Chars.Select(c => c.Value).ToArray())).ToArray();
			Words = string.Join(',', ViewModel.Words);

			IsBusy = false;

			await Task.CompletedTask;

			StateHasChanged();
		}

		protected override void Dispose(bool disposing)
		{
			AppState.OnChange -= OnWordlesChangedAsync;
		}
	}
}
