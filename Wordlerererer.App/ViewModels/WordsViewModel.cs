using Ujeby.Blazor.Base.ViewModels;

namespace Ujeby.Wordlerererer.App.ViewModels
{
	public class WordsViewModel : ViewModelBase
	{
		public List<string> Words { get; set; } = new List<string>();

		public WordsViewModel() : base()
		{
		}
	}
}
