using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace SimpleSqliteUnitOfWork.Cli.Data;

public abstract class Repository<TEntity>
    : IDisposable
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
    public abstract TEntity GetById(int id);
    public abstract bool Add(TEntity entity);
    public abstract bool Update(int id, TEntity entity);
    public abstract bool DeleteById(int id);
}