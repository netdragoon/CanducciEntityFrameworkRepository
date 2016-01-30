using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using PagedList;
using Canducci.EntityFramework.Repository.Contracts.Interfaces;
#if NET45
using System.Threading;
using System.Threading.Tasks;
#endif
namespace Canducci.EntityFramework.Repository.Contracts
{
    public abstract class Repository<T, C>: IRepository<T, C> 
        where T: class, new() 
        where C: DbContext
    {


        #region Properties
        protected C Context { get; private set; }
        protected DbSet<T> Entity { get; private set; }
        #endregion Properties

        #region Constructor
        public Repository()
        {
            Context = Activator.CreateInstance<C>();
            Entity = Context.Set<T>();
        }
        public Repository(C context)
        {
            Context = context;
            Entity = Context.Set<T>();            
        }
        #endregion Constructor

        #region Methods       

        #region Add

        public T Add(T model)
        {
            Entity.Add(model);            
            Save();
            return model;
        }

        public IEnumerable<T> Add(IEnumerable<T> models)
        {
            Entity.AddRange(models);
            Save();
            return models;
        }

        #endregion Add

        #region Edit

        public bool Edit(T model)
        {
            Context.Entry(model).State = EntityState.Modified;
            return Save() > 0;
        }

        #endregion Edit

        #region Create

        public DbSet<T1> Create<T1>() 
            where T1 : class, new()   
        {
            return Context.Set<T1>();
        }

        public T Create()
        {            
           return Entity.Create();
        }

        public T CreateAndAttach()
        {            
            T _entity = Entity.Create();
            Context.Entry(_entity).State = EntityState.Added;
            return _entity;
        }
        #endregion Create

        #region Count

        public long Count()
        {
            return GetQueryable().LongCount();
        }

        public long Count(Expression<Func<T, bool>> where)
        {
            return GetQueryable().LongCount(where);
        }

        #endregion Count

        #region Sum

        public decimal Sum(Expression<Func<T, decimal>> selector)
        {
            return GetQueryable().Sum(selector);
        }

        public decimal? Sum(Expression<Func<T, decimal?>> selector)
        {
            return GetQueryable().Sum(selector);
        }

        public double Sum(Expression<Func<T, double>> selector)
        {
            return GetQueryable().Sum(selector);
        }

        public double? Sum(Expression<Func<T, double?>> selector)
        {
            return GetQueryable().Sum(selector);
        }

        public float Sum(Expression<Func<T, float>> selector)
        {
            return GetQueryable().Sum(selector);
        }

        public float? Sum(Expression<Func<T, float?>> selector)
        {
            return GetQueryable().Sum(selector);
        }

        public int Sum(Expression<Func<T, int>> selector)
        {
            return GetQueryable().Sum(selector);
        }

        public int? Sum(Expression<Func<T, int?>> selector)
        {
            return GetQueryable().Sum(selector);
        }

        public long Sum(Expression<Func<T, long>> selector)
        {
            return GetQueryable().Sum(selector);
        }

        public long? Sum(Expression<Func<T, long?>> selector)
        {
            return GetQueryable().Sum(selector);
        }

        public decimal Sum(Expression<Func<T, bool>> where, Expression<Func<T, decimal>> selector)
        {
            return GetQueryable().Where(where).Sum(selector);
        }

        public decimal? Sum(Expression<Func<T, bool>> where, Expression<Func<T, decimal?>> selector)
        {
            return GetQueryable().Where(where).Sum(selector);
        }

        public double Sum(Expression<Func<T, bool>> where, Expression<Func<T, double>> selector)
        {
            return GetQueryable().Where(where).Sum(selector);
        }

        public double? Sum(Expression<Func<T, bool>> where, Expression<Func<T, double?>> selector)
        {
            return GetQueryable().Where(where).Sum(selector);
        }

        public float Sum(Expression<Func<T, bool>> where, Expression<Func<T, float>> selector)
        {
            return GetQueryable().Where(where).Sum(selector);
        }

        public float? Sum(Expression<Func<T, bool>> where, Expression<Func<T, float?>> selector)
        {
            return GetQueryable().Where(where).Sum(selector);
        }

        public int Sum(Expression<Func<T, bool>> where, Expression<Func<T, int>> selector)
        {
            return GetQueryable().Where(where).Sum(selector);
        }

        public int? Sum(Expression<Func<T, bool>> where, Expression<Func<T, int?>> selector)
        {
            return GetQueryable().Where(where).Sum(selector);
        }

        public long Sum(Expression<Func<T, bool>> where, Expression<Func<T, long>> selector)
        {
            return GetQueryable().Where(where).Sum(selector);
        }

        public long? Sum(Expression<Func<T, bool>> where, Expression<Func<T, long?>> selector)
        {
            return GetQueryable().Where(where).Sum(selector);
        }

        #endregion

        #region GroupBy  
                
        public IList<TSelect> GroupBy<TKey, TSelect>(Expression<Func<T, TKey>> keySelector, Expression<Func<IGrouping<TKey, T>, TSelect>> select)
        {
            return GetQueryable().GroupBy(keySelector).Select(select).ToList();                            
        }

        public IList<TSelect> GroupBy<TKey, TSelect>(IGroupOrderBy<TSelect> groupOrderBy, IGroupWhere<TSelect> groupWhere, Expression<Func<T, TKey>> keySelector, Expression<Func<IGrouping<TKey, T>, TSelect>> select)
        {
            IQueryable<T> model = GetQueryable();
            IQueryable<TSelect> modelSelect = model.GroupBy(keySelector).Select(select).AsQueryable();
            modelSelect = GetGroupWhereQueryable(groupWhere, modelSelect);
            return GetOrderedQueryable(groupOrderBy, modelSelect).ToList();
        }

        #endregion

        #region Delete

        public bool Delete(T model)
        {
            Context.Entry(model).State = EntityState.Deleted;
            return Save() > 0;
        }

        public bool Delete(params object[] Key)
        {
            T model = Find(Key);
            if (model != null) { return Delete(model); }
            return false;
        }

        public bool Delete(IEnumerable<T> models)
        {
            Entity.RemoveRange(models);
            return Save() > 0;
        }

        public bool Delete(Expression<Func<T, bool>> where)
        {
            T model = Find(where);
            if (model != null) { return Delete(model); }
            return false;
        }

        #endregion Delete

        #region Find

        public T Find(params object[] key)
        {
            return Entity.Find(key);
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return Entity.FirstOrDefault(where);
        }

        #endregion Find

        #region All

        public IEnumerable<T> All(bool AsNoTracking = true)
        {
            IQueryable<T> model = GetQueryable(AsNoTracking);            
            return model.AsEnumerable();
        }

        public IEnumerable<T> All(IConfiguration<T> configuration)
        {            
            return configuration.Query(Entity).ToList();
        }

        public IList<TResult> All<TResult>(IConfiguration<T, TResult> configuration)
        {
            return configuration.Query(Entity).ToList();
        }       

        public IEnumerable<T> All(IGroupOrderBy<T> groupOrderBy, bool AsNoTracking = true)
        {
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);
            if (AsNoTracking)
            {
                return modelOrdered.AsNoTracking().AsEnumerable();
            }
            return modelOrdered.AsEnumerable();
        }

