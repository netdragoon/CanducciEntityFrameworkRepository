using Canducci.EntityFramework.Repository.Contracts.Interfaces;
using Canducci.EntityFramework.Repository.Util;
using ConsoleApplication1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            BaseEFEntities c1 = new BaseEFEntities();
            c1.Database.Log = Log => TextSQL(Log);

            

            //RepositoryClientesContract repClientes = new RepositoryClientes(c1);
            RepositoryTelefonesContract repTelefones = new RepositoryTelefones(c1);
            //RepositorySomaContract repSoma = new RepositorySoma(c1);
            //RepositoryClientesContract repCliente = new RepositoryClientes(c1);

            //IGroupOrderBy<Peoples> groupOrderBy =
            //    GroupOrderBy<Peoples>.Create().Add(c => c.Nome, Order.Asc);

            //IGroupWhere<Peoples> groupWhere =
            //    GroupWhere<Peoples>.Create(c => c.Quantidade > 1);


            //var rre = repTelefones.GroupBy(a => new { a.Id },
            //    c => new
            //    {
            //        c.Key,
            //        count = c.Count()
            //    });

            //var resultado 
            //    = repTelefones.GroupBy(groupOrderBy, groupWhere,
            //    c => new { c.ClienteId, c.Clientes.Name },
            //    s => new Peoples
            //    {
            //        Id = s.Key.ClienteId,
            //        Nome = s.Key.Name, 
            //        Quantidade = s.Count()
            //    });

            
            //var resultado = repTelefones
            //    .GroupBy(
            //    groupOrderBy, 
            //    groupWhere,
            //    c => new { c.ClienteId, c.Clientes.Name }, 
            //    x => new { Id = x.Key.ClienteId, Name = x.Key.Name, c = x.Count() });

            //var d = repTelefones;

            //var t = new Telefones()
            //{
            //    ClienteId = 1,
            //    Numero = "4444-4444",
            //    Ddd = "22",
            //    TipoId = 1
            //};
            //repTelefones.Add(t);

            //IList<int> numeros = new List<int>() { 7, 8, 9 };
            //IEnumerable<Telefones> repT = repTelefones.All(x => numeros.Contains(x.Id), false);
            //repT.ToList().ForEach(x =>
            //{
            //    x.Ddd = "23";
            //});

            repTelefones.Delete(1);

            repTelefones.Save();

            //repTelefones.Query()
            //    .Include(x => x.Tipo)
            //    .Include(x => x.Clientes).ToArray();


            //var sum = repTelefones.Sum(x => x.Id > 2, x => x.TipoId);
            //var count = repTelefones.Count(x => x.Id > 0);


            //Telefones telefone =
            //    repTelefones.Query(c => c.Clientes, t => t.Tipo)
            //        .FirstOrDefault(x => x.Id == 1);





            //var item0 = repSoma.All(ConfigurationOrderBy<Soma>.Create(x =>
            //{
            //    x.OrderBy.Add(a => a.Valor);
            //}));

            //var item1 = repSoma.All(ConfigurationOrderByWhere<Soma>.Create(x =>
            //{
            //    x.OrderBy.Add(a => a.Valor);
            //    x.Where.Add(a => a.ItemId > 1);
            //}));

            //var item2 = repSoma.All(ConfigurationOrderByWhereSelect<Soma, ViewModel>.Create(x =>
            //{
            //    x.OrderBy.Add(a => a.Valor);
            //    x.Where.Add(a => a.ItemId > 1);
            //    x.Select.Set(a => new ViewModel() { Quantidade = a.ItemId, Valor = a.Valor });
            //}));

            //var item3 = repSoma.All(ConfigurationOrderByWhereSelectLimit<Soma, ViewModel>.Create(x =>
            //{
            //    //x.OrderBy.Add(a => a.Valor);
            //    //x.Where.Add(a => a.ItemId > 1);
            //    //x.Select.Set(a => new ViewModel() { Quantidade = a.ItemId, Valor = a.Valor });
            //    //x.Page = 1;
            //    //x.Total = 5;
            //}));

            //int i = 2;

            //IPagedList<ViewModel> itemPag0 = repSoma.Pagination(ConfigurationOrderByWherePaginationSelect<Soma, ViewModel>.Create(c =>
            //{
            //    //c.OrderBy.Add(x => x.ItemId);
            //    //c.Where.Add(x => x.ItemId > 0);
            //    //c.Page = i;
            //    //c.Total = 2;
            //    //c.Select.Set(s => new ViewModel() { Id = s.ItemId, Valor = s.Valor, Quantidade = s.ItemId });
            //}));

            //var resp0 = repSoma.All<ConfigurationOrderByWhereSelect<Soma, ViewModel>, ViewModel>(x =>
            //{
            //    x.OrderBy.Add(a => a.ItemId);
            //    x.OrderBy.Add(a => a.Valor, Order.Desc);
            //    x.Select.Set(s => new ViewModel() { Id = s.ItemId });
            //});

            //var resp1 = repSoma.All<ConfigurationOrderByWhere<Soma>>(x =>
            //{
            //    x.OrderBy.Add(a => a.ItemId);
            //    x.OrderBy.Add(a => a.Valor, Order.Desc);                
            //});


            //IPagedList<Soma> p = repSoma.Pagination<ConfigurationOrderByWherePagination<Soma>>(x =>
            //{
            //    x.OrderBy.Add(o => o.ItemId).Add(o => o.Valor);
            //    x.Page = 1;
            //    x.Total = 2;
            //    x.Where.Add(w => w.ItemId > 0).Add(w => w.Valor > 0);
            //});

            Console.WriteLine("Operação executado com sucesso");

            

            System.Console.ReadKey();
        }
        private static void TextSQL(string Log)
        {
            System.Console.WriteLine(Log);
        }
    }
}
