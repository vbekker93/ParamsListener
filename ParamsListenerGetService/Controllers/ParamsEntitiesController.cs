using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParamsListenerGetService.Contexts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParamsListenerGetService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParamsEntitiesController : ControllerBase
    {
        private readonly ParamsEntityContext _context;

        public ParamsEntitiesController(ParamsEntityContext context)
        {
            _context = context;
        }

        // GET: api/ParamsEntities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParamsEntity>>> GetParamsEntities()
        {
            return await _context.ParamsEntities.ToListAsync();
        }

        // GET: api/ParamsEntities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParamsEntity>> GetParamsEntity(long id)
        {
            var paramsEntity = await _context.ParamsEntities.FindAsync(id);

            if (paramsEntity == null)
            {
                return NotFound();
            }

            return paramsEntity;
        }
    }
}