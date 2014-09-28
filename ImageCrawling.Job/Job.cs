using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ImageCrawling.Job
{
	public class Job
	{
		public long Id { get; set; }

		public string Path { get; set; }

		public CrawlingStatus Status { get; set; }

		public IList<Page> Pages { get; set; }

		public void Crawl()
		{
			foreach (var page in Pages)
			{
				var webClient = new WebClient();
				var html = webClient.DownloadString(page.Url);

				page.Load(html);
			}

			Status = CrawlingStatus.Finish;
		}
	}
}
