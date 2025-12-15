using Escuela.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace Escuela.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        private readonly EscuelaApiContext _context;

        public EstudiantesController(EscuelaApiContext context)
        {
            _context = context;
        }

        // GET: api/Estudiantes
        [HttpGet]
        public async Task<ActionResult<ApiResult<List<Estudiante>>>> GetEstudiante()
        {
            try
            {
                var data = await _context.Estudiantes.ToListAsync();
                Log.Information($"Data OK \n {data}");
                return ApiResult<List<Estudiante>>.Ok(data);
            }
            catch (Exception ex)
            {
                Log.Information(ex.Message);
                return ApiResult<List<Estudiante>>.Fail(ex.Message);
            }
        }

        // GET: api/Estudiantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<Estudiante>>> GetEstudiante(int id)
        {
            try
            {
                var estudiante = await _context.Estudiantes
                    .Include(e => e.Institucion)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (estudiante == null)
                {
                    Log.Information("Datos no encontrados");
                    return ApiResult<Estudiante>.Fail("Datos no encontrados");
                }
                Log.Information($"La materia {estudiante} \n Esta Ok");
                return ApiResult<Estudiante>.Ok(estudiante);
            }
            catch (Exception ex)
            {
                Log.Information(ex.Message);
                return ApiResult<Estudiante>.Fail(ex.Message); 
            }
        }

        // PUT: api/Estudiantes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Estudiante>>> PutEstudiante(int id, Estudiante estudiante)
        {
            if (id != estudiante.Id)
            {
                Log.Information("No coinciden los identificadores");
                return ApiResult<Estudiante>.Fail("No coinciden los identificadores");
            }

            _context.Entry(estudiante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!EstudianteExists(id))
                {
                    Log.Information("Datos no encontrados");
                    return ApiResult<Estudiante>.Fail("Datos no encontrados");
                }
                else
                {
                    Log.Information(ex.Message);
                    return ApiResult<Estudiante>.Fail(ex.Message);
                }
            }
            Log.Information("Esta OK");
            return ApiResult<Estudiante>.Ok(null);
        }

        // POST: api/Estudiantes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResult<Estudiante>>> PostEstudiante(Estudiante estudiante)
        {
            try
            {
                _context.Estudiantes.Add(estudiante);
                await _context.SaveChangesAsync();
                Log.Information($"El Estudiante {estudiante}");
                return ApiResult<Estudiante>.Ok(estudiante);
            }
            catch (Exception ex)
            {
                Log.Information(ex.Message);
                return ApiResult<Estudiante>.Fail(ex.Message);
            }
        }

        // DELETE: api/Estudiantes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Estudiante>>> DeleteEstudiante(int id)
        {
            try
            {
                var estudiante = await _context.Estudiantes.FindAsync(id);
                if (estudiante == null)
                {
                    Log.Information("Datos no encontrados");
                    return ApiResult<Estudiante>.Fail("Datos no encontrados");
                }

                _context.Estudiantes.Remove(estudiante);
                await _context.SaveChangesAsync();
                Log.Information($"Estudiante con id {id} borrado exitosamente");
                return ApiResult<Estudiante>.Ok(null);
            }
            catch (Exception ex)
            {
                Log.Information(ex.Message);
                return ApiResult<Estudiante>.Fail(ex.Message);
            }
        }

        private bool EstudianteExists(int id)
        {
            return _context.Estudiantes.Any(e => e.Id == id);
        }
    }
}
