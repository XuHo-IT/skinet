using System;
using Core.Entities;
using Core.Specification;
using Microsoft.AspNetCore.Http;
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
  
    if (spec.OrderBy != null)
    {
        query = query.OrderBy(spec.OrderBy);  
    }

      if (spec.OrderByDescending != null)
    {
        query = query.OrderByDescending(spec.OrderByDescending);  
    }

     if (spec.isPagingEnabled)
    {
        query = query.Skip(spec.Skip).Take(spec.Take);  
    }
  query = spec.Include.Aggregate(query, (current, include) => current.Include(include));


    return query;
  }

}