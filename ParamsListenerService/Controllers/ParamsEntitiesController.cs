using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParamsListenerService.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParamsListenerService.Controllers
{
    /// <summary>
    /// Контроллер сервиса получения/сохранения сущностей
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ParamsEntitiesController : ControllerBase
    {
        private readonly ParamsEntityContext _context;

        public ParamsEntitiesController(ParamsEntityContext context)
        {
            _context = context;
        }

        #region Методы получения сущностей внешними API

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParamsEntity>>> GetParamsEntities()
        {
            return await _context.ParamsEntities.ToListAsync();
        }

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

        #endregion Методы получения сущностей внешними API

        #region Методы изменения сущностей в БД, внешними API

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

        [HttpPost]
        public async Task<ActionResult<ParamsEntity>> PostParamsEntity(ParamsEntity paramsEntity)
        {
            _context.ParamsEntities.Add(paramsEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParamsEntity", new { id = paramsEntity.Id }, paramsEntity);
        }

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

        #endregion Методы изменения сущностей в БД, внешними API

        /// <summary>
        /// Проверка существования сущности в БД
        /// </summary>
        /// <param name="id">ИД сущности</param>
        /// <returns>Признак существования объекта в БД</returns>
        private bool ParamsEntityExists(long id)
        {
            return _context.ParamsEntities.Any(e => e.Id == id);
        }
    }
}