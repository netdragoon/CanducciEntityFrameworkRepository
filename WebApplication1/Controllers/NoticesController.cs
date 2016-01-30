using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication1.Models;
using System;
using System.Linq.Expressions;
using Canducci.EntityFramework.Repository.Util;
using PagedList;

namespace WebApplication1.Controllers
{
    public class NoticesController : Controller
    {
        public readonly RepositoryNoticeContract repository;
        public readonly RepositoryTagsContract repositoryTags;
        public NoticesController(RepositoryNoticeContract repository, RepositoryTagsContract repositoryTags)
        {
            this.repository = repository;
            this.repositoryTags = repositoryTags;
        }        
        
        [AcceptVerbs("GET","POST")]
        public async Task<ActionResult> Index(int? page, string filter)
        {

            repository.CreateAndAttach();
            int rows = 6;
            ViewBag.filter = filter;
            IPagedList<Notice> model = null;
            if (!string.IsNullOrEmpty(filter))
            {
                model = await repository.PaginationAsync<ConfigurationOrderByWherePagination<Notice>>(a =>
                    {
                        a.OrderBy.Add(x => x.Title, Order.Asc)
                                 .Add(x => x.Id);
                        a.Where.Add(w => w.Title.Contains(filter));
                        a.Page = page ?? 1;
                        a.Total = rows;
                    });
                return View(model);
            }
            model = await repository.PaginationAsync<ConfigurationOrderByPagination<Notice>>(a =>
                    {
                        a.OrderBy.Add(x => x.Title, Order.Asc)
                                 .Add(x => x.Id);                        
                        a.Page = page ?? 1;
                        a.Total = rows;
                    });
            return View(model);
        }

        public ActionResult Index2(int? page, string filter)
        {
            int rows = 2;
            Expression<Func<Notice, ViewModel>> select = x => new ViewModel() { Id = x.Id, Title = x.Title };
            ViewBag.filter = filter;
            if (!string.IsNullOrEmpty(filter))
            {
                return View(repository
                    .Pagination(x => x.Title.Contains(filter), x => x.Id, select, (page ?? 1), rows));
            }
            return View(repository.Pagination(x => x.Id, select, (page ?? 1), rows));
        }

        [HttpGet()]
        public ActionResult Create()
        {
            ViewBag.TagId = new SelectList(repositoryTags.List(x => x.Description), "Id", "Description");
            return View();
        }

        [HttpPost()]
        public async Task<ActionResult> Create(Notice notice)
        {        
            await repository.AddAsync(notice);
            return RedirectToAction("Index");
        }

        [HttpGet()]
        public async Task<ActionResult> Edit(int id)
        {
            Notice notice = await repository.FindAsync(id);
            ViewBag.TagId = new SelectList(repositoryTags.List(x => x.Description), "Id", "Description", notice.TagId);
            return View(notice);
        }

        [HttpPost()]
        public async Task<ActionResult> Edit(int id, Notice notice)
        {
            await repository.EditAsync(notice);            
            return RedirectToAction("Index");
        }

        [HttpGet()]
        public async Task<ActionResult> Delete(int id)
        {
            return View(await repository.FindAsync(id));
        }

        [HttpPost()]
        public async Task<ActionResult> Delete(int id, Notice notice)
        {
            await repository.DeleteAsync(id);
            return RedirectToAction("Index");
        }


        [HttpPost()]
        public async Task<ActionResult> DefaultSave()
        {
            Notice noticia = repository.Create();
            Tags tag = repositoryTags.Create();

            noticia.Active = true;
            noticia.Date = DateTime.Now.Date.AddDays(-5);
            noticia.TagId = tag.Id;
            noticia.Tags = tag;
            noticia.Texto = "testando instancia";
            noticia.Title = "testando instancia";            

            tag.Description = "Da Argentina";
            tag.Notice.Add(noticia);

            await repositoryTags.AddAsync(tag);

            return RedirectToAction("Index");
        }

    }
}