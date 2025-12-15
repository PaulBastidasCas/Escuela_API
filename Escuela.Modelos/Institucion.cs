using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuela.Modelos
{
    public class Institucion
    {
        [Key] public int Id { get; set; }
        public string Nombre { get; set; }

        //Navegacion
        public List<Estudiante>? Estudiantes { get; set; }
    }
}
