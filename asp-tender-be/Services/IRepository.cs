﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using asp_tender_be.Models;

namespace asp_tender_be.Services
{
    public interface IRepository
    {
        Task<IEnumerable<Workplace>> GetWorkplaces();
        Task<IEnumerable<Position>> GetPositionsByWorkplaceID(int workplaceID);
        Task<Position> GetPositionByID(int positionID);
        Task<IEnumerable<JobApplication>> GetPendingJobApplications();
        Task<JobApplication> GetJobApplicationByID(int id);
        void InsertJobApplication(JobApplication jobApplication);
        Task Save();
    }
}
