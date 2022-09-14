using HotelManagementSystem.Core.IRepositories;

namespace HotelManagementSystem.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IHotelRepository Hotel { get; set; }
        Task CompleteAsync();
    }
}
