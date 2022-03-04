using Ujeby.Blazor.Base.Components;
using Ujeby.WordleGen;
using Ujeby.Wordlerererer.App.ViewModels;

namespace Ujeby.Wordlerererer.App.Components
{
	public partial class WordsComponent : ComponentBase<WordsViewModel, IWordleApplicationState, ApplicationSettings>
	{
		private readonly WordGenerator _WorldGenerator = new();

		protected override async Task OnInitializedAsync()
		{
			AppState.OnChange += OnWordlesChangedAsync;

			await base.OnInitializedAsync();
		}

		private async void OnWordlesChangedAsync()
		{
			IsBusy = true;

			var wordleMap = new WordleMap(AppState.Wordles.Values.ToArray());

			ViewModel.Words.Clear();
			await foreach (var word in _WorldGenerator.IterateAsync(wordleMap))
			{
				ViewModel.Words.Add(word);
				this.StateHasChanged();
			}

			IsBusy = false;
		}

		protected override void Dispose(bool disposing)
		{
			AppState.OnChange -= OnWordlesChangedAsync;
		}
	}
}
