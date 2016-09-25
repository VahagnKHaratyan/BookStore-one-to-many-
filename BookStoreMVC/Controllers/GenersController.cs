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
    [Authorize]
    public class GenersController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: Geners
        public async Task<ActionResult> Index()
        {
            return View(await db.Geners.ToListAsync());
        }

        // GET: Geners/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Geners geners = await db.Geners.FindAsync(id);
            if (geners == null)
            {
                return HttpNotFound();
            }
            return View(geners);
        }

        // GET: Geners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Geners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Gener")] Geners geners)
        {
            if (ModelState.IsValid)
            {
                db.Geners.Add(geners);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(geners);
        }

        // GET: Geners/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Geners geners = await db.Geners.FindAsync(id);
            if (geners == null)
            {
                return HttpNotFound();
            }
            return View(geners);
        }

        // POST: Geners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Gener")] Geners geners)
        {
            if (ModelState.IsValid)
            {
                db.Entry(geners).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(geners);
        }

        // GET: Geners/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Geners geners = await db.Geners.FindAsync(id);
            if (geners == null)
            {
                return HttpNotFound();
            }
            return View(geners);
        }

        // POST: Geners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Geners geners = await db.Geners.FindAsync(id);
            db.Geners.Remove(geners);
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
