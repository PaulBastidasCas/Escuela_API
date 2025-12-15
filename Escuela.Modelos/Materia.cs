using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuela.Modelos
{
    public class Materia
    {
        [Key]public int ID { get; set; }
        public string Nombre { get; set; }
        public int NivelMateria { get; set; }

        //FK
        public int? EstudianteID { get; set; }

        //Navegacion
        [ForeignKey("EstudianteID")]
        public Estudiante? Estudiante { get; set; }
    }
}
