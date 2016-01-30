using Canducci.EntityFramework.Repository.Contracts;
namespace ConsoleApplication1.Models
{
    public abstract class RepositoryNoticeContract:
        Repository<Notice, BaseEFEntities>,
        IRepository<Notice, BaseEFEntities>        
    {

        public RepositoryNoticeContract(BaseEFEntities context)
            :base(context)
        {

        }

    }

    public sealed class RepositoryNotice: RepositoryNoticeContract
    {

        public RepositoryNotice(BaseEFEntities context)
            :base(context)
        {
        }

    }

    public abstract class RepositoryTagsContract :
        Repository<Tags, BaseEFEntities>,
        IRepository<Tags, BaseEFEntities>
    {

        public RepositoryTagsContract(BaseEFEntities context)
            : base(context)
        {

        }

    }

    public sealed class RepositoryTags : RepositoryTagsContract
    {

        public RepositoryTags(BaseEFEntities context)
            : base(context)
        {
        }

    }

    public abstract class RepositorySomaContract :
        Repository<Soma, BaseEFEntities>,
        IRepository<Soma, BaseEFEntities>
    {

        public RepositorySomaContract(BaseEFEntities context)
            : base(context)
        {

        }

    }

    public sealed class RepositorySoma : RepositorySomaContract
    {

        public RepositorySoma(BaseEFEntities context)
            : base(context)
        {
        }

    }


    public abstract class RepositoryClientesContract :
        Repository<Clientes, BaseEFEntities>,
        IRepository<Clientes, BaseEFEntities>
    {

        public RepositoryClientesContract(BaseEFEntities context)
            : base(context)
        {

        }

    }

    public sealed class RepositoryClientes : RepositoryClientesContract
    {

        public RepositoryClientes(BaseEFEntities context)
            : base(context)
        {
        }

    }

    public abstract class RepositoryTelefonesContract :
        Repository<Telefones, BaseEFEntities>,
        IRepository<Telefones, BaseEFEntities>
    {

        public RepositoryTelefonesContract(BaseEFEntities context)
            : base(context)
        {

        }

    }

    public sealed class RepositoryTelefones : RepositoryTelefonesContract
    {

        public RepositoryTelefones(BaseEFEntities context)
            : base(context)
        {
        }

    }


    public abstract class RepositoryTipoContract :
        Repository<Tipo, BaseEFEntities>,
        IRepository<Tipo, BaseEFEntities>
    {

        public RepositoryTipoContract(BaseEFEntities context)
            : base(context)
        {

        }

    }

    public sealed class RepositoryTipo : RepositoryTipoContract
    {

        public RepositoryTipo(BaseEFEntities context)
            : base(context)
        {
        }

    }


    public class ViewModelCliente
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ViewModel
    {
        public ViewModel()
        {

        }
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
    }

    public class Peoples
    {
        public Peoples()
        {

        }
        public Peoples(int id, string nome, int quantidade)
        {
            Id = id;
            Nome = nome;
            Quantidade = quantidade;
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
    }

    //public enum Order
    //{
    //    Asc, 
    //    Desc
    //}
    //public sealed class GroupOrderBy
    //{

    //    private Dictionary<object, Order> items;

    //    public GroupOrderBy()
    //    {
    //        items = new Dictionary<object, Order>();
    //    }

    //    public GroupOrderBy OrderBy<T, Tkey>(Expression<Func<T, Tkey>> orderBy, Order order = Order.Asc)
    //    {
    //        items.Add(orderBy, order);
    //        return this;
    //    }

    //    public Dictionary<object, Order> Get()
    //    {
    //        return items;
    //    }
    //}
}
