﻿using Microsoft.EntityFrameworkCore;

namespace Tatuaz.History.DataAccess.Test;

public class ConventionTest
{
    [Fact]
    public void Should_HistDbContextMirrorDbContext()
    {
        var dbType = typeof(DbContext);
        var histType = typeof(HistDbContext);

        var dbProps = dbType.GetProperties().Where(x =>
            x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>)).ToList();
        var histProps = histType.GetProperties().Where(x =>
            x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>)).ToList();

        var dbPropNames = dbProps.Select(p => p.Name).ToList();
        var histPropNames = histProps.Select(p => p.Name).ToList();

        var missingProps = dbPropNames.Except(histPropNames).ToList();
        var extraProps = histPropNames.Except(dbPropNames).ToList();

        Assert.Empty(missingProps);
        Assert.Empty(extraProps);
    }
}
