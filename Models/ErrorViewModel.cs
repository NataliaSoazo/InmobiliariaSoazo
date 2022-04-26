using System;
using Project = InmobiliariaSoazo;
namespace InmobiliariaSoazo.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
 