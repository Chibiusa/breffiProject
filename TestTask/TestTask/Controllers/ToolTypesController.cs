using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestTask.ModelsFromDB;

namespace TestTask.Controllers
{
    public class ToolTypesController : Controller
    {
        private EquipmentDBEntities db = new EquipmentDBEntities();

        // GET: ToolTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.ToolType.ToListAsync());
        }

        // GET: ToolTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToolType toolType = await db.ToolType.FindAsync(id);
            if (toolType == null)
            {
                return HttpNotFound();
            }
            return View(toolType);
        }

        // GET: ToolTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToolTypes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdTool,Name,Description")] ToolType toolType)
        {
            if (ModelState.IsValid)
            {
                db.ToolType.Add(toolType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(toolType);
        }

        // GET: ToolTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToolType toolType = await db.ToolType.FindAsync(id);
            if (toolType == null)
            {
                return HttpNotFound();
            }
            return View(toolType);
        }

        // POST: ToolTypes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdTool,Name,Description")] ToolType toolType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toolType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(toolType);
        }

        // GET: ToolTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToolType toolType = await db.ToolType.FindAsync(id);
            if (toolType == null)
            {
                return HttpNotFound();
            }
            return View(toolType);
        }

        // POST: ToolTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ToolType toolType = await db.ToolType.FindAsync(id);
            db.ToolType.Remove(toolType);
            await db.SaveChangesAsync();
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
