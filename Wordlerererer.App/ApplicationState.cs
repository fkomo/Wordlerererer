using Ujeby.Blazor.Base;
using Ujeby.WordleGen;

namespace Ujeby.Wordlerererer.App
{
    public interface IWordleApplicationState : IApplicationState
	{
        Dictionary<string, Wordle> Wordles { get; set; }

        void UpdateWordle(string key, Wordle value);
    }

    public class ApplicationState : ApplicationStateBase, IWordleApplicationState
    {
        public ApplicationState()
		{
        }

        private bool _darkMode = true;
        public bool DarkMode
        { 
            get { return _darkMode; }
            set {  _darkMode = value; }  
        }

        public string ApplicationTitle => "Wordlerererer";

        public string HomeUrl => "/";

        public Dictionary<string, Wordle> Wordles { get; set; } = new Dictionary<string, Wordle>();

        public void UpdateWordle(string key, Wordle value)
		{
            if (Wordles.ContainsKey(key))
                Wordles.Remove(key);
    
            if (value != null)
                Wordles.Add(key, value);

            NotifyStateChanged();
        }
    }
}
