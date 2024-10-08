using System;
using System.Linq.Expressions;

namespace Core.Specification;

public interface ISpecification<T>
{
   Expression<Func<T, bool>> Criteria {get;}
   List<Expression<Func<T, Object>>> Include {get; }
   Expression<Func<T,object>> OrderBy {get;}
   Expression<Func<T,object>> OrderByDescending {get;}

  
   int Take {get;}
   int Skip{get;}
   bool isPagingEnabled {get;}

}
 public interface ISpecification<T, TResult>: ISpecification<T>
   {
      Expression<Func<T,TResult>> Select{get;}
   }
