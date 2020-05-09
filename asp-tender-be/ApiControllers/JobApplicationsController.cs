using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using asp_tender_be.Data;
using asp_tender_be.Models;
using asp_tender_be.Services;
using asp_tender_be.ApiModels;

namespace asp_tender_be.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobApplicationsController : ControllerBase
    {
        private IRepository _repository;

        public JobApplicationsController(IRepository repository)
        {
            _repository = repository;
        }

        // POST: api/JobApplications
        [HttpPost]
        public async Task<ActionResult<JobApplication>> PostJobApplication([FromForm] PostJobApplicationModel form)
        {
            var position = await _repository.GetPositionByID(form.PositionID);

            if (position == null)
            {
                return NotFound();
            }

            using (var memoryStream = new MemoryStream())
            {
                var jobApplication = new JobApplication { PositionID = form.PositionID, Text = form.Text, Email = form.Email, Phone = form.Phone, CreatedAt = DateTime.Now };

                await form.Cv.CopyToAsync(memoryStream);
                jobApplication.Cv = memoryStream.ToArray();

                _repository.InsertJobApplication(jobApplication);
                await _repository.Save();
                return StatusCode(201, new { id = jobApplication.ID });
            }
        }
    }
}
