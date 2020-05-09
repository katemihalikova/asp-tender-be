using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using asp_tender_be.Models;
using asp_tender_be.Data;

namespace asp_tender_be.Services
{
    public class Repository : IRepository
    {
        private TenderContext _context;

        public Repository(TenderContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Workplace>> GetWorkplaces()
        {
            return await _context.Workplaces
                .OrderBy(w => w.Name)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Position>> GetPositionsByWorkplaceID(int workplaceID)
        {
            return await _context.Positions
                .Where(p => p.WorkplaceID == workplaceID)
                .OrderBy(p => p.Name)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Position> GetPositionByID(int id)
        {
            return await _context.Positions
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task<IEnumerable<JobApplication>> GetPendingJobApplications()
        {
            return await _context.JobApplications
                .Include(j => j.Position)
                    .ThenInclude(p => p.Workplace)
                .Where(j => j.JobApplicationAnswerID == null)
                .OrderBy(j => j.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<JobApplication> GetPendingJobApplicationByID(int id)
        {
            return await _context.JobApplications
                .Where(j => j.JobApplicationAnswerID == null)
                .FirstOrDefaultAsync(p => p.ID == id);
        }

        public void InsertJobApplication(JobApplication jobApplication)
        {
            _context.Add(jobApplication);
        }

        public void InsertJobApplicationAnswer(JobApplicationAnswer jobApplicationAnswer)
        {
            _context.Add(jobApplicationAnswer);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
