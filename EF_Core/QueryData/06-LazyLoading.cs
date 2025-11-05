using System;
using System.Linq;
using EF_Core.QueryData.Data;
using EF_Core.QueryData.Entities;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace EF_Core.QueryData;

public abstract class LazyLoading
{
    public static void Run()
    {
        Utils.printTitle(title: "Lazy Loading", color: ConsoleColor.Blue, width: 70);

        Console.WriteLine("""
        Steps:
        1) Install Microsoft.EntityFrameworkCore.Proxies
        2) In AppDbContext -> OnConfiguring() add optionsBuilder.UseLazyLoadingProxies().UseSqlServer(constr)
        3) All entity types must be public, unsealed, have virtual navigation properties and have a public or protected constructor
        4) Now Navigation properties will work without need to use Eager or Explicit Loading
        """);
    }
}