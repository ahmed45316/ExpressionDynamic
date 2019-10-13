using ExpressionGenerator.Common;
using LinqKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ExpressionGenerator
{
    public class FilterBuilder
    {
        public Expression<Func<T, bool>> GetExpression<T,U>(U filterDto) where T : class
        {
            var predicate = PredicateBuilder.New<T>(true);
            var param = Expression.Parameter(typeof(T), "x");
            var columns = typeof(U).GetProperties().Select(x => new {type=x.PropertyType, property = x.Name, value = x.GetValue(filterDto) }).Where(x => x.value != null).ToList();
            foreach (var column in columns)
            {
                var member = Expression.Property(param, column.property);
                var constant = GetValueExpression(column.property, column.value, param);
                if (column.type == typeof(string))
                {
                   var exp = Expression.Lambda<Func<T, bool>>( member.StartWith(constant),param);
                   predicate = predicate.And(exp);
                }
                else
                {
                    var exp = Expression.Equal(member, constant);
                    predicate.And(Expression.Lambda<Func<T, bool>>(exp, param));
                }
            }

            return predicate;
        }
        private static UnaryExpression GetValueExpression(string propertyName, object val, ParameterExpression param)
        {
            var member = Expression.Property(param, propertyName);
            var propertyType = ((PropertyInfo)member.Member).PropertyType;
            var converter = TypeDescriptor.GetConverter(propertyType);

            if (!converter.CanConvertFrom(typeof(string)))
                throw new NotSupportedException();

            var constant = Expression.Constant(val);
            return Expression.Convert(constant, propertyType);
        }
        
    }
}
