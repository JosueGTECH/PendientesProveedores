using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PendientesProveedores.Models
{
    public class ProveedoresVM
    {
        [NotMapped]
        public int proveedorSeleccionado { get; set; }
        [NotMapped]
        public SelectList listaProveedores { get; set; }
    }
}