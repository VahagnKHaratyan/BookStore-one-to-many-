using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStoreEntity;

namespace BookStoreMVC.Controllers
{
    public class AttributesController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: Attributes
        public async Task<ActionResult> Index()
        {
            return View(await db.Attributes.ToListAsync());
        }

        // GET: Attributes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attributes attributes = await db.Attributes.FindAsync(id);
            if (attributes == null)
            {
                return HttpNotFound();
            }
            return View(attributes);
        }

        // GET: Attributes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Attributes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,AttrType,TableRef")] Attributes attributes)
        {
            if (ModelState.IsValid)
            {
                db.Attributes.Add(attributes);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(attributes);
        }

        // GET: Attributes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attributes attributes = await db.Attributes.FindAsync(id);
            if (attributes == null)
            {
                return HttpNotFound();
            }
            return View(attributes);
        }

        // POST: Attributes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,AttrType,TableRef")] Attributes attributes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attributes).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(attributes);
        }

        // GET: Attributes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attributes attributes = await db.Attributes.FindAsync(id);
            if (attributes == null)
            {
                return HttpNotFound();
            }
            return View(attributes);
        }

        // POST: Attributes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Attributes attributes = await db.Attributes.FindAsync(id);
            db.Attributes.Remove(attributes);
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
