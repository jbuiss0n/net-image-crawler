using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ImageCrawling.Job
{
	public class Page
	{
		public Uri Url { get; set; }

		public string Dest { get; set; }

		public CrawlingStatus Status { get; set; }

		public IList<Image> Images { get; set; }

		public void Load(string html)
		{
			var dest = Path.Combine(Dest, Url.Host);
			Directory.CreateDirectory(dest);

			Images = new List<Image>();
			var regex = new Regex(@"<img[^>]*src=""([^""]+)""[^>]*>", RegexOptions.IgnoreCase);
			var macthes = regex.Matches(html);

			foreach (Match match in macthes)
			{
				var src = match.Groups[1].Value;

				var image = new Image
				{
					Src = GetImageUrl(src),
					Dest = dest,
					Status = CrawlingStatus.Dowloading,
				};

				Images.Add(image);
				image.Download();
			}

			Status = CrawlingStatus.Finish;
		}

		private string GetImageUrl(string url)
		{
			if (!url.StartsWith("http"))
			{
				if (url.StartsWith("//"))
				{
					url = Url.Scheme + ":" + url;
				}
				else
				{
					url = Url.OriginalString + url;
				}
			}

			return url;
		}
	}
}
