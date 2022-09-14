using AutoMapper;
using HotelManagementSystem.Core.IConfiguration;
using HotelManagementSystem.Core.IRepositories;
using HotelManagementSystem.Core.Repository;

namespace HotelManagementSystem.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public IHotelRepository Hotel { get; set; }

        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory, IMapper mapper)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");
            _mapper = mapper;

            Hotel = new HotelRepository(_context, _logger, _mapper);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
           _context.DisposeAsync();
        }
    }
}
