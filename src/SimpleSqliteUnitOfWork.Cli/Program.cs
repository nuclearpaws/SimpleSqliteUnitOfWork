﻿using System;
using System.Text.Json;
using SimpleSqliteUnitOfWork.Cli;
using SimpleSqliteUnitOfWork.Cli.Data;
using SimpleSqliteUnitOfWork.Cli.Data.Entities;

var dbFilePath = "./mydb.db";

var basicLogger = new BasicLogger();
using var uow = new UnitOfWork(dbFilePath, basicLogger);

uow.Person.DeleteById(1);
uow.SaveChanges();

public static class Extensions
{
    public static string ToJson(this object obj)
    {
        var json = JsonSerializer.Serialize(obj);
        return json;
    }
}
