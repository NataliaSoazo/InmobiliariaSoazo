using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaSoazo.Models
{
    public class Inmueble

    {
      
            [Display(Name = "Código")]
            public int Id { get; set; }

            [Required(ErrorMessage = "Campo obligatorio")]
            [StringLength(50, MinimumLength = 5, ErrorMessage = "Ingrese una dirección válida")]
    
            [Display(Name = "Dirección del inmueble")]
            public string Direccion { get; set; }


            [Required(ErrorMessage = "Campo obligatorio")]
            public int Ambientes { get; set; }

            [Required(ErrorMessage = "Campo obligatorio")]
            [StringLength(8, MinimumLength = 4, ErrorMessage = "Ingrese un uso válido válido")]
            public String Uso { get; set; }

            [Required(ErrorMessage = "Campo obligatorio")]
            [Display(Name = "Valuación en dólares")]
            public decimal Valor { get; set; }


            [Required(ErrorMessage = "Campo obligatorio")]
            public String Disponible { get; set; }
            [Display(Name = "Dueño")]
            [Required(ErrorMessage = "Campo obligatorio")]
            public int PropietarioId { get; set; }


            [ForeignKey(nameof(PropietarioId))]
            public Propietario Duenio { get; set; }
        }
    }


