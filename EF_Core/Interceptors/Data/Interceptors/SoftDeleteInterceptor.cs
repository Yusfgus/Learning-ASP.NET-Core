using EF_Core.Interceptors.Entities.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EF_Core.Interceptors.Data.Interceptors;

public class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is null)
            return result;

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            // if(entry is not { State: EntityState.Deleted, Entity: ISoftDeletable entity })  // Pattern matching
            if (entry is null || entry.State != EntityState.Deleted || !(entry.Entity is ISoftDeletable entity))
                continue; // not book

            System.Console.WriteLine("Is Book");

            // is book
            entry.State = EntityState.Modified;
            entity.Delete();
        }

        return result;
    }
}