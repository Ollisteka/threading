using System;
using System.Collections.Generic;
using System.Threading;

namespace threading
{
	public class ThreadPool
	{
		public readonly int MaxSize;
		private readonly Queue<Action> tasks = new Queue<Action>();

		public ThreadPool(int quantity)
		{
			MaxSize = quantity;
			
			for (var i = 0; i < quantity; i++)
			{
				var thread = new Thread(WaitForTask);
				thread.Start();
			}
		}

		private void WaitForTask()
		{
			while (true)
			{
				Action action;
				lock (tasks)
				{
					if (tasks.Count == 0)
						Monitor.Wait(tasks);
					
					action = tasks.Dequeue();
				}
				action.Invoke();
			}

		}

		public void EnqueueTask(Action act)
		{
			lock (tasks)
			{
				tasks.Enqueue(act);
				Monitor.Pulse(tasks);
			}
		}
	}
}