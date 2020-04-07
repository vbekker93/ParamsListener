using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParamsListenerUpdateService.Contexts;
using Infrastructure.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ParamsListenerUpdateService.Controllers
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

        // PUT: api/ParamsEntities/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParamsEntity(long id, ParamsEntity paramsEntity)
        {
            if (id != paramsEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(paramsEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParamsEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ParamsEntities
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ParamsEntity>> PostParamsEntity(ParamsEntity paramsEntity)
        {
            _context.ParamsEntities.Add(paramsEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParamsEntity", new { id = paramsEntity.Id }, paramsEntity);
        }

        // DELETE: api/ParamsEntities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ParamsEntity>> DeleteParamsEntity(long id)
        {
            var paramsEntity = await _context.ParamsEntities.FindAsync(id);
            if (paramsEntity == null)
            {
                return NotFound();
            }

            _context.ParamsEntities.Remove(paramsEntity);
            await _context.SaveChangesAsync();

            return paramsEntity;
        }

        private bool ParamsEntityExists(long id)
        {
            return _context.ParamsEntities.Any(e => e.Id == id);
        }
    }
}