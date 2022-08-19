﻿using Business.Services;
using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Implementations
{
    public class SomeBlogRepository : ISomeBlogService
    {
        private readonly AppDbContext _context;

        public SomeBlogRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<SomeBlog> Get(int? id)
        {
            if (id is null)
            {
                throw new NotImplementedException();
            }

            var data = await _context.SomeBlogs.Where(n => !n.IsDeleted && n.Id == id).FirstOrDefaultAsync();

            if (data is null)
            {
                throw new ArgumentNullException();
            }

            return data;
        }

        public async Task<List<SomeBlog>> GetAll()
        {
            var data = await _context.SomeBlogs.Where(n => !n.IsDeleted).ToListAsync();

            if (data is null)
            {
                throw new ArgumentNullException();
            }

            return data;
        }

        public async Task Create(SomeBlog entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException();
            }

            entity.CreatedDate = DateTime.Now;
            await _context.SomeBlogs.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task Update(SomeBlog entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException();
            }

            var data = await Get(entity.Id);

            entity.UpdatedDate = DateTime.Now;
            _context.SomeBlogs.Update(data);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            if (id is null)
            {
                throw new NullReferenceException();
            }
            var data = await Get(id);

            data.IsDeleted = true;
            _context.SomeBlogs.Update(data);
            await _context.SaveChangesAsync();
        }
    }
}
