using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using SimpleSqliteUnitOfWork.Cli.Data.Entities;

namespace SimpleSqliteUnitOfWork.Cli.Data;

public class PersonRepository
    : Repository<Person, int>
{
    private readonly BasicLogger _logger;

    public PersonRepository(
        SQLiteTransaction transaction,
        BasicLogger logger)
        : base(transaction)
    {
        _logger = logger;
    }

    public override bool Add(Person entity)
    {
        using var command = new SQLiteCommand();
        command.Transaction = _transaction;
        command.CommandType = CommandType.Text;
        command.CommandText = @$"
            INSERT INTO
                Person (FirstName, LastName, DateOfBirth)
            VALUES
                ('{entity.FirstName}', '{entity.LastName}', '{entity.DateOfBirth}')
        ";
        try
        {
            command.ExecuteNonQuery();
            _logger.Log("Added entity OK!");
            return true;
        }
        catch (Exception ex)
        {
            _logger.Log($"Something broke: {ex.Message}");
            return false;
        }
    }

    public override bool DeleteById(int id)
    {
        using var command = new SQLiteCommand();
        command.Transaction = _transaction;
        command.CommandType = CommandType.Text;
        command.CommandText = @$"
            DELETE FROM Person WHERE PersonId = {id} LIMIT 1
        ";
        try
        {
            command.ExecuteNonQuery();
            _logger.Log("Deleted entity OK!");
            return true;
        }
        catch (Exception ex)
        {
            _logger.Log($"Something broke: {ex.Message}");
            return false;
        }
    }

    public override IEnumerable<Person> GetAll()
    {
        using var command = new SQLiteCommand();
        command.Transaction = _transaction;
        command.CommandType = CommandType.Text;
        command.CommandText = @"
            SELECT * FROM Person
        ";

        try
        {
            using var reader = command.ExecuteReader();
            var persons = new List<Person>();
            while (reader.Read())
            {
                var person = new Person
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    DateOfBirth = reader.GetDateTime(3),
                };
                persons.Add(person);
            }
            _logger.Log("Got entities OK!");
            return persons;
        }
        catch (Exception ex)
        {
            _logger.Log($"Something broke: {ex.Message}");
            return null;
        }
    }

    public override Person GetById(int id)
    {
        using var command = new SQLiteCommand();
        command.Transaction = _transaction;
        command.CommandType = CommandType.Text;
        command.CommandText = @$"
            SELECT * FROM Person WHERE PersonId = {id} LIMIT 1
        ";

        try
        {
            using var reader = command.ExecuteReader();
            var person = default(Person);
            if (reader.Read())
            {
                person = new Person
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    DateOfBirth = reader.GetDateTime(3),
                };
            }
            _logger.Log("Got entity OK!");
            return person;
        }
        catch (Exception ex)
        {
            _logger.Log($"Something broke: {ex.Message}");
            return null;
        }
    }

    public override bool Update(int id, Person entity)
    {
        using var command = new SQLiteCommand();
        command.Transaction = _transaction;
        command.CommandType = CommandType.Text;
        command.CommandText = @$"
            UPDATE Person
            SET 
                FirstName = '{entity.FirstName}',
                LastName = '{entity.LastName}',
                DateOfBirth = '{entity.DateOfBirth:yyyy-MM-ddTHH:mm:ss.fff}'
            WHERE
                PersonId = {entity.Id}
        ";
        try
        {
            command.ExecuteNonQuery();
            _logger.Log("Added entity OK!");
            return true;
        }
        catch (Exception ex)
        {
            _logger.Log($"Something broke: {ex.Message}");
            return false;
        }
    }
}