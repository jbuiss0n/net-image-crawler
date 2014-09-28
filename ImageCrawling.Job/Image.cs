using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ImageCrawling.Job
{
	public class Image
	{
		public CrawlingStatus Status { get; set; }

		public string Src { get; set; }

		public string Dest { get; set; }

		internal void Download()
		{
			try
			{
				var webClient = new WebClient();
				webClient.DownloadFile(Src, Path.Combine(Dest, Path.GetFileName(Src)));
				Status = CrawlingStatus.Finish;
			}
			catch (Exception)
			{

				Status = CrawlingStatus.Error;
			}
		}
	}
}
