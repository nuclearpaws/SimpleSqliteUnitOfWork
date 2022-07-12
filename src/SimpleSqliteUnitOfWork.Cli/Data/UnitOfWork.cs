using System;
using System.Data.SQLite;

namespace SimpleSqliteUnitOfWork.Cli.Data;

public class UnitOfWork
    : IDisposable
{
    private readonly SQLiteConnection _dbConnection;
    private readonly SQLiteTransaction _transaction;

    private readonly BasicLogger _logger;

    public PersonRepository Person { get; private set; }

    protected bool IsDisposed { get; private set; }

    public UnitOfWork(string dbFilePath, BasicLogger basicLogger)
    {
        _logger = basicLogger;

        var connectionString = $"Data Source={dbFilePath};Version=3;";
        _dbConnection = new SQLiteConnection(connectionString);

        _dbConnection.Open();
        _transaction = _dbConnection.BeginTransaction();

        Person = new PersonRepository(_transaction, basicLogger);
    }

    public void SaveChanges()
    {
        _transaction.Commit();
    }

    public void Dispose()
    {
        if (IsDisposed)
            return;

        IsDisposed = true;

        _dbConnection.Close();
        _dbConnection.Dispose();
    }
}