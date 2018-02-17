using System.Threading;

namespace threading
{
	public static class ThreadsCreation
	{
		public static void Test()
		{
			for (int i = 0; i < 10000; i++)
			{
				var t = new Thread(() => { });
				t.Start();
			}
		}
	}
}