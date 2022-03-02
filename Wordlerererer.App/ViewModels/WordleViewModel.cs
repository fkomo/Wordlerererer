using Ujeby.Blazor.Base.ViewModels;
using Ujeby.WordleGen;

namespace Ujeby.Wordlerererer.App.ViewModels
{
	public class WordleViewModel : ViewModelBase
	{
		public const int Length = 5;

		public string Id => Data.Word;
		public Wordle Data { get; set; } = null;

		public WordleViewModel() : base()
		{

		}
	}
}
