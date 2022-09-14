using AutoMapper;
using HotelManagementSystem.Core.IRepositories;
using HotelManagementSystem.Data;
using HotelManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Nest;
using System.Data.Entity.Spatial;

namespace HotelManagementSystem.Core.Repository
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
        private readonly IMapper _mapper;
        public HotelRepository(AppDbContext _context, ILogger logger, IMapper mapper) : base(_context, logger)
        {
            _mapper = mapper;
        }

        public override async Task<IEnumerable<Hotel>> GetAll()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<Hotel>();
            }
        }
        public override async Task<bool> Add(Hotel entity)
        {
            try
            {
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedDate = DateTime.Now;
                await dbSet.AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }

        }
        public override async Task<bool> Update(Hotel entity)
        {
            try
            {
                var existingRecord = await dbSet
                                .Where(x => x.Id == entity.Id)
                                .FirstOrDefaultAsync();
                if (existingRecord != null)
                {
                    existingRecord.Id = entity.Id;
                    existingRecord.Name = entity.Name;
                    existingRecord.Price = entity.Price;
                    existingRecord.Latitude = entity.Latitude;
                    existingRecord.Longitude = entity.Longitude;
                    existingRecord.ModifiedDate = DateTime.Now;
                    dbSet.Update(existingRecord);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
            
        }
        public override async Task<bool> Remove(Guid Id)
        {
            try
            {
                var existingRecord = await dbSet
                                .Where(x => x.Id == Id)
                                .FirstOrDefaultAsync();
                if (existingRecord != null)
                {
                    dbSet.Remove(existingRecord);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }

        }
    }
}
