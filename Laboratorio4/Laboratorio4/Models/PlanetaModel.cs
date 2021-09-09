using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Laboratorio4.Models
{
    public class PlanetaModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Es necesario que le indique el nombre del planeta")]
        [Display(Name = "Ingrese el nombre del planeta")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Es necesario que le indique el tipo del planeta")]
        [Display(Name = "Seleccione el tipo de planeta")]
        public string tipo { get; set; }

        [Required(ErrorMessage = "Es necesario que ingrese el numero de anillos")]
        [Display(Name = "Ingrese el numero de anillos del planeta")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Debe ingresar numeros")]
        public int numeroAnillos { get; set; }

        [Required(ErrorMessage = "Debe agregar un archivo (imagen, pdf, etc...)")]
        [Display(Name = "Ingrese el archivo con los detalles del planeta;")]
        public HttpPostedFileBase archivo { get; set; }

        public string tipoArchivo { get; set; }
    }
}