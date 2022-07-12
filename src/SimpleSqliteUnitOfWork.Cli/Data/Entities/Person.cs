using System;

namespace SimpleSqliteUnitOfWork.Cli.Data.Entities;

public class Person
    : IEntity<int>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}