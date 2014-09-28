using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ImageCrawling.Api.Controllers
{
	public class JobsController : ApiController
	{
		public IEnumerable<Job.Job> Get()
		{
			return Job.JobManager.GetAll();
		}

		public Job.Job Get(long id)
		{
			return Job.JobManager.Get(id);
		}

		public Job.Job Post([FromBody]string[] urls)
		{
			if (urls == null || urls.Length == 0)
			{
				throw new HttpResponseException(HttpStatusCode.BadRequest);
			}

			var job = Job.JobManager.CreateJob(HttpContext.Current.Server.MapPath("~/Files"), urls);

			Task.Run(() => Job.JobManager.Queue(job));

			return job;
		}
	}
}
