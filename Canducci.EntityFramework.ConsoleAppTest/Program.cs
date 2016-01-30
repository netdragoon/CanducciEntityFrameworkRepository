using Canducci.EntityFramework.ConsoleAppTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Canducci.EntityFramework.Repository.Util;
using Canducci.EntityFramework.Repository.Contracts.Interfaces;

namespace Canducci.EntityFramework.ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {

            BaseEFContext Context = new BaseEFContext();
            RepositoryNoticeContract rep = new RepositoryNotice(Context);
            Context.Database.Log = Log => TextSQL(Log);

            //Add(rep); ok
            //Create(rep); ok
            //Edit(rep); ok
            //Delete(rep); ok
            //Find(rep); ok
            //ToList(rep); ok
            //ToAll(rep); ok
            //ToPagination(rep); ok
            //ToGroupBy(rep); ok

            Action<ConfigurationOrderBy<Notice>> a = c =>
            {
                c.OrderBy.Add(x => x.TagId);
            };

            var c1 = rep.All(a);

            
            System.Console.ReadKey();

        }

        private static void TextSQL(string Log)
        {
            System.Console.WriteLine(Log);
        }

        private static void ToGroupBy(RepositoryNoticeContract rep)
        {
            var res1 = rep.GroupBy(a => a.TagId, a => new
            {
                TipoId = a.Key,
                Quantidade = a.Count()
            });


            IGroupOrderBy<Tipo> groupOrderBy =
                GroupOrderBy<Tipo>
                .Create()
                .Add(c => c.TipoId, Order.Asc)
                .Add(c => c.Quantidade, Order.Asc);

            IGroupWhere<Tipo> groupWhere =
                GroupWhere<Tipo>
                .Create(c => c.Quantidade > 1);

            IList<Tipo> res2
                = rep.GroupBy(groupOrderBy, groupWhere, n => n.TagId,
                s => new Tipo
                {
                    TipoId = s.Key,
                    Quantidade = s.Count()
                });

        }

        private static void ToPagination(RepositoryNoticeContract rep)
        {
            IPagedList<Notice> p1 = rep.Pagination(c => c.Id, 1, 10);
            IPagedList<ViewModel> p2 = rep.Pagination(c => c.Id, s => new ViewModel { Id = s.Id, Title = s.Title }, 1, 10);
            IPagedList<Notice> p3 = rep.Pagination(c => c.TagId > 0, c => c.Id, 1, 10);
            IPagedList<ViewModel> p4 = rep.Pagination(c => c.TagId > 0, c => c.Id, s => new ViewModel { Id = s.Id, Title = s.Title }, 1, 10);

            IGroupOrderBy<Notice> groupOrderBy = GroupOrderBy<Notice>.Create(x => x.Id, Order.Asc).Add(x => x.TagId, Order.Desc);
            IPagedList<Notice> p5 = rep.Pagination(groupOrderBy, 1, 10);
            IPagedList<Notice> p6 = rep.Pagination(groupOrderBy, c => c.TagId > 0, 1, 10);
            IPagedList<ViewModel> p7 = rep.Pagination(groupOrderBy, s => new ViewModel { Id = s.Id, Title = s.Title }, 1, 10);
            IPagedList<ViewModel> p8 = rep.Pagination(groupOrderBy, c => c.TagId > 0, s => new ViewModel { Id = s.Id, Title = s.Title }, 1, 10);

            ConfigurationOrderByPagination<Notice> configNotice = new ConfigurationOrderByPagination<Notice>();
            configNotice.OrderBy.Add(c => c.Active);
            configNotice.Page = 1;
            configNotice.Total = 2;

            ConfigurationOrderByPaginationSelect<Notice, ViewModel> configNoticeViewModel = new ConfigurationOrderByPaginationSelect<Notice, ViewModel>();
            configNoticeViewModel.OrderBy.Add(c => c.Active);
            configNoticeViewModel.Page = 1;
            configNoticeViewModel.Total = 2;
            configNoticeViewModel.Select.Set(s => new ViewModel { Id = s.Id, Title = s.Title });

            IPagedList<Notice> p9 = rep.Pagination(configNotice);
            IPagedList<ViewModel> p10 = rep.Pagination(configNoticeViewModel);

            IPagedList<Notice> p11 = rep.Pagination<ConfigurationOrderByPagination<Notice>>(a =>
            {
                a.OrderBy.Add(c => c.Active);
                a.Page = 1;
                a.Total = 2;
            });
            IPagedList<ViewModel> p12 = rep.Pagination<ConfigurationOrderByPaginationSelect<Notice, ViewModel>, ViewModel>(a =>
            {
                a.OrderBy.Add(c => c.Title);
                a.Page = 1;
                a.Total = 2;
                a.Select.Set(s => new ViewModel { Id = s.Id, Title = s.Title });
            });
        }

        private static void ToAll(RepositoryNoticeContract rep)
        {
            IEnumerable<Notice> n1 = rep.All();
            IEnumerable<Notice> n2 = rep.All(x => x.Active == true);
            IEnumerable<Notice> n3 = rep.All(x => x.Active);
            IEnumerable<Notice> n4 = rep.All(x => x.Active == true, x => x.Active);
            IEnumerable<ViewModel> n5 = rep.All(x => x.Active == true, x => x.Active, s => new ViewModel { Id = s.Id, Title = s.Title });

            IGroupOrderBy<Notice> groupOrderBy = GroupOrderBy<Notice>.Create(x => x.Id, Order.Asc).Add(x => x.TagId, Order.Desc);
            IEnumerable<Notice> n6 = rep.All(groupOrderBy);
            IEnumerable<Notice> n7 = rep.All(groupOrderBy, x => x.Active == true);
            IEnumerable<ViewModel> n8 = rep.All(groupOrderBy, x => x.Active == true, s => new ViewModel { Id = s.Id, Title = s.Title });

            ConfigurationOrderBy<Notice> configNotice = new ConfigurationOrderBy<Notice>();
            configNotice.OrderBy.Add(x => x.Id, Order.Asc).Add(x => x.TagId, Order.Desc);
            IEnumerable<Notice> n9 = rep.All(configNotice);

            ConfigurationOrderBySelect<Notice, ViewModel> configNoticeViewModel = new ConfigurationOrderBySelect<Notice, ViewModel>();
            configNoticeViewModel.OrderBy.Add(x => x.Id, Order.Asc).Add(x => x.TagId, Order.Desc);
            configNoticeViewModel.Select.Set(s => new ViewModel() { Id = s.Id, Title = s.Title });
            IList<ViewModel> n10 = rep.All(configNoticeViewModel);


            IEnumerable<Notice> n11 = rep.All<ConfigurationOrderByLimit<Notice>>(a =>
            {
                a.OrderBy.Add(x => x.Id);
                a.Page = 1;
                a.Total = 2;
            });
            IList<ViewModel> n12 = rep.All<ConfigurationOrderBySelect<Notice, ViewModel>, ViewModel>(a =>
            {
                a.OrderBy.Add(x => x.Id);
                a.Select.Set(s => new ViewModel { Id = s.Id, Title = s.Title });
            });
        }

        private static void ToList(RepositoryNoticeContract rep)
        {
            IList<Notice> n1 = rep.List();
            IList<Notice> n2 = rep.List(x => x.Active);
            IList<Notice> n3 = rep.List(x => x.Active == false);
            IList<Notice> n4 = rep.List(x => x.Active, 1, 10);
            IList<Notice> n5 = rep.List(c => c.Id > 0, x => x.Active.Value, true);
            IList<Notice> n6 = rep.List(c => c.Id > 0, x => x.Active.Value, 1);
            IList<ViewModel> n7 = rep.List(c => c.Id, x => new ViewModel { Id = x.Id, Title = x.Title });
            IList<ViewModel> n8 = rep.List(x => x.TagId > 0, c => c.Id, x => new ViewModel { Id = x.Id, Title = x.Title });
            IList<ViewModel> n9 = rep.List(x => x.TagId > 0, c => c.Id, x => new ViewModel { Id = x.Id, Title = x.Title }, 1);
            IList<ViewModel> n10 = rep.List(x => x.TagId > 0, c => c.Id, x => new ViewModel { Id = x.Id, Title = x.Title }, 1);
            IGroupOrderBy<Notice> groupOrderBy = GroupOrderBy<Notice>.Create(x => x.Id, Order.Asc).Add(x => x.TagId, Order.Desc);
            IList<Notice> n11 = rep.List(groupOrderBy);
            IList<Notice> n12 = rep.List(groupOrderBy, 1);
            IList<Notice> n13 = rep.List(groupOrderBy, x => x.Id > 0, true);
            IList<Notice> n14 = rep.List(groupOrderBy, x => x.Id > 0, 1);
            IList<ViewModel> n15 = rep.List(groupOrderBy, x => new ViewModel { Id = x.Id, Title = x.Title });
            IList<ViewModel> n16 = rep.List(groupOrderBy, x => x.TagId > 0, x => new ViewModel { Id = x.Id, Title = x.Title });
            IList<ViewModel> n17 = rep.List(groupOrderBy, x => x.TagId > 0, x => new ViewModel { Id = x.Id, Title = x.Title }, 1);
            ConfigurationOrderBy<Notice> configNotice = new ConfigurationOrderBy<Notice>();
            configNotice.OrderBy.Add(x => x.Id, Order.Asc).Add(x => x.TagId, Order.Desc);
            IList<Notice> n18 = rep.List(configNotice);
            ConfigurationOrderBySelect<Notice, ViewModel> configNoticeViewModel = new ConfigurationOrderBySelect<Notice, ViewModel>();
            configNoticeViewModel.OrderBy.Add(x => x.Id, Order.Asc).Add(x => x.TagId, Order.Desc);
            configNoticeViewModel.Select.Set(s => new ViewModel() { Id = s.Id, Title = s.Title });
            IList<ViewModel> n19 = rep.List(configNoticeViewModel);
            IList<Notice> n20 = rep.List<ConfigurationOrderByLimit<Notice>>(a =>
            {
                a.OrderBy.Add(x => x.Id);
                a.Page = 1;
                a.Total = 2;
            });
            IList<ViewModel> n21 = rep.List<ConfigurationOrderBySelect<Notice, ViewModel>, ViewModel>(a =>
            {
                a.OrderBy.Add(x => x.Id);
                a.Select.Set(s => new ViewModel { Id = s.Id, Title = s.Title });
            });
        }

        private static void Find(RepositoryNoticeContract rep)
        {
            Notice notFind = rep.Find(8);
            if (notFind.Id != 8)
                throw new Exception("Error", new FormatException());

        }

        private static void Delete(RepositoryNoticeContract rep)
        {
            Notice notDelete = rep.CreateAndAttach();
            notDelete.Active = true;
            notDelete.Date = DateTime.Now.Date.AddDays(-30);
            notDelete.TagId = 1;
            notDelete.Texto = "Texto Delete";
            notDelete.Title = "Titulo Delete";
            rep.Save();
            bool result = rep.Delete(notDelete);
        }

        private static void Edit(RepositoryNoticeContract rep)
        {
            bool result = false;
            Notice notc2 = rep.Find(7);
            if (notc2 != null)
            {
                notc2.Active = true;
                notc2.Date = DateTime.Now.Date.AddDays(-30);
                notc2.TagId = 1;
                notc2.Texto = "Texto Edit";
                notc2.Title = "Titulo Edit";
                result = rep.Edit(notc2);
            }
        }

        private static void Create(RepositoryNoticeContract rep)
        {
            Notice notc1 = rep.Create();
            DbSet<Tags> tagc1 = rep.Create<Tags>();

            Notice notc2 = rep.CreateAndAttach();
            notc2.Active = true;
            notc2.Date = DateTime.Now.Date.AddDays(-30);
            notc2.TagId = 1;
            notc2.Texto = "Texto Create";
            notc2.Title = "Titulo Create";
            int result = rep.Save();
        }

        private static void Add(RepositoryNoticeContract rep)
        {
            string guid = Guid.NewGuid().ToString();

            Notice not = new Notice();
            not.Active = true;
            not.Date = DateTime.Now.Date.AddDays(-30);
            not.TagId = 1;
            not.Texto = "Texto " + guid;
            not.Title = "Titulo " + guid;


            Notice not0 = new Notice();
            not0.Active = true;
            not0.Date = DateTime.Now.Date.AddDays(-30);
            not0.TagId = 1;
            not0.Texto = "Texto " + guid;
            not0.Title = "Titulo " + guid;

            Notice not1 = new Notice();
            not1.Active = true;
            not1.Date = DateTime.Now.Date.AddDays(-30);
            not1.TagId = 1;
            not1.Texto = "Texto " + guid;
            not1.Title = "Titulo " + guid;

            rep.Add(not);
            rep.Add(new List<Notice>(2) { not0, not1 });
        }
    }
}

