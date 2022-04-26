using System.ComponentModel.DataAnnotations;

namespace InmobiliariaSoazo.Models
{
    public class Propietario
    {
      
        [Display(Name = "Código")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Min 3 caracteres, máximo 50")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Min 3 caracteres, máximo 50")]
        public string Apellido { get; set; }
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Min 10 caracteres, máximo 50")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string Dni { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        [StringLength(25, MinimumLength = 7, ErrorMessage = "Min 7 caracteres")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        [StringLength(50, MinimumLength = 15, ErrorMessage = "Min 15 caracteres")]
        public string Email { get; set; }
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Min 5 caracteres")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public string Domicilio { get; set; }

    }
}
