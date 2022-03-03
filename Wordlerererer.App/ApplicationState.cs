using Ujeby.Blazor.Base;
using Ujeby.WordleGen;

namespace Ujeby.Wordlerererer.App
{
    public interface IWordleApplicationState : IApplicationState
	{
        Dictionary<string, Wordle> Wordles { get; set; }

        void UpdateWordle(string key, Wordle value);

        event Action<string> OnKeyPress;
        void KeyPress(string key);
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

        public string Key { get; set; }

        public void UpdateWordle(string key, Wordle value)
		{
            if (Wordles.ContainsKey(key))
                Wordles.Remove(key);
    
            if (value != null)
                Wordles.Add(key, value);

            NotifyStateChanged();
        }

        public event Action<string> OnKeyPress;
        public void KeyPress(string key)
		{
            this.OnKeyPress?.Invoke(key);
        }
    }
}
