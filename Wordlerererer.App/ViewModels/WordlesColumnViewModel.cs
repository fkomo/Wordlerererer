using Ujeby.Blazor.Base.ViewModels;
using Ujeby.WordleGen;

namespace Ujeby.Wordlerererer.App.ViewModels
{
	public class WordlesColumnViewModel : ViewModelBase
	{
		public string[] Words { get; set; } = Array.Empty<string>();

		public WordlesColumnViewModel() : base()
		{

		}
	}
}
