using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using asp_tender_be.Models;
using asp_tender_be.Services;

namespace asp_tender_be.Pages
{
    public class JobApplicationIndexModel : PageModel
    {
        private IRepository _repository;

        public JobApplicationIndexModel(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<JobApplication> JobApplications { get; set; }

        public async Task OnGetAsync()
        {
            JobApplications = await _repository.GetPendingJobApplications();
        }
    }
}
