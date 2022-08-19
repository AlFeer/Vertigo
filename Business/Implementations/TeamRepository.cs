using Business.Services;
using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Implementations
{
    public class TeamRepository : ITeamService
    {
        private readonly AppDbContext _context;

        public TeamRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Team> Get(int? id)
        {
            if (id is null)
            {
                throw new NotImplementedException();
            }

            var data = await _context.Teams.Where(n => !n.IsDeleted && n.Id == id).FirstOrDefaultAsync();

            if (data is null)
            {
                throw new ArgumentNullException();
            }

            return data;
        }

        public async Task<List<Team>> GetAll()
        {
            var data = await _context.Teams.Where(n => !n.IsDeleted).ToListAsync();

            if (data is null)
            {
                throw new ArgumentNullException();
            }

            return data;
        }

        public async Task Create(Team entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException();
            }

            entity.CreatedDate = DateTime.Now;
            await _context.Teams.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Team entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException();
            }

            var data = await Get(entity.Id);

            entity.UpdatedDate = DateTime.Now;
            _context.Teams.Update(data);
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
            _context.Teams.Update(data);
            await _context.SaveChangesAsync();
        }
    }
}
