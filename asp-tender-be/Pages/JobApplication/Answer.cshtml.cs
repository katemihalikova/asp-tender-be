using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using asp_tender_be.Models;
using asp_tender_be.Services;

namespace asp_tender_be.Pages
{
    public class JobApplicationAnswerModel : PageModel
    {
        private IRepository _repository;
        private IJobApplicationsHubConnector _jobApplicationsHubConnector;

        public JobApplicationAnswerModel(IRepository repository, IJobApplicationsHubConnector jobApplicationsHubConnector)
        {
            _repository = repository;
            _jobApplicationsHubConnector = jobApplicationsHubConnector;
        }

        [BindProperty]
        public JobApplicationAnswer JobApplicationAnswer { get; set; }

        public JobApplication JobApplication { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            JobApplication = await FindJobApplication(id);

            if (JobApplication == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            JobApplication = await FindJobApplication(id);

            if (JobApplication == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            JobApplicationAnswer.CreatedAt = DateTime.Now;

            _repository.InsertJobApplicationAnswer(JobApplicationAnswer);
            JobApplication.JobApplicationAnswerID = JobApplicationAnswer.ID;
            await _repository.Save();

            await _jobApplicationsHubConnector.RefreshOverviewViaHub();

            return RedirectToPage("./Index");
        }

        private async Task<JobApplication> FindJobApplication(int? id)
        {
            if (id == null)
            {
                return null;
            }

            return await _repository.GetPendingJobApplicationByID(id.Value);
        }
    }
}
