using Ujeby.Blazor.Base.ViewModels;
using Ujeby.WordleGen;

namespace Ujeby.Wordlerererer.App.ViewModels
{
	public class WordleViewModel : ViewModelBase
	{
		public Wordle Data { get; set; } = null;

		public WordleViewModel() : base()
		{

		}
	}
}
