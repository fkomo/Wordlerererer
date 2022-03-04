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
		//[Parameter]
		//public string Words { get; set; } = string.Empty;

		//protected override async Task OnParametersSetAsync()
		//{
		//	ViewModel.Words = Words?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
		//	await base.OnParametersSetAsync();
		//}

		protected override void OnInitialized()
		{
			AppState.OnChange += OnWordlesChangedAsync;

			base.OnInitialized();
		}

		private async void OnWordlesChangedAsync()
		{
			IsBusy = true;

			ViewModel.Words = AppState.Wordles.Values.Select(w => w.Word).ToArray();
			//Words = string.Join(',', ViewModel.Words);

			IsBusy = false;

			//await this.OnUpdateAsync();
			StateHasChanged();
		}

		protected override void Dispose(bool disposing)
		{
			AppState.OnChange -= OnWordlesChangedAsync;
		}
	}
}
