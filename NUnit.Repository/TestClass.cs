using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Canducci.EntityFramework.ConsoleAppTest;
using Canducci.EntityFramework.ConsoleAppTest.Models;
using Canducci.EntityFramework.Repository.Util;
using Canducci.EntityFramework.Repository.Contracts.Interfaces;
using PagedList;
using System;
using System.Data.Entity;

namespace NUnit.Repository
{
    [TestFixture]
    public class TestClass
    {
        public BaseEFContext ctx;
        public RepositoryNoticeContract rep;        
        public TestClass()
        {
            ctx = new BaseEFContext();
            rep = new RepositoryNotice(ctx);             
        }

        [Test]
        public void TestAdd()
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

            Assert.NotNull(not);
            Assert.NotNull(not0);
            Assert.NotNull(not1);
            Assert.True(not.Id > 0);
            Assert.True(not0.Id > 0);
            Assert.True(not1.Id > 0);            

        }

        [Test]
        public void TestCreate()
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

            Assert.IsInstanceOf(typeof(Notice), notc1);
            Assert.IsInstanceOf(typeof(Notice), notc2);
            Assert.IsInstanceOf(typeof(DbSet<Tags>), tagc1);
            Assert.True(result > 0);

        }

        [Test]
        public void TestEdit()
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
            Assert.IsInstanceOf(typeof(Notice), notc2);
            Assert.True(result);
        }

        [Test]
        public void TestDelete()
        {
            Notice notDelete = rep.CreateAndAttach();
            notDelete.Active = true;
            notDelete.Date = DateTime.Now.Date.AddDays(-30);
            notDelete.TagId = 1;
            notDelete.Texto = "Texto Edit";
            notDelete.Title = "Titulo Edit";
            rep.Save();

            Assert.IsInstanceOf<Notice>(notDelete);
            bool result = rep.Delete(notDelete);
            Assert.True(result);
        }

        [Test]
        public void TestFind()
        {
            Notice notFind = rep.Find(8);
            Assert.IsInstanceOf<Notice>(notFind);
            Assert.NotNull(notFind);
        }

        [Test]
        public void TestList()
        {
            IList<Notice> n1 = rep.List();
            IList<Notice> n2 = rep.List(x => x.Active);
            IList<Notice> n3 = rep.List(x => x.Active == false);
            IList<Notice> n4 = rep.List(x => x.Active, 1, 10);
            IList<Notice> n5 = rep.List(c => c.Id > 0, x => x.Active.Value, true);
            IList<Notice> n6 = rep.List(c => c.Id > 0, x => x.Active.Value, 1);
            IList<ViewModel> n7 = rep.List(c => c.Id, x => new ViewModel { Id = x.Id, Title = x.Title });
            IList<ViewModel> n8 = rep.List(x=> x.TagId > 0, c => c.Id, x => new ViewModel { Id = x.Id, Title = x.Title });
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

            Assert.NotNull(n1);
            Assert.NotNull(n2);
            Assert.NotNull(n3);
            Assert.NotNull(n4);
            Assert.NotNull(n5);
            Assert.NotNull(n6);
            Assert.NotNull(n7);
            Assert.NotNull(n8);
            Assert.NotNull(n9);
            Assert.NotNull(n10);
            Assert.NotNull(n21);
            Assert.NotNull(n11);
            Assert.NotNull(n12);
            Assert.NotNull(n13);
            Assert.NotNull(n14);
            Assert.NotNull(n15);
            Assert.NotNull(n16);
            Assert.NotNull(n17);
            Assert.NotNull(n18);
            Assert.NotNull(n19);
            Assert.NotNull(n20);
            Assert.NotNull(n21);

        }

        


        [Test]
        public void TestAll()
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

            Assert.NotNull(n1);
            Assert.NotNull(n2);
            Assert.NotNull(n3);
            Assert.NotNull(n4);
            Assert.NotNull(n5);
            Assert.NotNull(n6);
            Assert.NotNull(n7);
            Assert.NotNull(n8);
            Assert.NotNull(n9);
            Assert.NotNull(n10);
            Assert.NotNull(n11);
            Assert.NotNull(n12);

        }

        [Test]
        public void TestPagination()
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
                a.OrderBy.Add(c => c.Active);
                a.Page = 1;
                a.Total = 2;
                a.Select.Set(s => new ViewModel { Id = s.Id, Title = s.Title });
            });

            Assert.NotNull(p1);
            Assert.NotNull(p2);
            Assert.NotNull(p3);
            Assert.NotNull(p4);
            Assert.NotNull(p5);
            Assert.NotNull(p6);
            Assert.NotNull(p7);
            Assert.NotNull(p8);
            Assert.NotNull(p9);
            Assert.NotNull(p10);
            Assert.NotNull(p11);
            Assert.NotNull(p12);
        }

        [Test]
        public void TestGroupBy()
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

            var res2
                = rep.GroupBy(groupOrderBy, groupWhere, n => n.TagId,
                s => new Tipo
                {
                    TipoId = s.Key,
                    Quantidade = s.Count()
                });

            Assert.NotNull(res1);
            Assert.IsInstanceOf<IList<Tipo>>(res2);

        }
    }
}
