using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cursos.Models
{
    public partial class Curso
    {
        public Curso()
        {
            InscripcionCurso = new HashSet<InscripcionCurso>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdCurso { get; set; }

        [StringLength(10)]
        [MinLength(2, ErrorMessage = "El codigo debe ser minimo de 2 caracteres")]
        [MaxLength(10, ErrorMessage = "El codigo debe ser minimo de 10 caracteres")]
        public string Codigo { get; set; }

        [StringLength(100)]
        [MinLength(5, ErrorMessage = "La descripcion debe ser minimo de 5 caracteres")]
        [MaxLength(100, ErrorMessage = "La descripcion debe ser minimo de 100 caracteres")]
        public string Descripcion { get; set; }
        
        public bool? Estado { get; set; }

        public virtual ICollection<InscripcionCurso> InscripcionCurso { get; set; }
    }
}
