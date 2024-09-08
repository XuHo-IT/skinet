using System;
using System.Linq.Expressions;

namespace Core.Specification;

public class BaseSpecification<T> : ISpecification<T>
{
    public BaseSpecification()
    {
    }

    public BaseSpecification(Expression<Func<T,bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<T, bool>> Criteria {get;}

    public List<Expression<Func<T, object>>> Include {get;}
     = new List<Expression<Func<T,object>>>();

    

    protected void AddInclude(Expression<Func<T,Object>> includeExpression)
    {
        Include.Add(includeExpression);
    } 
}
