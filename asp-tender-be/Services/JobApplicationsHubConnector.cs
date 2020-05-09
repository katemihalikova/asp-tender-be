using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using asp_tender_be.Models;
using asp_tender_be.Data;
using asp_tender_be.Hubs;

namespace asp_tender_be.Services
{
    public class JobApplicationsHubConnector : IJobApplicationsHubConnector
    {
        private IRepository _repository;
        private IHubContext<JobApplicationsHub> _hubContext;

        public JobApplicationsHubConnector(IRepository repository, IHubContext<JobApplicationsHub> hubContext)
        {
            _repository = repository;
            _hubContext = hubContext;
        }

        public async Task RefreshOverviewViaHub()
        {
            var jobApplications = await _repository.GetPendingJobApplications();
            var count = jobApplications.Count();
            var oldest = jobApplications.Take(10).Select(j => new { id = j.ID, createdAt = j.CreatedAt.ToString("d.M.yyyy H:mm"), text = j.Text, position = j.Position.Name, workplace = j.Position.Workplace.Name });
            await _hubContext.Clients.All.SendAsync("RefreshOverview", new { count, oldest });
        }
    }
}
