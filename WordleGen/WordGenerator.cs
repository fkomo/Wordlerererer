
namespace Ujeby.WordleGen
{
	public struct Wordle
	{
		public static char[] All = "abcdefghijklmopqrstuvwxyz".ToArray();

		public char[] Discarded;
		public char[] Possible;
		public char[] Must;

		public char[][] Map;
	}

	public class WordGenerator
	{
		public bool AllowCharRepeat { get; private set; }

		public string[] Dictionary { get; private set; }

		public WordGenerator(bool allowCharRepeat = true, string[] dictionary = null)
		{
			AllowCharRepeat = allowCharRepeat;
			Dictionary = dictionary ?? Array.Empty<string>();
		}

		public List<string> Words { get; private set; } = new List<string>();

		public void Iterate(Wordle wordle, char[] build, int pos)
		{
			if (pos == wordle.Map.Length)
			{
				var word = new string(build).Trim();
				if (word.Length == wordle.Map.Length && !word.Contains('\0') 
					&& !Words.Contains(word) && wordle.Must.All(c => word.Contains(c))
					&& (AllowCharRepeat || word.Distinct().Count() == word.Length)
					&& (!Dictionary.Any() || Dictionary.Contains(word)))
				{
					Console.Write(word + " ");
					Words.Add(word);
				}
			}
			else
			{
				for (int i = 0; i < wordle.Map[pos].Length; i++)
				{
					build[pos] = wordle.Map[pos][i];
					Iterate(wordle, build, pos + 1);
				}
			}
		}
	}
}
