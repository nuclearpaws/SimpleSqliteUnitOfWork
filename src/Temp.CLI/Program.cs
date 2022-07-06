using System;
using System.Text.Json;
using Temp.CLI;
using Temp.CLI.Data;
using Temp.CLI.Data.Entities;

var dbFilePath = "./mydb.db";

var basicLogger = new BasicLogger();
using var uow = new UnitOfWork(dbFilePath, basicLogger);

var people = uow.Person.GetAll();

foreach(var person in people)
{
    System.Console.WriteLine(person.ToJson());
}

uow.SaveChanges();

public static class Extensions
{
    public static string ToJson(this object obj)
    {
        var json = JsonSerializer.Serialize(obj);
        return json;
    }
}