        public IEnumerable<T> All(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, bool AsNoTracking = true)
        {
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);
            if (AsNoTracking)
            {
                return modelOrdered.Where(where).AsNoTracking().AsEnumerable();
            }
            return modelOrdered.Where(where).AsEnumerable();
        }
        public IEnumerable<T> All(Expression<Func<T, bool>> where, bool AsNoTracking = true)
        {
            IQueryable<T> model = Entity.AsQueryable();            
            return model.Where(where).AsEnumerable();
        }

        public IEnumerable<T> All<Tkey>(Expression<Func<T, Tkey>> orderBy, bool AsNoTracking = true)
        {
            IQueryable<T> model = GetQueryable(AsNoTracking);            
            return model.OrderBy(orderBy).AsEnumerable();
        }
        
        public IEnumerable<T> All<Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, bool AsNoTracking = true)
        {
            IQueryable<T> model = GetQueryable(AsNoTracking);            
            return model.Where(where).OrderBy(orderBy).AsEnumerable();
        }
                
        public IList<TResult> All<TResult, Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, Expression<Func<T, TResult>> select)
        {            
            return GetQueryable().Where(where).OrderBy(orderBy).Select(select).ToList();
        }

        public IList<TResult> All<TResult>(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, Expression<Func<T, TResult>> select)
        {
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);
            return modelOrdered.Where(where).Select(select).ToList();
        }

        public IEnumerable<T> All<TConfiguration>(Action<TConfiguration> configuration)
            where TConfiguration : IConfiguration<T>
        {            
            return All(ConfigurationUtil.Call<T, TConfiguration>(configuration));
        }

        public IList<TResult> All<TConfiguration, TResult>(Action<TConfiguration> configuration) 
            where TConfiguration : IConfiguration<T, TResult>
        {
            return All(ConfigurationUtil.Call<T, TResult, TConfiguration>(configuration));
        }

        #endregion All

        #region Query

        public IQueryable<T> Query()
        {
            return Entity;
        }

        public IQueryable<T> Query(params Expression<Func<T,object>>[] includes)
        {
            if (includes == null || includes.Count() == 0) { return Query(); }
            IQueryable<T> query = Query();
            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }

        public DbRawSqlQuery<T1> Query<T1>(string sql, params object[] parameters)
        {
            return Context.Database.SqlQuery<T1>(sql, parameters);
        }

        public DbSqlQuery<T> Query(string sql, params object[] parameters)
        {
            return Entity.SqlQuery(sql, parameters);
        }

        public int QueryCommand(string sql, params object[] parameters)
        {
            return Context.Database.ExecuteSqlCommand(sql, parameters);
        }

        public int QueryCommand(TransactionalBehavior transactionalBehavior, string sql, params object[] parameters)
        {
            return Context.Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters);
        }

        #endregion Query

        #region List

        public IList<T> List(bool AsNoTracking = true)
        {
            IQueryable<T> model = GetQueryable(AsNoTracking);            
            return model.ToList();
        }
        public IList<T> List(Expression<Func<T, bool>> where, bool AsNoTracking = true)
        {
            IQueryable<T> model = GetQueryable(AsNoTracking);            
            return model.Where(where).ToList();
        }

        public IList<T> List<Tkey>(Expression<Func<T, Tkey>> orderBy, bool AsNoTracking = true)
        {
            IQueryable<T> model = GetQueryable(AsNoTracking);            
            return model.OrderBy(orderBy).ToList();
        }

        public IList<T> List<Tkey>(Expression<Func<T, Tkey>> orderBy, int page, int total = 10, bool AsNoTracking = true)
        {
            if (page <= 0) page = 1;
            IQueryable<T> model = GetQueryable(AsNoTracking);            
            return model.OrderBy(orderBy).Skip((page - 1) * total).Take(total).ToList();
        }

        public IList<T> List<Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, bool AsNoTracking)
        {
            IQueryable<T> model = GetQueryable(AsNoTracking);            
            return model.Where(where).OrderBy(orderBy).ToList();
        }

        public IList<T> List<Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, int page, int total = 10, bool AsNoTracking = true)
        {
            if (page <= 0) page = 1;
            IQueryable<T> model = GetQueryable(AsNoTracking);            
            return model.Where(where).OrderBy(orderBy).Skip((page - 1) * total).Take(total).ToList();
        }

        public IList<TResult> List<TResult, Tkey>(Expression<Func<T, Tkey>> orderBy, Expression<Func<T, TResult>> select)
        {            
            return GetQueryable().OrderBy(orderBy).Select(select).ToList();
        }

        public IList<TResult> List<TResult, Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, Expression<Func<T, TResult>> select)
        {            
            return GetQueryable().Where(where).OrderBy(orderBy).Select(select).ToList();
        }

        public IList<TResult> List<TResult, Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, Expression<Func<T, TResult>> select, int page, int total = 10)
        {
            if (page <= 0) page = 1;            
            return GetQueryable().Where(where).OrderBy(orderBy).Select(select).Skip((page - 1) * total).Take(total).ToList();
        }

        public IList<T> List(IGroupOrderBy<T> groupOrderBy, bool AsNoTracking = true)
        {
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);
            if (AsNoTracking)
            {
                return modelOrdered.AsNoTracking().ToList();
            }
            return modelOrdered.ToList();
        }

        public IList<T> List(IGroupOrderBy<T> groupOrderBy, int page, int total = 10, bool AsNoTracking = true)
        {
            if (page <= 0) page = 1;
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);
            if (AsNoTracking)
            {
                return modelOrdered.AsNoTracking().Skip((page - 1) * total).Take(total).ToList();
            }
            return modelOrdered.Skip((page - 1) * total).Take(total).ToList();
        }

        public IList<T> List(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, bool AsNoTracking)
        {
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);
            if (AsNoTracking)
            {
                return modelOrdered.Where(where).AsNoTracking().ToList();
            }
            return modelOrdered.Where(where).ToList();
        }

        public IList<T> List(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, int page, int total = 10, bool AsNoTracking = true)
        {
            if (page <= 0) page = 1;
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);
            if (AsNoTracking)
            {
                return modelOrdered.AsNoTracking().Where(where).Skip((page - 1) * total).Take(total).ToList();
            }
            return modelOrdered.Where(where).Skip((page - 1) * total).Take(total).ToList();
        }

        public IList<TResult> List<TResult>(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, TResult>> select)
        {
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);            
            return modelOrdered.AsNoTracking().Select(select).ToList();
        }

        public IList<TResult> List<TResult>(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, Expression<Func<T, TResult>> select)
        {
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);
            return modelOrdered.AsNoTracking().Where(where).Select(select).ToList();
        }

        public IList<TResult> List<TResult>(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, Expression<Func<T, TResult>> select, int page, int total = 10)
        {
            if (page <= 0) page = 1;
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);
            return modelOrdered.AsNoTracking().Where(where).Select(select).Skip((page - 1) * total).Take(total).ToList();
        }

        public IList<T> List(IConfiguration<T> configuration)
        {
            return configuration.Query(Entity).ToList();
        }

        public IList<TResult> List<TResult>(IConfiguration<T, TResult> configuration)
        {
            return configuration.Query(Entity).ToList();
        }

        public IList<T> List<TConfiguration>(Action<TConfiguration> configuration)
            where TConfiguration : IConfiguration<T>
        {

            return List(ConfigurationUtil.Call<T, TConfiguration>(configuration));
        }

        public IList<TResult> List<TConfiguration, TResult>(Action<TConfiguration> configuration)
            where TConfiguration : IConfiguration<T, TResult>
        {
            return List(ConfigurationUtil.Call<T, TResult, TConfiguration>(configuration));
        }
        #endregion List

        #region Pagination

        public IPagedList<T> Pagination<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy, int page, int total = 10)
        {
            if (page <= 0) page = 1;
            return GetQueryable().OrderBy(orderBy).ToPagedList(page, total);            
        }

        public IPagedList<TResult> Pagination<TResult, TOrderBy>(Expression<Func<T, TOrderBy>> orderBy, Expression<Func<T, TResult>> select, int page, int total = 10)
        {
            if (page <= 0) page = 1;
            return GetQueryable().OrderBy(orderBy).Select(select).ToPagedList(page, total);
        }

        public IPagedList<T> Pagination<TOrderBy>(Expression<Func<T, bool>> where, Expression<Func<T, TOrderBy>> orderBy, int page, int total = 10)
        {
            if (page <= 0) page = 1;
            return GetQueryable().Where(where).OrderBy(orderBy).ToPagedList(page, total);
        }

        public IPagedList<TResult> Pagination<TResult, TOrderBy>(Expression<Func<T, bool>> where, Expression<Func<T, TOrderBy>> orderBy, Expression<Func<T, TResult>> select, int page, int total = 10)
        {
            if (page <= 0) page = 1;
            return GetQueryable().Where(where).OrderBy(orderBy).Select(select).ToPagedList(page, total);
        }

        public IPagedList<T> Pagination(IGroupOrderBy<T> groupOrderBy, int page, int total = 10)
        {
            if (page <= 0) page = 1;
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);
            return modelOrdered.AsNoTracking().ToPagedList(page, total);
        }

        public IPagedList<TResult> Pagination<TResult>(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, TResult>> select, int page, int total = 10)
        {
            if (page <= 0) page = 1;
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);
            return modelOrdered.AsNoTracking().Select(select).ToPagedList(page, total);
        }

        public IPagedList<T> Pagination(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, int page, int total = 10)
        {
            if (page <= 0) page = 1;
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);
            return modelOrdered.AsNoTracking().Where(where).ToPagedList(page, total);
        }

        public IPagedList<TResult> Pagination<TResult>(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, Expression<Func<T, TResult>> select, int page, int total = 10)
        {
            if (page <= 0) page = 1;
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);
            return modelOrdered.AsNoTracking().Where(where).Select(select).ToPagedList(page, total);
        }

        public IPagedList<T> Pagination(IConfiguration<T, T, IPagedList<T>> configuration)
        {
            return configuration.Query(Entity);
        }

        public IPagedList<TResult> Pagination<TResult>(IConfiguration<T, TResult, IPagedList<TResult>> configuration)
        {
            return configuration.Query(Entity);
        }

        public IPagedList<T> Pagination<TConfiguration>(Action<TConfiguration> configuration)
            where TConfiguration : IConfiguration<T, T, IPagedList<T>>
        {
            return Pagination(ConfigurationUtil.PaginationCall<T, TConfiguration>(configuration));            
        }

        public IPagedList<TResult> Pagination<TConfiguration, TResult>(Action<TConfiguration> configuration)
            where TConfiguration : IConfiguration<T, TResult, IPagedList<TResult>>
        {
            return Pagination(ConfigurationUtil.PaginationCall<T, TResult, TConfiguration>(configuration));
        }

        #endregion Pagination

        #region Save

        public int Save()
        {            
            return Context.SaveChanges();
        }

        #endregion Save

        #endregion Methods

