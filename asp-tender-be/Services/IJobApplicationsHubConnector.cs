﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using asp_tender_be.Models;

namespace asp_tender_be.Services
{
    public interface IJobApplicationsHubConnector
    {
        Task RefreshOverviewViaHub();
    }
}
