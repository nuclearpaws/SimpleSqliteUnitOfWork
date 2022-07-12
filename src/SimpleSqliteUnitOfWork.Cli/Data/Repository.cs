using System.Collections.Generic;

namespace SimpleSqliteUnitOfWork.Cli.Data;

public abstract class Repository<TEntity>
{
    public abstract IEnumerable<TEntity> GetAll();
    public abstract TEntity GetById(int id);
    public abstract bool Add(TEntity entity);
    public abstract bool Update(int id, TEntity entity);
    public abstract bool DeleteById(int id);
}