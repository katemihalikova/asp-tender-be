using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_tender_be.Models;
using Microsoft.EntityFrameworkCore;

namespace asp_tender_be.Data
{
    public class TenderContext : DbContext
    {
        public TenderContext(DbContextOptions<TenderContext> options) : base(options)
        {
        }

        public DbSet<Workplace> Workplaces { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<JobApplicationAnswer> JobApplicationAnswer { get; set; }
    }
}
