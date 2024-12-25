﻿using Microsoft.EntityFrameworkCore;
using WebApplication1.Repositories.Interfaces;

namespace WebApplication1.Repositories.Implementations
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }



        public IQueryable<Category> GetAll()
        {
            return _context.Categories;
        }





        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }




        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }



        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }



        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
        }




        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
