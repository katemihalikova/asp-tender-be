using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using asp_tender_be.Models;
using asp_tender_be.Services;

namespace asp_tender_be.Pages
{
    public class IndexModel : PageModel
    {
        public int UnsolvedCount { get; private set; }
        public IEnumerable<JobApplication> OldestJobApplications { get; private set; }

        private IRepository _repository;

        public IndexModel(IRepository repository)
        {
            _repository = repository;
        }

        public async Task OnGet()
        {
            var jobApplications = await _repository.GetPendingJobApplications();
            UnsolvedCount = jobApplications.Count();
            OldestJobApplications = jobApplications.Take(10);
        }
    }
}
