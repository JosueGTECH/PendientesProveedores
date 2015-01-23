using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PendientesProveedores.Models
{
    public class ProveedoresVM
    {
        public int proveedorSeleccionado { get; set; }
        public SelectList listaProveedores { get; set; }
    }
}