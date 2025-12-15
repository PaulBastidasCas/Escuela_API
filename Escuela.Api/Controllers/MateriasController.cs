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
    public class MateriasController : ControllerBase
    {
        private readonly EscuelaApiContext _context;

        public MateriasController(EscuelaApiContext context)
        {
            _context = context;
        }

        // GET: api/Materias
        [HttpGet]
        public async Task<ActionResult<ApiResult<List<Materia>>>> GetMateria()
        {
            try
            {
                var data = await _context.Materias.ToListAsync();
                Log.Information($"Data OK \n {data}");
                return ApiResult<List<Materia>>.Ok(data);
            }
            catch (Exception ex)
            {
                Log.Information(ex.Message);
                return ApiResult<List<Materia>>.Fail(ex.Message);
            }
        }

        // GET: api/Materias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<Materia>>> GetMateria(int id)
        {
            try
            {
                var materia = await _context.Materias
                    .Include(e => e.Estudiante)
                    .FirstOrDefaultAsync(e => e.ID == id);

                if (materia == null)
                {
                    Log.Information("Datos no encontrados");
                    return ApiResult<Materia>.Fail("Datos no encontrados");
                }
                Log.Information($"La materia {materia} \n Esta Ok");
                return ApiResult<Materia>.Ok(materia);
            }
            catch (Exception ex)
            {
                Log.Information(ex.Message);
                return ApiResult<Materia>.Fail(ex.Message);
            }
        }

        // PUT: api/Materias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<Materia>>> PutMateria(int id, Materia materia)
        {
            if (id != materia.ID)
            {
                Log.Information("No coinciden los identificadores");
                return ApiResult<Materia>.Fail("No coinciden los identificadores");
            }

            _context.Entry(materia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!MateriaExists(id))
                {
                    Log.Information("Datos no encontrados");
                    return ApiResult<Materia>.Fail("Datos no encontrados");
                }
                else
                {
                    Log.Information(ex.Message);
                    return ApiResult<Materia>.Fail(ex.Message);
                }
            }
            Log.Information("Esta OK");
            return ApiResult<Materia>.Ok(null);
        }

        // POST: api/Materias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResult<Materia>>> PostMateria(Materia materia)
        {
            try
            {
                _context.Materias.Add(materia);
                await _context.SaveChangesAsync();
                Log.Information($"La materia {materia.ID}, {materia.Nombre}, {materia.NivelMateria}");
                return ApiResult<Materia>.Ok(materia);
            }
            catch (Exception ex) 
            {
                Log.Information(ex.Message);
                return ApiResult<Materia>.Fail(ex.Message);
            }
        }

        // DELETE: api/Materias/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<Materia>>> DeleteMateria(int id)
        {
            try
            {
                var materia = await _context.Materias.FindAsync(id);
                if (materia == null)
                {
                    Log.Information($"Datos con id {id} no encontrada");
                    return ApiResult<Materia>.Fail("Datos no encontrados");
                }

                _context.Materias.Remove(materia);
                await _context.SaveChangesAsync();

                Log.Information($"Materia con id {id} borrada exitosamente");
                return ApiResult<Materia>.Ok(null);
            }
            catch (Exception ex)
            {
                Log.Information(ex.Message);
                return ApiResult<Materia>.Fail(ex.Message);
            }      
        }

        private bool MateriaExists(int id)
        {
            return _context.Materias.Any(e => e.ID == id);
        }
    }
}
