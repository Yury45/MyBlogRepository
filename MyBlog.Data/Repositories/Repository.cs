using Microsoft.EntityFrameworkCore;
using MyBlog.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected BlogDbContext _context;
        public DbSet<T> Set { get; private set; }
        public Repository(BlogDbContext context)
        {
            _context = context;
            var set = _context.Set<T>();
            set.Load();

            Set = set;
        }

        public virtual async Task<int> CreateAsync(T item)
        {
            await Set.AddAsync(item);
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<int> DeleteAsync(T item)
        {
            await Task.Run(() => Set.Remove(item));
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await Set.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await Set.FindAsync(id);
        }

        public virtual async Task<int> UpdateAsync(T item)
        {
            await Task.Run(() => Set.Update(item));
            return await _context.SaveChangesAsync();
        }
    }
}
