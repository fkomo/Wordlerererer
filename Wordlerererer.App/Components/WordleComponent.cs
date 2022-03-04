using Microsoft.AspNetCore.Components;
using Ujeby.Blazor.Base.Components;
using Ujeby.WordleGen;
using Ujeby.Wordlerererer.App.ViewModels;

namespace Ujeby.Wordlerererer.App.Components
{
	public partial class WordleComponent : ComponentBase<WordleViewModel, IWordleApplicationState, ApplicationSettings>
	{
		[Parameter]
		public string Word { get; set; } = string.Empty;

		public bool Valid => Word?.Length == WordleMap.WordLength;

		protected override async Task OnParametersSetAsync()
		{
			if (Valid)
				Update();

			await base.OnParametersSetAsync();
		}

		protected override async Task OnInitializedAsync()
		{
			if (Valid)
				Update();

			else
				AppState.OnKeyPress += OnKeyPress;

			await base.OnInitializedAsync();
		}

		private void Update()
		{
			if (AppState.Wordles.TryGetValue(Word, out Wordle wordle))
				ViewModel.Data = wordle;

			else
			{
				ViewModel.Data = new Wordle(Word);
				AppState.UpdateWordle(Word, ViewModel.Data);
			}
		}

		public void Add()
		{
			AppState.UpdateWordle(Word, new Wordle(Word));
			Word = string.Empty;

			StateHasChanged();
		}

		private void OnKeyPress(string key)
		{
			if (key?.ToLower() == "backspace" && Word?.Length > 0)
				Word = Word[..^1];

			else if (key.Length == 1 && WordGenerator.All.Contains(key[0]))
				Word += key;

			if (Valid)
				Add();

			StateHasChanged();
		}

		public void CharClicked(WordleChar wChar)
		{
			wChar.Type = (WordleCharType)(((int)wChar.Type + 1) % (int)WordleCharType.Count);

			AppState.UpdateWordle(ViewModel.Data.Word, ViewModel.Data);

			StateHasChanged();
		}

		public void Remove()
		{
			AppState.UpdateWordle(ViewModel.Data.Word, null);
		}

		protected override void Dispose(bool disposing)
		{
			AppState.OnKeyPress -= OnKeyPress;
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
	}
}
