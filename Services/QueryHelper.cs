using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using onlyarts.Extensions;
using onlyarts.Interfaces;
using onlyarts.Models;
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
        public User getUserByLogin(string login)
        {
            var user = (
                from _user in _context.Users
                where _user.Login == login
                select _user
            ).SingleOrDefault();
            return user;
        }
        public List<T> GetMinMax<T>(List<T> collection, int min, int max)
        {
            var length = collection.Count;
            if (min == 0 && max == 0) {
                return collection;
            }
            if (max > length) 
            {
                return collection.GetRange(min, length - min);
            }
            if (max - min > length) {
                return collection.GetRange(min, length - min);
            }
            return collection.GetRange(min, max - min);
        }
        public int GetLikesCount(Content content) 
        {
            return GetReactionsCount(content, false);
        }
        public int GetDislikesCount(Content content) 
        {
            return GetReactionsCount(content, true);
        }
        private int GetReactionsCount(Content content, bool reactionType)
        {
            if (content == null) {
                return -1;
            }
            var reactions = (
                from _reaction in _context.Reactions
                where _reaction.Content == content
                && _reaction.Type == reactionType
                select _reaction
            ).Count();
            return reactions;
        }
    }
}