using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageCrawling.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			var path = @"C:\Users\Jérémy\Documents\Perso\Amplitude\ImageCrawling.Net\ImageCrawling.Api\Files";
			var urls = new[] { "http://www.cnn.com", "http://www.4chan.org" };

			var job1 = Job.JobManager.CreateJob(path, urls);
			var job2 = Job.JobManager.CreateJob(path, urls);
			var job3 = Job.JobManager.CreateJob(path, urls);

			System.Console.WriteLine("Press enter to start");
			var input = System.Console.ReadLine();

			Task.Run(() => Job.JobManager.Queue(job1));
			Task.Run(() => Job.JobManager.Queue(job2));
			Task.Run(() => Job.JobManager.Queue(job3));

			while (input != "quit")
			{
				Thread.Sleep(1000);

				WriteJobStatus(job1);
				WriteJobStatus(job2);
				WriteJobStatus(job3);

				input = System.Console.ReadLine();
			}
		}

		private static void WriteJobStatus(Job.Job job)
		{
			System.Console.WriteLine(job.Status);
			foreach (var page in job.Pages)
			{
				System.Console.WriteLine(page.Status);

				foreach (var image in page.Images)
				{
					System.Console.WriteLine(image.Status);
					System.Console.WriteLine(image.Src);
				}
			}
			System.Console.WriteLine("-----------------------");
		}
	}
}
