using Microsoft.AspNetCore.Components;
using Ujeby.Blazor.Base.Components;
using Ujeby.WordleGen;
using Ujeby.Wordlerererer.App.ViewModels;

namespace Ujeby.Wordlerererer.App.Components
{
	public partial class WordleComponent : ComponentBase<WordleViewModel, IWordleApplicationState, ApplicationSettings>
	{
		[Parameter]
		public string Word { get; set; }

		protected override void OnInitialized()
		{
			base.OnInitialized();
		}

		protected override async Task OnLoadDataAsync()
		{
			UpdateData();
			AppState.UpdateWordle(ViewModel.Id, ViewModel.Data);

			await base.OnLoadDataAsync();
		}

		public static MudBlazor.Color GetWordleCharColor(WordleCharType type)
		{
			return type switch
			{
				WordleCharType.RightPosition => MudBlazor.Color.Success,
				WordleCharType.BadPosition => MudBlazor.Color.Warning,
				_ => MudBlazor.Color.Default,
			};
		}

		protected override async Task OnUpdateAsync()
		{
			UpdateData();	

			await base.OnUpdateAsync();
		}

		private void UpdateData()
		{
			ViewModel.Data = new Wordle(Word);
		}

		public async void CharClickedAsync(WordleChar wChar)
		{
			wChar.Type = (WordleCharType)(((int)wChar.Type + 1) % (int)WordleCharType.Count);

			AppState.UpdateWordle(ViewModel.Id, ViewModel.Data);

			await base.OnUpdateAsync();
		}

		public async void RemoveAsync()
		{
			AppState.UpdateWordle(ViewModel.Id, null);

			await base.OnUpdateAsync();
		}
	}
}
