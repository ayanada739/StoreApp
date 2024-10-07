using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Store.G04.Core.Entities;
using Store.G04.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Repository
{
    public static class SpecificationsEvaluator<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        //Create and Return Query
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> spec)
        {
            var query = inputQuery;

            if(spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }

            if(spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if(spec.OrderByDescendeing is not null)
            {
                query = query.OrderByDescending(spec.OrderByDescendeing);
            }

            if(spec.IsPaginationEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            //P => P.Brand
            //P => P.Type
            // _context.Products.Include(P => P.Brand)
            // _context.Products.Include(P => P.Brand).Include(P => P.Type)
            // _context.Products.Include(P => P.Brand).Include(P => P.Type)


            query = spec.Includes.Aggregate(query, (currentQuery, IncludeExpression) => currentQuery.Include(IncludeExpression));

            return query;

        }

        //P => P.Brand
        //P => P.Type

        // _context.Products.Where(P => P.Id == id as int?).Include(P => P.Brand).Include(P => P.Type);

    }
}
