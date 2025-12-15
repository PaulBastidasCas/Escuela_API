using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Escuela.Modelos;

    public class EscuelaApiContext : DbContext
    {
        public EscuelaApiContext (DbContextOptions<EscuelaApiContext> options)
            : base(options)
        {
        }

        public DbSet<Escuela.Modelos.Materia> Materias { get; set; } = default!;

public DbSet<Escuela.Modelos.Estudiante> Estudiantes { get; set; } = default!;

public DbSet<Escuela.Modelos.Institucion> Instituciones { get; set; } = default!;
    }
