using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Cursos.Models
{
    public partial class Periodo
    {
        public Periodo()
        {
            Matricula = new HashSet<Matricula>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdPeriodo { get; set; }
        [Required(ErrorMessage = "El año es obligatorio")]
        [Range(200, 2999, ErrorMessage = "El año debe estar comprendido entre el 2000 y el 2999")]
        public int Anio { get; set; }
        public bool?Estado { get; set; }

        [JsonIgnore]
        public virtual ICollection<Matricula> Matricula { get; set; }
    }
}
