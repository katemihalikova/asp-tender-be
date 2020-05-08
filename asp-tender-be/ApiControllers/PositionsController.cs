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
    public class PositionsController : ControllerBase
    {
        private IRepository _repository;

        public PositionsController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Positions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Position>>> GetPositions(int? workplaceID)
        {
            if (workplaceID == null)
            {
                return BadRequest(new { error = "Parameter 'workplaceID' is required." });
            }

            return Ok(await _repository.GetPositionsByWorkplaceID(workplaceID.Value));
        }
    }
}
