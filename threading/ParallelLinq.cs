using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace threading
{
	public class ParallelLinq
	{
		public static void Run(string[] args)
		{
			var res = GetTop10Users();
			foreach (var pair in res)
			{
				Console.Write($"{pair.Key} : ");
				for (int i = 0; i < pair.Value / 20; i++)
				{
					Console.Write("*");
				}
				Console.WriteLine();
			}
			//ip - время
			//распарсить, топ 10
		}

		public static Dictionary<int, int> GetTop10Users()
		{
			var stat = new List<int>();
			Parallel.ForEach(Enumerable.Repeat(0, 10000),
				//new ParallelOptions {MaxDegreeOfParallelism = 2},
				line =>
				{
					lock (stat)
					{
						var temp = Environment.CurrentManagedThreadId;
						stat.Add(temp);
				//		Console.Write(Environment.CurrentManagedThreadId);
					}
				});
			foreach (var v in stat)
			{
				//Console.Write(v);
			}
			return stat.GroupBy(x => x).ToDictionary(k => k.Key, v => v.Sum());
			//return new Dictionary<int, int>();
			//			foreach (var pair in usersStats)
			//			{
			//				if (result.ContainsKey(pair.Key))
			//					result[pair.Key] += 1;
			//				else
			//				{
			//					result[pair.Key] = 1;
			//				}
			//			}
			//return new Dictionary<int, int>();
		}
	}
}