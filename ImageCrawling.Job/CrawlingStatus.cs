using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCrawling.Job
{
	public enum CrawlingStatus
	{
		Crawling = 0,
		Dowloading = 1,
		Finish = 2,
		Error = 3,
	}
}
