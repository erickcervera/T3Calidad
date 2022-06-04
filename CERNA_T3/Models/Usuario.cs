using System.ComponentModel.DataAnnotations;

namespace CERNA_T3.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Password { get; set; }
        //public List<Historia> Historia { get; set; }
    }
}
