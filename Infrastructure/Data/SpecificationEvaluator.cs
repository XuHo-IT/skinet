using System;
using Core.Entities;
using Core.Specification;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
{
  public static IQueryable<TEntity> GetQuerry(IQueryable<TEntity> inputQuery,
  ISpecification<TEntity> spec)
  {
    var query = inputQuery;
    if (spec.Criteria != null)
    {
        query = query.Where(spec.Criteria);  // p => p.ProductId == id
    }
    query = spec.Include.Aggregate(query, (current, include) => current.Include(include));

    return query;
  }
}