#if NET45

        #region MethodsAsync

        #region AddAsync

        public async Task<T> AddAsync(T model)
        {
            Entity.Add(model);
            await SaveAsync();
            return model;
        }    
                    
        public async Task<IEnumerable<T>> AddAsync(IEnumerable<T> models)
        {            
            Entity.AddRange(models);
            await SaveAsync();
            return models;            
        }

        #endregion AddAsync

        #region EditAsync

        public async Task<bool> EditAsync(T model)
        {
            Context.Entry(model).State = EntityState.Modified;
            return await SaveAsync() > 0;
        }

        #endregion EditAsync

        #region CountAsync

        public async Task<long> CountAsync()
        {
            return await GetQueryable().LongCountAsync();
        }

        public async Task<long> CountAsync(Expression<Func<T, bool>> where)
        {
            return await GetQueryable().LongCountAsync(where);
        }

        #endregion

        #region SumAsync

        public async Task<decimal> SumAsync(Expression<Func<T, decimal>> selector)
        {
            return await GetQueryable().SumAsync(selector);
        }

        public async Task<decimal?> SumAsync(Expression<Func<T, decimal?>> selector)
        {
            return await GetQueryable().SumAsync(selector);
        }

        public async Task<double> SumAsync(Expression<Func<T, double>> selector)
        {
            return await GetQueryable().SumAsync(selector);
        }

        public async Task<double?> SumAsync(Expression<Func<T, double?>> selector)
        {
            return await GetQueryable().SumAsync(selector);
        }

        public async Task<float> SumAsync(Expression<Func<T, float>> selector)
        {
            return await GetQueryable().SumAsync(selector);
        }

        public async Task<float?> SumAsync(Expression<Func<T, float?>> selector)
        {
            return await GetQueryable().SumAsync(selector);
        }

        public async Task<int> SumAsync(Expression<Func<T, int>> selector)
        {
            return await GetQueryable().SumAsync(selector);
        }

        public async Task<int?> SumAsync(Expression<Func<T, int?>> selector)
        {
            return await GetQueryable().SumAsync(selector);
        }

        public async Task<long> SumAsync(Expression<Func<T, long>> selector)
        {
            return await GetQueryable().SumAsync(selector);
        }

        public async Task<long?> SumAsync(Expression<Func<T, long?>> selector)
        {
            return await GetQueryable().SumAsync(selector);
        }

        public async Task<decimal> SumAsync(Expression<Func<T, bool>> where, Expression<Func<T, decimal>> selector)
        {
            return await GetQueryable().Where(where).SumAsync(selector);
        }

        public async Task<decimal?> SumAsync(Expression<Func<T, bool>> where, Expression<Func<T, decimal?>> selector)
        {
            return await GetQueryable().Where(where).SumAsync(selector);
        }

        public async Task<double> SumAsync(Expression<Func<T, bool>> where, Expression<Func<T, double>> selector)
        {
            return await GetQueryable().Where(where).SumAsync(selector);
        }

        public async Task<double?> SumAsync(Expression<Func<T, bool>> where, Expression<Func<T, double?>> selector)
        {
            return await GetQueryable().Where(where).SumAsync(selector);
        }

        public async Task<float> SumAsync(Expression<Func<T, bool>> where, Expression<Func<T, float>> selector)
        {
            return await GetQueryable().Where(where).SumAsync(selector);
        }

        public async Task<float?> SumAsync(Expression<Func<T, bool>> where, Expression<Func<T, float?>> selector)
        {
            return await GetQueryable().Where(where).SumAsync(selector);
        }

        public async Task<int> SumAsync(Expression<Func<T, bool>> where, Expression<Func<T, int>> selector)
        {
            return await GetQueryable().Where(where).SumAsync(selector);
        }

        public async Task<int?> SumAsync(Expression<Func<T, bool>> where, Expression<Func<T, int?>> selector)
        {
            return await GetQueryable().Where(where).SumAsync(selector);
        }

        public async Task<long> SumAsync(Expression<Func<T, bool>> where, Expression<Func<T, long>> selector)
        {
            return await GetQueryable().Where(where).SumAsync(selector);
        }

        public async Task<long?> SumAsync(Expression<Func<T, bool>> where, Expression<Func<T, long?>> selector)
        {
            return await GetQueryable().Where(where).SumAsync(selector);
        }

        #endregion

        #region GroupByAsync

        public async Task<IList<TSelect>> GroupByAsync<TKey, TSelect>(Expression<Func<T, TKey>> keySelector, Expression<Func<IGrouping<TKey, T>, TSelect>> select)
        {
            return await GetQueryable().GroupBy(keySelector).Select(select).ToListAsync();
        }       

        public async Task<IList<TSelect>> GroupByAsync<TKey, TSelect>(IGroupOrderBy<TSelect> groupOrderBy, IGroupWhere<TSelect> groupWhere, Expression<Func<T, TKey>> keySelector, Expression<Func<IGrouping<TKey, T>, TSelect>> select)
        {
            IQueryable<T> model = GetQueryable();
            IQueryable<TSelect> modelSelect = model.GroupBy(keySelector).Select(select).AsQueryable();
            modelSelect = GetGroupWhereQueryable(groupWhere, modelSelect);
            return await GetOrderedQueryable(groupOrderBy, modelSelect).ToListAsync();
        }

        #endregion

        #region DeleteAsync

        public async Task<bool> DeleteAsync(IEnumerable<T> model)
        {            
            Entity.RemoveRange(model);
            return await SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(T model)
        {
            Context.Entry(model).State = EntityState.Deleted;
            return await SaveAsync() > 0;
        }

        public async Task<bool> DeleteAsync(params object[] key)
        {
            T model = await FindAsync(key);
            if (model != null) { return await DeleteAsync(model); }
            return false;
        }

        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> where)
        {
            T model = await FindAsync(where);
            if (model != null) { return await DeleteAsync(model); }
            return false;
        }

        #endregion DeleteAsync

        #region FindAsync

        public async Task<T> FindAsync(params object[] key)
        {
            return await Entity.FindAsync(key);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> where)
        {            
            return await Entity.FirstOrDefaultAsync(where);
        }

        #endregion FindAsync

        #region AllAsync

        public async Task<IList<T>> AllAsync(bool AsNoTracking = true)
        {
            IQueryable<T> model = GetQueryable(AsNoTracking);            
            return await model.ToListAsync();
        }

        public async Task<IList<T>> AllAsync(IGroupOrderBy<T> groupOrderBy, bool AsNoTracking = true)
        {
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);
            if (AsNoTracking)
            {
                return await modelOrdered.AsNoTracking().ToListAsync();
            }
            return await modelOrdered.ToListAsync();
        }

        public async Task<IList<T>> AllAsync(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, bool AsNoTracking = true)
        {
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);
            if (AsNoTracking)
            {
                return await modelOrdered.Where(where).AsNoTracking().ToListAsync();
            }
            return await modelOrdered.Where(where).ToListAsync();
        }

        public async Task<IList<T>> AllAsync(Expression<Func<T, bool>> where, bool AsNoTracking = true)
        {
            IQueryable<T> model = Entity.AsQueryable();            
            return await model.Where(where).ToListAsync();
        }

        public async Task<IList<T>> AllAsync<Tkey>(Expression<Func<T, Tkey>> orderBy, bool AsNoTracking = true)
        {
            IQueryable<T> model = Entity.AsQueryable();            
            return await model.OrderBy(orderBy).ToListAsync();
        }

        public async Task<IList<T>> AllAsync<Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, bool AsNoTracking = true)
        {
            IQueryable<T> model = GetQueryable(AsNoTracking);
            return await model.Where(where).OrderBy(orderBy).ToListAsync();
        }

        public async Task<IList<TResult>> AllAsync<TResult, Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, Expression<Func<T, TResult>> select)
        {            
            return await GetQueryable().Where(where).OrderBy(orderBy).Select(select).ToListAsync();
        }        

        public async Task<IList<TResult>> AllAsync<TResult>(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, Expression<Func<T, TResult>> select)
        {
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);
            return await modelOrdered.AsNoTracking().Where(where).Select(select).ToListAsync();
        }

        public async Task<IList<T>> AllAsync(IConfiguration<T> configuration)
        {
            return await configuration.Query(Entity).ToListAsync();
        }

        public async Task<IList<TResult>> AllAsync<TResult>(IConfiguration<T, TResult> configuration)
        {
            return await configuration.Query(Entity).ToListAsync();
        }

        public async Task<IList<T>> AllAsync<TConfiguration>(Action<TConfiguration> configuration)
            where TConfiguration : IConfiguration<T>
        {
            return await AllAsync(ConfigurationUtil.Call<T, TConfiguration>(configuration));
        }

        public async Task<IList<TResult>> AllAsync<TConfiguration, TResult>(Action<TConfiguration> configuration)
            where TConfiguration : IConfiguration<T, TResult>
        {
            return await AllAsync(ConfigurationUtil.Call<T, TResult, TConfiguration>(configuration));
        }

        #endregion

        #region QueryComandAsync

        public async Task<int> QueryCommandAsync(string sql, params object[] parameters)
        {
            return await Context.Database.ExecuteSqlCommandAsync(sql, parameters);
        }

        public async Task<int> QueryCommandAsync(string sql, CancellationToken cancellationToken, params object[] parameters)
        {
            return await Context.Database.ExecuteSqlCommandAsync(sql, cancellationToken, parameters);
        }

        public async Task<int> QueryCommandAsync(TransactionalBehavior transactionalBehavior, string sql, params object[] parameters)
        {
            return await Context.Database.ExecuteSqlCommandAsync(transactionalBehavior, sql, parameters);
        }

        public async Task<int> QueryCommandAsync(TransactionalBehavior transactionalBehavior, string sql, CancellationToken cancellationToken, params object[] parameters)
        {
            return await Context.Database.ExecuteSqlCommandAsync(transactionalBehavior, sql, cancellationToken, parameters);
        }

        #endregion QueryComandAsync

        #region ListAsync

        public async Task<IList<T>> ListAsync(bool AsNoTracking = true)
        {
            IQueryable<T> model = GetQueryable(AsNoTracking);            
            return await model.ToListAsync();
        }

        public async Task<IList<T>> ListAsync(Expression<Func<T, bool>> where, bool AsNoTracking = true)
        {
            IQueryable<T> model = GetQueryable(AsNoTracking);            
            return await model.Where(where).ToListAsync();
        }

        public async Task<IList<T>> ListAsync<Tkey>(Expression<Func<T, Tkey>> orderBy, bool AsNoTracking = true)
        {
            IQueryable<T> model = GetQueryable(AsNoTracking);
            return await model.OrderBy(orderBy).ToListAsync();
        }

        public async Task<IList<T>> ListAsync<Tkey>(Expression<Func<T, Tkey>> orderBy, int page, int total = 10, bool AsNoTracking = true)
        {
            if (page <= 0) page = 1;
            IQueryable<T> model = GetQueryable(AsNoTracking);            
            return await model.OrderBy(orderBy).Skip((page - 1) * total).Take(total).ToListAsync();
        }

        public async Task<IList<T>> ListAsync<Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, bool AsNoTracking)
        {
            IQueryable<T> model = GetQueryable(AsNoTracking);
            return await model.Where(where).OrderBy(orderBy).ToListAsync();
        }

        public async Task<IList<T>> ListAsync<Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, int page, int total = 10, bool AsNoTracking = true)
        {
            if (page <= 0) page = 1;
            IQueryable<T> model = GetQueryable(AsNoTracking);            
            return await model.Where(where).OrderBy(orderBy).Skip((page - 1) * total).Take(total).ToListAsync();
        }

        public async Task<IList<TResult>> ListAsync<TResult, Tkey>(Expression<Func<T, Tkey>> orderBy, Expression<Func<T, TResult>> select)
        {                        
            return await Entity.AsNoTracking().OrderBy(orderBy).Select(select).ToListAsync();
        }

        public async Task<IList<TResult>> ListAsync<TResult, Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, Expression<Func<T, TResult>> select)
        {                        
            return await Entity.AsNoTracking().Where(where).OrderBy(orderBy).Select(select).ToListAsync();
        }

        public async Task<IList<TResult>> ListAsync<TResult, Tkey>(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, Expression<Func<T, TResult>> select, int page, int total = 10)
        {
            if (page <= 0) page = 1;
            return await Entity.AsNoTracking().Where(where).OrderBy(orderBy).Select(select).Skip((page - 1) * total).Take(total).ToListAsync();
        }

        public async Task<IList<T>> ListAsync(IGroupOrderBy<T> groupOrderBy, bool AsNoTracking = true)
        {
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);
            if (AsNoTracking)
            {
                return await modelOrdered.AsNoTracking().ToListAsync();
            }
            return await modelOrdered.ToListAsync();
        }

        public async Task<IList<T>> ListAsync(IGroupOrderBy<T> groupOrderBy, int page, int total = 10, bool AsNoTracking = true)
        {
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);
            if (AsNoTracking)
            {
                return await modelOrdered.AsNoTracking().Skip((page - 1) * total).Take(total).ToListAsync();
            }
            return await modelOrdered.Skip((page - 1) * total).Take(total).ToListAsync();
        }

        public async Task<IList<T>> ListAsync(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, bool AsNoTracking)
        {
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);
            if (AsNoTracking)
            {
                return await modelOrdered.AsNoTracking().Where(where).ToListAsync();
            }
            return await modelOrdered.Where(where).ToListAsync();
        }

        public async Task<IList<T>> ListAsync(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, int page, int total = 10, bool AsNoTracking = true)
        {
            if (page <= 0) page = 1;
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);
            if (AsNoTracking)
            {
                return await modelOrdered.AsNoTracking().Where(where).Skip((page - 1) * total).Take(total).ToListAsync();
            }
            return await modelOrdered.Where(where).Skip((page - 1) * total).Take(total).ToListAsync();
        }

        public async Task<IList<TResult>> ListAsync<TResult>(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, TResult>> select)
        {
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);
            return await modelOrdered.AsNoTracking().Select(select).ToListAsync();
        }

        public async Task<IList<TResult>> ListAsync<TResult>(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, Expression<Func<T, TResult>> select)
        {
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);
            return await modelOrdered.AsNoTracking().Where(where).Select(select).ToListAsync();
        }

        public async Task<IList<TResult>> ListAsync<TResult>(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, Expression<Func<T, TResult>> select, int page, int total = 10)
        {
            if (page <= 0) page = 1;
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);
            return await modelOrdered.AsNoTracking().Where(where).Skip((page - 1) * total).Take(total).Select(select).ToListAsync();
        }

        public async Task<IList<T>> ListAsync(IConfiguration<T> configuration)
        {
            return await configuration.Query(Entity).ToListAsync();
        }

        public async Task<IList<TResult>> ListAsync<TResult>(IConfiguration<T, TResult> configuration)
        {
            return await configuration.Query(Entity).ToListAsync();
        }

        public async Task<IList<T>> ListAsync<TConfiguration>(Action<TConfiguration> configuration)
            where TConfiguration : IConfiguration<T>
        {
            return await ListAsync(ConfigurationUtil.Call<T, TConfiguration>(configuration));
        }

        public async Task<IList<TResult>> ListAsync<TConfiguration, TResult>(Action<TConfiguration> configuration)
            where TConfiguration : IConfiguration<T, TResult>
        {
            return await ListAsync(ConfigurationUtil.Call<T, TResult, TConfiguration>(configuration));
        }
        #endregion ListAsync

        #region PaginationAsync

        public async Task<IPagedList<T>> PaginationAsync<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy, int page, int total = 10)
        {
            if (page <= 0) page = 1;            
            IQueryable<T> model = GetQueryable();
            int totalItemCount = await model.CountAsync();
            model = model.OrderBy(orderBy).Skip((page - 1) * total).Take(total);
            return new StaticPagedList<T>(await model.ToListAsync(), page, total, totalItemCount);            
        }

        public async Task<IPagedList<TResult>> PaginationAsync<TResult, TOrderBy>(Expression<Func<T, TOrderBy>> orderBy, Expression<Func<T, TResult>> select, int page, int total = 10)
        {
            if (page <= 0) page = 1;
            IQueryable<T> model = GetQueryable();
            int totalItemCount = await model.CountAsync();
            IQueryable<TResult> result = model.OrderBy(orderBy).Skip((page - 1) * total).Take(total).Select(select);
            return new StaticPagedList<TResult>(await result.ToListAsync(), page, total, totalItemCount);
        }

        public async Task<IPagedList<T>> PaginationAsync<TOrderBy>(Expression<Func<T, bool>> where, Expression<Func<T, TOrderBy>> orderBy, int page, int total = 10)
        {
            if (page <= 0) page = 1;
            IQueryable<T> model = GetQueryable();
            int totalItemCount = await model.CountAsync(where);
            model = model.Where(where).OrderBy(orderBy).Skip((page - 1) * total).Take(total);
            return new StaticPagedList<T>(await model.ToListAsync(), page, total, totalItemCount);
        }

        public async Task<IPagedList<TResult>> PaginationAsync<TResult, TOrderBy>(Expression<Func<T, bool>> where, Expression<Func<T, TOrderBy>> orderBy, Expression<Func<T, TResult>> select, int page, int total = 10)
        {
            if (page <= 0) page = 1;
            IQueryable<T> model = GetQueryable();
            int totalItemCount = await model.CountAsync(where);            
            IQueryable<TResult> result = model.Where(where).OrderBy(orderBy).Select(select).Skip((page - 1) * total).Take(total);
            return new StaticPagedList<TResult>(await result.ToListAsync(), page, total, totalItemCount);
        }

        public async Task<IPagedList<T>> PaginationAsync(IGroupOrderBy<T> groupOrderBy, int page, int total = 10)
        {
            if (page <= 0) page = 1;
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);
            int totalItemCount = await GetQueryable().CountAsync();
            IQueryable<T> model = modelOrdered.AsNoTracking().Skip((page - 1) * total).Take(total);
            return new StaticPagedList<T>(await model.ToListAsync(), page, total, totalItemCount);
        }

        public async Task<IPagedList<TResult>> PaginationAsync<TResult>(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, TResult>> select, int page, int total = 10)
        {
            if (page <= 0) page = 1;
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);
            int totalItemCount = await GetQueryable().CountAsync();
            IQueryable<T> model = modelOrdered.AsNoTracking().Skip((page - 1) * total).Take(total);
            return new StaticPagedList<TResult>(await model.Select(select).ToListAsync(), page, total, totalItemCount);
        }

        public async Task<IPagedList<T>> PaginationAsync(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, int page, int total = 10)
        {
            if (page <= 0) page = 1;
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);
            int totalItemCount = await GetQueryable().CountAsync(where);
            IQueryable<T> model = modelOrdered.AsNoTracking().Where(where).Skip((page - 1) * total).Take(total);
            return new StaticPagedList<T>(await model.ToListAsync(), page, total, totalItemCount);
        }

        public async Task<IPagedList<TResult>> PaginationAsync<TResult>(IGroupOrderBy<T> groupOrderBy, Expression<Func<T, bool>> where, Expression<Func<T, TResult>> select, int page, int total = 10)
        {
            if (page <= 0) page = 1;
            IQueryable<T> modelOrdered = GetOrderedQueryable(groupOrderBy);
            int totalItemCount = await GetQueryable().CountAsync(where);
            IQueryable<T> model = modelOrdered.AsNoTracking().Where(where).Skip((page - 1) * total).Take(total);
            return new StaticPagedList<TResult>(await model.Select(select).ToListAsync(), page, total, totalItemCount);
        }

        public async Task<IPagedList<T>> PaginationAsync(IConfiguration<T, T, IPagedList<T>> configuration)
        {
            return await configuration.QueryAsync(Entity);
        }

        public async Task<IPagedList<TResult>> PaginationAsync<TResult>(IConfiguration<T, TResult, IPagedList<TResult>> configuration)
        {
            return await configuration.QueryAsync(Entity);
        }

        public async Task<IPagedList<T>> PaginationAsync<TConfiguration>(Action<TConfiguration> configuration)
            where TConfiguration : IConfiguration<T, T, IPagedList<T>>
        {
            return await PaginationAsync(ConfigurationUtil.PaginationCall<T, TConfiguration>(configuration));
        }
        public async Task<IPagedList<TResult>> PaginationAsync<TConfiguration, TResult>(Action<TConfiguration> configuration)
            where TConfiguration : IConfiguration<T, TResult, IPagedList<TResult>>
        {
            return await PaginationAsync(ConfigurationUtil.PaginationCall<T, TResult, TConfiguration>(configuration));
        }

        #endregion

        #region SaveAsync

        public async Task<int> SaveAsync()
        {
            return await Context.SaveChangesAsync();
        }

        #endregion SaveAsync

        #endregion MethodsAsync

