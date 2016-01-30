using Canducci.EntityFramework.Repository.Util;
using PagedList;
using System.Web.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class NoticesDefaultController : Controller
    {
        public readonly RepositoryNoticeContract repository;
        public NoticesDefaultController(RepositoryNoticeContract repository)
        {
            this.repository = repository;
        }
        //protected override void Dispose(bool disposing)
        //{
        //    if (repository != null)
        //    {
        //        repository.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        [AcceptVerbs("GET", "POST")]
        public ActionResult Index(int? page, string filter)
        {   

            ViewBag.filter = filter;
            IPagedList<Notice> model = null;
            if (!string.IsNullOrEmpty(filter))
            {
                model = repository.Pagination<ConfigurationOrderByWherePagination<Notice>>(x =>
                    {
                        x.Where.Add(w => w.Title.Contains(filter));
                        x.OrderBy.Add(o => o.Id);
                        x.Page = page ?? 1;
                        x.Total = 2;

                    });
                return View("~/Views/Notices/Index.cshtml", model);
            }
            model = repository.Pagination<ConfigurationOrderByPagination<Notice>>(x =>
            {                
                x.OrderBy.Add(o => o.Id);
                x.Page = page ?? 1;
                x.Total = 2;
            });
            return View("~/Views/Notices/Index.cshtml", model);
        }

        [HttpGet()]
        public ActionResult Create()
        {
            return View("~/Views/Notices/Create.cshtml");
        }

        [HttpPost()]
        public ActionResult Create(Notice notice)
        {
            repository.Add(notice);
            return RedirectToAction("Index");
        }

        [HttpGet()]
        public ActionResult Edit(int id)
        {
            return View("~/Views/Notices/Edit.cshtml", repository.Find(id));
        }

        [HttpPost()]
        public ActionResult Edit(int id, Notice notice)
        {
            repository.Edit(notice);
            return RedirectToAction("Index");
        }

        [HttpGet()]
        public ActionResult Delete(int id)
        {
            return View("~/Views/Notices/Delete.cshtml", repository.Find(id));
        }

        [HttpPost()]
        public ActionResult Delete(int id, Notice notice)
        {
            repository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}