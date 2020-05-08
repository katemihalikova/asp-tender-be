using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using asp_tender_be.Data;
using asp_tender_be.Models;
using asp_tender_be.Services;

namespace asp_tender_be.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkplacesController : ControllerBase
    {
        private IRepository _repository;

        public WorkplacesController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Workplaces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workplace>>> GetWorkplaces()
        {
            return Ok(await _repository.GetWorkplaces());
        }
    }
}
