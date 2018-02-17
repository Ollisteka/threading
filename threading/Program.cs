using System;
using System.Threading;

namespace threading
{
	public class Program
	{
		private static ThreadPool pool;

		private static void Main(string[] args)
		{
			pool = new ThreadPool(2);
			for (var i = 0; i < 16; i++)
			{
				var temp = i;
				pool.EnqueueTask(() =>
				{
					Thread.Sleep(3000);
					Console.WriteLine($"Hi from {temp}");
				});
			}
		}
	}
}