using InterviewTask.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace InterviewTask.Models
{
    public class IndexModel
    {
        public List<IndexService> Services { get; set; }

        public IndexModel(HelperServiceRepository repo)
        {
            Services = new List<IndexService>();

            Parallel.ForEach(repo.Get(), (service) =>
            {
                Services.Add(new IndexService(service));
            });
        }
    }
}