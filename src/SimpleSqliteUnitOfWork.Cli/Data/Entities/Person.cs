using System;

namespace SimpleSqliteUnitOfWork.Cli.Data.Entities;

public class Person
{
    public int PersonId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}