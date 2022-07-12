using System;
using System.Collections.Generic;
using System.Data.SQLite;
using SimpleSqliteUnitOfWork.Cli.Data.Entities;

namespace SimpleSqliteUnitOfWork.Cli.Data;

public abstract class Repository<TEntity, TId>
    : IDisposable
    where TEntity : IEntity<TId>
{
    protected readonly SQLiteTransaction _transaction;

    protected bool IsDisposed { get; private set; }

    public Repository(SQLiteTransaction transaction)
    {
        _transaction = transaction;

        IsDisposed = false;
    }

    public virtual void Dispose()
    {
        if (IsDisposed)
            return;

        IsDisposed = true;

        _transaction.Dispose();
    }

    public abstract IEnumerable<TEntity> GetAll();
    public abstract TEntity GetById(TId id);
    public abstract bool Add(TEntity entity);
    public abstract bool Update(TId id, TEntity entity);
    public abstract bool DeleteById(TId id);
}