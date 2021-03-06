﻿using Canducci.EntityFramework.Repository.Contracts.Interfaces;
using System;
using System.Data.Entity;
namespace Canducci.EntityFramework.Repository.Contracts
{
    public interface IRepository<T, C>: 
        IAdd<T>,
        ICreate<T>,
        IEdit<T>,
        IDelete<T>,
        IFind<T>,
        IQuery<T>,
        IQueryCommand,
        IPagination<T>,
        IAll<T>,
        ISum<T>,
        IGroupBy<T>,
        ILists<T>,
        ICount<T>,
        ISave,
        IDisposable
        where T: class, new()
        where C: DbContext
    {
        
    }
}
