using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Drones.Infrastructure.Helpers;

public static class RepositoryHelper
{
    public static IQueryable<TEntity> Filter<TEntity>(this IQueryable<TEntity> source, IEnumerable<Expression<Func<TEntity, bool>>> predicates) where TEntity : class
    {
        if (predicates != null && predicates.Any())
            foreach (var predicate in predicates)
                source = source.Where(predicate);

        return source;
    }
}
