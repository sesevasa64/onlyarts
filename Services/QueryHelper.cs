using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using onlyarts.Extensions;
using onlyarts.Interfaces;
using onlyarts.Data;

namespace onlyarts.Services
{
    public class QueryHelper
    {
        private readonly OnlyartsContext _context;
        public QueryHelper(OnlyartsContext context) 
        {
            _context = context;
        }
        public T getByID<T>(int id, string[] includes = null) where T : class, IEntity
        {
            includes = includes ?? new string[0];
            var result = (
                from entry in _context.Set<T>()
                where entry.Id == id
                select entry
            ).GetAllIncluding(includes)
            .SingleOrDefault();
            return result;
        }
        public List<T> getMultipleByID<T>(int[] id, string[] includes = null) where T : class, IEntity
        {
            includes = includes ?? new string[0];
            var result = (
                from entry in _context.Set<T>()
                where id.Contains(entry.Id)
                select entry
            ).GetAllIncluding(includes)
            .ToList();
            return result;
        }
    }
}