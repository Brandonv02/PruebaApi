using Cursos.Helper;
using Cursos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrandonPrueba.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeriodosController:ControllerBase
    {
        private readonly CursosCTX ctx;
        public PeriodosController(CursosCTX _ctx)
        {
            ctx = _ctx;
        }

        public async Task<IEnumerable<Periodo>> Get()
        {
            return await ctx.Periodo.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var Periodo = await ctx.Periodo.FindAsync(id);
            if (Periodo == null)
            {
                return NotFound(ErrorHelper.Response(404, $"El periodo {id} no existe."));
            }

            return Ok(Periodo);
        }

        [HttpGet("Activo")]
        public async Task<IActionResult> GetActivo()
        {
            var Periodo = await ctx.Periodo.Where(x => x.Estado == true).OrderByDescending(x => x.Anio).FirstOrDefaultAsync();
            if(Periodo == null)
            {
                return NotFound(ErrorHelper.Response(404, "No existen periodos abiertos."));
            }
            return Ok(Periodo);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Periodo Periodo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
            }
            if (await ctx.Periodo.Where(x => x.Anio == Periodo.Anio).AnyAsync())
            {
                return BadRequest(ErrorHelper.Response(400, $"El año {Periodo.Anio} ya existe."));
            }

            ctx.Periodo.Add(Periodo);
            await ctx.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = Periodo.IdPeriodo }, Periodo);

        }
        [HttpPatch("activar/{id}")]
        public async Task<IActionResult> Activar(int id)
        {
            using (var transaction = ctx.Database.BeginTransaction())
            {
                try
                {
                    var Periodo = await ctx.Periodo.FindAsync(id);
                    if (Periodo == null)
                    {
                        return NotFound(ErrorHelper.Response(404, $"No existe el periodo {id}."));
                    }

                    if (Periodo.Estado.Value)
                    {
                        await transaction.RollbackAsync();
                        return NoContent();
                    }
                    else
                    {
                        var Periodos = await ctx.Periodo.Where(x => x.IdPeriodo != id).ToListAsync();
                        Periodos.ForEach(x => x.Estado = false);
                        Periodo.Estado = true;
                        await ctx.SaveChangesAsync();
                        await transaction.CommitAsync();
                        return Ok();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(500, ErrorHelper.Response(500, "Ha ocurrido un error en la base de datos."));
                }
            }
        }
        [HttpPatch("desactivar/{id}")]
        public async Task<IActionResult> Desactivar(int id)
        {
            var Periodo = await ctx.Periodo.FindAsync(id);
            if (Periodo == null)
            {
                return NotFound(ErrorHelper.Response(404, $"No existe el periodo {id}."));
            }

            if (!Periodo.Estado.Value)
            {
                return NoContent();
            }
            else
            {
                Periodo.Estado = false;
                await ctx.SaveChangesAsync();
                return Ok();
            }
        }
    }
}
