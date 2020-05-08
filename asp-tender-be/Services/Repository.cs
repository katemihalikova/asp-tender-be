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

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