#endif

        #region Dispose

        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);  
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                if (Context != null)
                {
                    Context.Dispose();
                }           
            }
            disposed = true;
        }

        ~Repository()
        {
            Dispose(false);
        }
        #endregion Dispose        

        #region Routines

        private IQueryable<T> GetQueryable(bool AsNoTracking = true)
        {
            if (AsNoTracking) return Entity.AsNoTracking();
            return Entity;
        }

        private IQueryable<T> GetOrderedQueryable(IGroupOrderBy<T> groupOrderBy, IQueryable<T> model = null)
        {
            if (model == null) model = Entity.AsNoTracking();
            return QueryableUtil.GetOrderedQueryable(model, groupOrderBy);
        }

        private IQueryable<TSelect> GetOrderedQueryable<TSelect>(IGroupOrderBy<TSelect> groupOrderBy, IQueryable<TSelect> model = null)
        {            
            return QueryableUtil.GetOrderedQueryable(model, groupOrderBy);
        }

        private IQueryable<T> GetGroupWhereQueryable(IGroupWhere<T> groupWhere, IQueryable<T> model = null)
        {
            if (model == null) model = Entity.AsNoTracking();
            return QueryableUtil.GetWhereQueryable(model, groupWhere);
        }

        private IQueryable<TSelect> GetGroupWhereQueryable<TSelect>(IGroupWhere<TSelect> groupWhere, IQueryable<TSelect> model = null)
        {            
            return QueryableUtil.GetWhereQueryable(model, groupWhere);
        }

        #endregion

    }
}
