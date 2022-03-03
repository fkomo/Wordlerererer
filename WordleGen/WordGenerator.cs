
namespace Ujeby.WordleGen
{
	public enum WordleCharType
	{
		Wrong = 0,
		BadPosition,
		RightPosition,

		Count
	}
	public class WordleChar
	{
		public char Value { get; set; }
		public WordleCharType Type { get; set; }
	}

	public class Wordle
	{
		public WordleChar[] Chars;
		public string Word { get; private set; }

		public Wordle(string word)
		{
			Word = word;
			Chars = word.ToCharArray().Select(c => new WordleChar { Type = WordleCharType.Wrong, Value = c }).ToArray();
		}

		public override string ToString() => Word;
	}

	public class WordleMap
	{
		public const int WordLength = 5;

		public char[] Must { get; set; }
		public char[] Discarded { get; set; }

		public List<char>[] Map { get; set; }

		public WordleMap(Wordle[] wordles)
		{
			Map = new List<char>[WordLength];

			for (var i = 0; i < WordLength; i++)
				Map[i] = new List<char>(WordGenerator.All);

			var must = new List<char>();
			var discarded = new List<char>();
			foreach (var wordle in wordles)
			{
				for (var i = 0; i < wordle.Chars.Length; i++)
				{
					var _char = wordle.Chars[i];

					if (_char.Type == WordleCharType.Wrong || _char.Type == WordleCharType.BadPosition)
						Map[i].Remove(_char.Value);

					if (_char.Type == WordleCharType.RightPosition)
						Map[i] = new List<char>() { _char.Value };

					if (_char.Type == WordleCharType.RightPosition || _char.Type == WordleCharType.BadPosition)
						must.Add(_char.Value);

					if (_char.Type == WordleCharType.Wrong)
						discarded.Add(_char.Value);
				}
			}

			Discarded = discarded.Distinct().Except(must).ToArray();
			Must = must.ToArray();
		}
	}

	public class WordGenerator
	{
		public static char[] All = "abcdefghijklmnopqrstuvwxyz".ToArray();

		public static string[] WordDictionary { get; set; } = Array.Empty<string>();

		private static bool CheckWord(string word, WordleMap wordle)
		{
			if (word.Length != WordleMap.WordLength)
				return false;

			if (wordle.Discarded.Any(d => word.Contains(d)))
				return false;

			if (wordle.Must.Any(m => !word.Contains(m)))
				return false;

			for (var i = 0; i < word.Length; i++)
				if (!wordle.Map[i].Contains(word[i]))
					return false;

			return true;
		}

		public async IAsyncEnumerable<string> IterateAsync(WordleMap wordleMap)
		{
			foreach (var word in WordDictionary)
			{
				if (CheckWord(word, wordleMap))
					yield return word;
			}
		}

		//public void Iterate(WordleMap wordle, char[] build, int pos)
		//{
		//	var words = new List<string>();
		//	if (pos == wordle.Map.Length)
		//	{
		//		var word = new string(build).Trim();
		//		if (word.Length == wordle.Map.Length && !word.Contains('\0')
		//			&& !Words.Contains(word) && wordle.Must.All(c => word.Contains(c)))
		//		{
		//			Words.Add(word);
		//		}
		//	}
		//	else
		//	{
		//		for (int i = 0; i < wordle.Map[pos].Count; i++)
		//		{
		//			build[pos] = wordle.Map[pos][i];
		//			Iterate(wordle, build, pos + 1);
		//		}
		//	}
		//}
	}
}
