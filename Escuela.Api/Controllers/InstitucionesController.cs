using Escuela.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Escuela.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitucionesController : ControllerBase
    {
        private readonly EscuelaApiContext _context;

        public InstitucionesController(EscuelaApiContext context)
        {
            _context = context;
        }

        // GET: api/Instituciones
        [HttpGet]
        public async Task<ActionResult<ApiResult<List<Institucion>>>> GetEscuela()
        {
            try
            {
                var data = await _context.Instituciones.ToListAsync();
                Log.Information($"Data OK \n {data}");
                return ApiResult<List<Institucion>>.Ok(data);
            }
            catch (Exception ex)
            {
                Log.Information(ex.Message);
                return ApiResult<List<Institucion>>.Fail(ex.Message);
            }
        }

        // GET: api/Instituciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<Institucion>>> GetInstitucion(int id)
        {
            try
            {
                var institucion = await _context.Instituciones
                    .Include(e => e.Estudiantes)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (institucion == null)
                {
                    Log.Information("Datos no encontrados");
                    return ApiResult<Institucion>.Fail("Datos no encontrados");
                }

                Log.Information($"La materia {institucion} \n Esta Ok");
                return ApiResult<Institucion>.Ok(institucion);
            }
            catch (Exception ex)
            {
                Log.Information(ex.Message);
                return ApiResult<Institucion>.Fail(ex.Message);
            }
        }

        // PUT: api/Instituciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Institucion>>> PutInstitucion(int id, Institucion institucion)
        {
            
            if (id != institucion.Id)
            {
                Log.Information("No coinciden los identificadores");
                return ApiResult<Institucion>.Fail("No coinciden los identificadores");
            }

            _context.Entry(institucion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!InstitucionExists(id))
                {
                    Log.Information("Datos no encontrados");
                    return ApiResult<Institucion>.Fail("Datos no encontrados");
                }
                else
                {
                    Log.Information(ex.Message);
                    return ApiResult<Institucion>.Fail(ex.Message);
                }
            }

            Log.Information("Esta OK");
            return ApiResult<Institucion>.Ok(null);
        }

        // POST: api/Instituciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResult<Institucion>>> PostInstitucion(Institucion institucion)
        {
            try
            {
                _context.Instituciones.Add(institucion);
                await _context.SaveChangesAsync();

                Log.Information($"El Estudiante {institucion}");
                return ApiResult<Institucion>.Ok(institucion);
            }
            catch (Exception ex)
            {
                Log.Information(ex.Message);
                return ApiResult<Institucion>.Fail(ex.Message);
            }
        }

        // DELETE: api/Instituciones/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Institucion>>> DeleteInstitucion(int id)
        {
            try
            {
                var institucion = await _context.Instituciones.FindAsync(id);
                if (institucion == null)
                {
                    Log.Information("Datos no encontrados");
                    return ApiResult<Institucion>.Fail("Datos no encontrados");
                }

                _context.Instituciones.Remove(institucion);
                await _context.SaveChangesAsync();

                Log.Information($"Institucion con id {id} borrada exitosamente");
                return ApiResult<Institucion>.Ok(null);
            }
            catch (Exception ex)
            {
                Log.Information(ex.Message);
                return ApiResult<Institucion>.Fail(ex.Message);
            }
        }

        private bool InstitucionExists(int id)
        {
            return _context.Instituciones.Any(e => e.Id == id);
        }
    }
}
