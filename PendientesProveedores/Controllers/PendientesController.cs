using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PendientesProveedores.Models;
using System.Data.Entity.Validation;

namespace PendientesProveedores.Controllers
{
    public class PendientesController : Controller
    {
        private VIA_DREntities db = new VIA_DREntities();

        // GET: Pendientes
        public ActionResult Index()
        {
            
            return View(db.Local_Pendientes_Proveedores.ToList());
        }

        // GET: Pendientes/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Local_Pendientes_Proveedores local_Pendientes_Proveedores = db.Local_Pendientes_Proveedores.Find(id);
            if (local_Pendientes_Proveedores == null)
            {
                return HttpNotFound();
            }
            return View(local_Pendientes_Proveedores);
        }

        // GET: Pendientes/Create
        public ActionResult Create()
        {
            //SELECT L.Grupo, L.id  FROM CONVENIOS_UTILITY AS [U] JOIN Local_Grupo_Convenio AS [L] ON U.LOCAL_GRUPO_CONVENIO = L.id group by l.Grupo, l.id

            var proveedores = from l in db.Local_Grupo_Convenio join u in db.CONVENIOS_UTILITY on l.id equals u.LOCAL_GRUPO_CONVENIO select l.Grupo;
            var viewModel = new Local_Pendientes_Proveedores();
            viewModel.listaProveedores = new SelectList(proveedores);

            return View(viewModel);
        }

        // POST: Pendientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Local_Pendientes_Proveedores local_Pendientes_Proveedores)
        {
            //Para ver los posibles errores
            var errores = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                try
                {
                    //agregar estado por default 'NO PAGADA'
                    string estadoDefault = "NO PAGADO";
                    string proveedorSeleccionado = local_Pendientes_Proveedores.proveedorSeleccionado;

                    local_Pendientes_Proveedores.ESTADO = estadoDefault;
                    
                    //buscar proveedor por proveedorSeleccionado
                    var proveedor = from l in db.Local_Grupo_Convenio where l.Grupo == proveedorSeleccionado select l.id; 

                    //asignar proveedor
                    local_Pendientes_Proveedores.UTILITY_ID = proveedor.First();
                    db.Local_Pendientes_Proveedores.Add(local_Pendientes_Proveedores);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }

            }

            return View(local_Pendientes_Proveedores);
        }

        // GET: Pendientes/Edit/5
        public ActionResult Edit(int id)
        {
            //Para ver los posibles errores
            var errores = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Local_Pendientes_Proveedores local_Pendientes_Proveedores = db.Local_Pendientes_Proveedores.Find(id);
                if (local_Pendientes_Proveedores == null)
                {
                    return HttpNotFound();
                }
                return View(local_Pendientes_Proveedores);
            }
            return View();
        }

        // POST: Pendientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Local_Pendientes_Proveedores local_Pendientes_Proveedores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(local_Pendientes_Proveedores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(local_Pendientes_Proveedores);
        }

        // GET: Pendientes/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Local_Pendientes_Proveedores local_Pendientes_Proveedores = db.Local_Pendientes_Proveedores.Find(id);
            if (local_Pendientes_Proveedores == null)
            {
                return HttpNotFound();
            }
            return View(local_Pendientes_Proveedores);
        }

        // POST: Pendientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime id)
        {
            Local_Pendientes_Proveedores local_Pendientes_Proveedores = db.Local_Pendientes_Proveedores.Find(id);
            db.Local_Pendientes_Proveedores.Remove(local_Pendientes_Proveedores);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
