using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Cursos.Models
{
    public partial class Estudiante
    {
        public Estudiante()
        {
            Matricula = new HashSet<Matricula>();
        }

        [Key]
        public int IdEstudiante { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "El codigo es obligatorio")]
        [MinLength(4, ErrorMessage = "El codigo debe ser minimo de 10 caracteres")]
        [MaxLength(10, ErrorMessage = "El codigo debe ser minimo de 10 caracteres")]
        public string Codigo { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MinLength(3, ErrorMessage = "El nombre debe ser minimo de 3 caracteres")]
        [MaxLength(50, ErrorMessage = "El nombre debe ser minimo de 50 caracteres")]
        public string Nombre { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "El apellido es obligatorio")]
        [MinLength(3, ErrorMessage = "El apellido debe ser minimo de 3 caracteres")]
        [MaxLength(50, ErrorMessage = "El apellido debe ser minimo de 50 caracteres")]
        public string Apellido { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string NombreApellido { get; set; }


        [Column(TypeName = "date")]
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date, ErrorMessage = "La fecha no es valida")]
        public DateTime? FechaNacimiento { get; set; }


        [JsonIgnore]
        public virtual ICollection<Matricula> Matricula { get; set; }
    }
}
