using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaSoazo.Models
{
    public class Pagos
    {
        [Display(Name = "Contrato")]
        [ForeignKey(nameof(IdContrato))]
        public int IdContrato { get; set; }
        [Required]
        public int NroPago { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public decimal Importe { get; set; }

        [Display(Name = "Código Y Datos del Locatario")]
        public Contrato Datos { get; set; }

    }
}
