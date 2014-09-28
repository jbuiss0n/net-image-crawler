using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ImageCrawling.Job
{
	public class JobManager
	{
		private static IList<Job> s_jobs = new List<Job>();
		private static object s_lock = new object();

		public static IList<Job> GetAll()
		{
			lock (s_lock)
			{
				return s_jobs.ToList();
			}
		}

		public static Job Get(long id)
		{
			lock (s_lock)
			{
				return s_jobs.FirstOrDefault(j => j.Id == id);
			}
		}

		public static Job CreateJob(string path, string[] urls)
		{
			var id = s_jobs.Count;

			return new Job
			{
				Id = id,
				Status = CrawlingStatus.Crawling,
				Pages = urls.Select(url => new Page
				{
					Url = new Uri(url),
					Dest = Path.Combine(path, id.ToString()),
					Status = CrawlingStatus.Crawling,
					Images = new List<Image>(),
				}).ToList()
			};
		}

		public static void Queue(Job job)
		{
			lock (s_lock)
			{
				s_jobs.Add(job);
			}
			job.Crawl();
		}
	}
}
