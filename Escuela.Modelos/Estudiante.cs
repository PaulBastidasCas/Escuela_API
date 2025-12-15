using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuela.Modelos
{
    public class Estudiante
    {
        [Key] public int Id { get; set; }
        public string Nombre { get; set; }
        public int Nivel { get; set; }

        //FK
        public int InstitucionId { get; set; }

        //Navegacion
        [ForeignKey("InstitucionId")]
        public Institucion? Institucion { get; set; }

        //Relacion
        public List<Materia>? Materias { get; set; }

    }
}
