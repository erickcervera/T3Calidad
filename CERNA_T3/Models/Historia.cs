using System;
using System.ComponentModel.DataAnnotations;

namespace CERNA_T3.Models
{
    public class Historia
    {
        public int Codigo { get; set; }

        public DateTime FechaRegistro { get; set; }
        [Required(ErrorMessage = "Obligatorio")]
        public string Mascota { get; set; }
        [Required(ErrorMessage = "Obligatorio")]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "Obligatorio")]
        public decimal Tamano { get; set; }
        public string DatosParticulares { get; set; }
        [Required(ErrorMessage = "Obligatorio")]
        public string Dueno { get; set; }
        [Required(ErrorMessage = "Obligatorio")]
        public string DuenoDireccion { get; set; }
        [Required(ErrorMessage = "Obligatorio")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Obligatorio")]
        [EmailAddress(ErrorMessage = "Correo invalido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Obligatorio")]
        public int IdSexo { get; set; }
        [Required(ErrorMessage = "Obligatorio")]
        public int IdRaza { get; set; }
        [Required(ErrorMessage = "Obligatorio")]
        public int IdEspecie { get; set; }
        public Sexo Sexo { get; set; }
        public Especie Especie { get; set; }
        public Raza Raza { get; set; }
    }
}
