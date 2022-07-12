namespace SimpleSqliteUnitOfWork.Cli.Data.Entities;

public interface IEntity<TId>
{
    TId Id { get; set; }
}