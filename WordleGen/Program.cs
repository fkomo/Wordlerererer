
//namespace Wordlerer
//{


//	public class Program
//	{
//		public static void Main(string[] args)
//		{
//			try
//			{
//				var dictionary = new List<string>();
//				foreach (var word in File.ReadAllLines(@"c:\Users\filip\source\repos\fkomo\Wordlerer\Wordlerer\dictionary.txt"))
//				{
//					if (word.Length == 5)
//						dictionary.Add(word);
//				}

//				var wordle = new Wordle();
//				wordle.Discarded = "scontmal".ToCharArray();
//				wordle.Possible = Wordle.All.Where(c => !wordle.Discarded.Contains(c)).ToArray();
//				wordle.Must = "uper".ToCharArray();

//				wordle.Map = new char[][]
//				{
//					"r".ToCharArray(),
//					"u".ToCharArray(),
//					"p".ToCharArray(),
//					"e".ToCharArray(),
//					wordle.Possible.Where(c => !"r".Contains(c)).ToArray(),
//				};

//				var gen = new WordGenerator(allowCharRepeat: true, dictionary: dictionary.ToArray());
//				gen.Iterate(wordle, new char[wordle.Map.Length], 0);

//				Console.WriteLine(Environment.NewLine + gen.Words.Count);
//			}
//			catch (Exception ex)
//			{
//				Console.WriteLine(ex.ToString());
//			}
//			finally
//			{
//				Console.ReadKey();
//			}
//		}
//	}
//}