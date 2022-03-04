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
		protected override void OnInitialized()
		{
			AppState.OnChange += OnWordlesChanged;

			base.OnInitialized();
		}

		private void OnWordlesChanged()
		{
			IsBusy = true;

			ViewModel.Words = AppState.Wordles.Values.Select(w => w.Word).ToArray();

			IsBusy = false;

			StateHasChanged();
		}

		protected override void Dispose(bool disposing)
		{
			AppState.OnChange -= OnWordlesChanged;
		}
	}
}
