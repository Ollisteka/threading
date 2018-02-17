using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace threading
{
	public static class ListExtensions
	{
		public static decimal GetMedian(this IEnumerable<int> source)
		{
			// Create a copy of the input, and sort the copy
			var temp = source.ToArray();
			Array.Sort(temp);

			var count = temp.Length;
			if (count == 0)
				throw new InvalidOperationException("Empty collection");
			if (count % 2 == 0)
			{
				// count is even, average two middle elements
				var a = temp[count / 2 - 1];
				var b = temp[count / 2];
				return (a + b) / 2m;
			}
			// count is odd, return the middle element
			return temp[count / 2];
		}
	}

	public class QuantsTime
	{
		private static readonly List<int> quants = new List<int>();


		private static void Test(string[] args)
		{
			var processorNum = args.Length > 0 ? int.Parse(args[0]) - 1 : 0;
			Process.GetCurrentProcess().ProcessorAffinity = (IntPtr) (1 << processorNum);
			var t = new Thread(CheckTime);
			t.Start();

			CheckTime();
			t.Join();
			foreach (var quant in quants)
				Console.WriteLine($"The quant is: {quant}");

			Console.WriteLine(quants.GetMedian());
			Console.WriteLine(quants.Average());
		}

		private static void CheckTime()
		{
			var previousTime = Environment.TickCount;
			for (var i = 0; i < 300000000; i++)
			{
				var currentTime = Environment.TickCount;
				var difference = currentTime - previousTime;
				if (difference > 1)
					quants.Add(difference);
				previousTime = currentTime;
			}
		}
	}
}