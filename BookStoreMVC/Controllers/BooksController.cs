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
using PagedList;
using System.IO;

namespace BookStoreMVC.Controllers
{
    public class BooksController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: Books
        public async Task<ActionResult> Index(string searchString, string sortOption, int page = 1, string pageType = "table")
        {
            
            var books = db.Books.Include(b => b.Authors).Include(b => b.Countries).Include(b => b.Geners);
            if (!String.IsNullOrEmpty(searchString))
            {

                books = db.Books.Where(p => p.Title.ToLower().Contains(searchString) || (p.Authors.FirstName.ToLower() + " " + p.Authors.LastName.ToLower()).Contains(searchString));
            }
            switch (sortOption)
            {
                case "Title_asc":
                    books = books.OrderBy(p => p.Title);
                    break;
                case "Title_desc":
                    books = books.OrderByDescending(p => p.Title);
                    break;
                case "Price_asc":
                    books = books.OrderBy(p => p.Price);
                    break;
                case "Price_desc":
                    books = books.OrderByDescending(p => p.Price);
                    break;

                case "PageCount_asc":
                    books = books.OrderBy(p => p.PageCount);
                    break;
                case "PageCount_desc":
                    books = books.OrderByDescending(p => p.PageCount);
                    break;
                case "FirstName_asc":
                    books = books.OrderBy(p => p.Authors.FirstName);
                    break;
                case "FirstName_desc":
                    books = books.OrderByDescending(p => p.Authors.FirstName);
                    break;
                case "LastName_asc":
                    books = books.OrderBy(p => p.Authors.LastName);
                    break;
                case "LastName_desc":
                    books = books.OrderByDescending(p => p.Authors.LastName);
                    break;
                case "Name_asc":
                    books = books.OrderBy(p => p.CountryId);
                    break;
                case "Name_desc":
                    books = books.OrderByDescending(p => p.Countries.Name);
                    break;
                case "Gener_asc":
                    books = books.OrderBy(p => p.Geners.Gener);
                    break;
                case "Gener_desc":
                    books = books.OrderByDescending(p => p.Geners.Gener);
                    break;
                default:
                    books = books.OrderBy(p => p.ID);
                    break;

            }
            int pageSize;

            if (pageType == "table")
            {
                pageSize = 5;
                ViewBag.pageType = "table";
            }
            else
            {
                 pageSize = 10;
                ViewBag.pageType = "image";
            }

          if(page > books.Count()/ pageSize +1)
            {
                page = 1;
            }

            return Request.IsAjaxRequest()
                ? pageType == "image" ? (ActionResult)PartialView("_ImageList", books.ToPagedList(page, pageSize)) : (ActionResult)PartialView("_BookList", books.ToPagedList(page, pageSize))
                : View(books.ToPagedList(page, pageSize));
        }
       

        // GET: Books/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = await db.Books.FindAsync(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }
        [Authorize]
        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Authors, "ID", "FirstName");
            ViewBag.CountryId = new SelectList(db.Countries, "ID", "Name");
            ViewBag.GenerId = new SelectList(db.Geners, "ID", "Gener");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Title,Price,PageCount,BookDescription,Picture,CountryId,AuthorId,GenerId")] Books books, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null)
                {
                    var supportedTypes = new[] { "jpg", "jpeg", "png" };
                    var fileExt = System.IO.Path.GetExtension(upload.FileName).Substring(1);

                    if (!supportedTypes.Contains(fileExt))
                    {
                        return RedirectToAction("Index");
                        // ModelState.AddModelError("photo", "Invalid type. Only the following types (jpg, jpeg, png) are supported.");

                    }

                    string filename = Guid.NewGuid().ToString()+ "_" + Path.GetExtension(upload.FileName);
                    string path = Path.Combine(Server.MapPath("~/Upload"), filename);//("~/App_Data/Images"), filename);
                    upload.SaveAs(path);

                    books.Picture = filename;
                }
                else
                {
                    books.Picture = "defoult.png";
                }

                db.Books.Add(books);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Authors, "ID", "FirstName", books.AuthorId);
            ViewBag.CountryId = new SelectList(db.Countries, "ID", "Name", books.CountryId);
            ViewBag.GenerId = new SelectList(db.Geners, "ID", "Gener", books.GenerId);
            return View(books);
        }

        // GET: Books/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = await db.Books.FindAsync(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "ID", "FirstName", books.AuthorId);
            ViewBag.CountryId = new SelectList(db.Countries, "ID", "Name", books.CountryId);
            ViewBag.GenerId = new SelectList(db.Geners, "ID", "Gener", books.GenerId);
            return View(books);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Title,Price,PageCount,BookDescription,Picture,CountryId,AuthorId,GenerId")] Books books, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                 if (upload != null)
                 {
                     var supportedTypes = new[] { "jpg", "jpeg", "png" };
                     var fileExt = System.IO.Path.GetExtension(upload.FileName).Substring(1);

                     if (!supportedTypes.Contains(fileExt))
                     {
                         return RedirectToAction("Index");
                         // ModelState.AddModelError("photo", "Invalid type. Only the following types (jpg, jpeg, png) are supported.");

                     }

                     string filename = Guid.NewGuid().ToString() + "_" + Path.GetExtension(upload.FileName);
                     string path = Path.Combine(Server.MapPath("~/Upload"), filename);//("~/App_Data/Images"), filename);
                     upload.SaveAs(path);
                     string oldFilePath = Path.Combine(Server.MapPath("~/Upload"), books.Picture);

                     if (System.IO.File.Exists(oldFilePath) && books.Picture != "defoult.png")
                     {

                         System.IO.File.Delete(oldFilePath);
                     }
                     books.Picture = filename;

                 }
                 
                db.Entry(books).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "ID", "FirstName", books.AuthorId);
            ViewBag.CountryId = new SelectList(db.Countries, "ID", "Name", books.CountryId);
            ViewBag.GenerId = new SelectList(db.Geners, "ID", "Gener", books.GenerId);
            return View(books);
        }

        // GET: Books/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            Books books = await db.Books.FindAsync(id);
            db.Books.Remove(books);
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
