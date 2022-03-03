using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Ujeby.Blazor.Base.Components;
using Ujeby.WordleGen;
using Ujeby.Wordlerererer.App.ViewModels;

namespace Ujeby.Wordlerererer.App.Components
{
	public partial class WordleComponent : ComponentBase<WordleViewModel, IWordleApplicationState, ApplicationSettings>
	{
		[Parameter]
		public string Word { get; set; } = string.Empty;

		[Inject]
		public IJSRuntime JSRuntime { get; set; }

		public bool Valid => Word?.Length == WordleMap.WordLength;

		protected override void OnInitialized()
		{
			base.OnInitialized();
		}

		protected override async Task OnLoadDataAsync()
		{
			UpdateData();

			if (Valid)
				AppState.UpdateWordle(ViewModel.Id, ViewModel.Data);

			else
				AppState.OnKeyPress += OnKeyPressAsync;

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
			if (Valid)
				ViewModel.Data = ViewModel.Data ?? new Wordle(Word);
		}

		private async Task AddAsync()
		{
			ViewModel.Data = new Wordle(Word);
			AppState.UpdateWordle(ViewModel.Id, ViewModel.Data);

			ViewModel.Data = null;
			Word = string.Empty;

			await base.OnUpdateAsync();
		}

		private async void OnKeyPressAsync(string key)
		{
			if (key?.ToLower() == "backspace" && Word?.Length > 0)
				Word = Word[..^1];

			else if (key.Length == 1 && WordGenerator.All.Contains(key[0]))
				Word += key;

			if (Valid)
				await AddAsync();

			else
				StateHasChanged();
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

		protected override void Dispose(bool disposing)
		{
			AppState.OnKeyPress -= OnKeyPressAsync;
		}
	}
}
