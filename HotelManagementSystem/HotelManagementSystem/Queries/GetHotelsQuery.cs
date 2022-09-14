using HotelManagementSystem.Data;
using HotelManagementSystem.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HotelManagementSystem.Core;

namespace HotelManagementSystem.Queries
{
    public class GetHotelsQuery : IRequest<PaginatedList<HotelDTO>>
    {
        public GetHotelsQuery(
            double longitude,
            double latitude,
            int pageNumber = 0,
            int pageSize = 0)
        {
            PageNumber = pageNumber <= 0 ? 1 : pageNumber;
            PageSize = pageSize <= 0 ? DefaultPageSize : pageSize;
            Longitude = longitude;
            Latitude = latitude;
        }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        private int DefaultPageSize { get; } = 10;

    }
    public class GetHotelsQueryHandler : IRequestHandler<GetHotelsQuery, PaginatedList<HotelDTO>>
    {
        private readonly AppDbContext _applicationDbContext;

        public GetHotelsQueryHandler(AppDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<PaginatedList<HotelDTO>> Handle(GetHotelsQuery request, CancellationToken cancellationToken)
        {
            //Latitude: 44.4689527
            //Longitude: 18.7735621

            var query = _applicationDbContext.Hotel
                .AsNoTracking()
                .Where(x => x.Price > 0)
                .AsQueryable();
            PaginatedList <HotelDTO> data = null;
            if (request.Longitude != 0 && request.Latitude != 0)
            {
                data = await query.Select(x => new HotelDTO
                {
                    Name = x.Name,
                    Price = (double)x.Price,
                    Distance = DistanceTo(request.Latitude, request.Longitude, x.Latitude, x.Longitude)
                })
                .OrderBy(x => x.Distance)
                .ThenBy(x => x.Price)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
            }         

            return data;
        }
        public static double DistanceTo(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            double rlat1 = Math.PI * latitude1 / 180;
            double rlat2 = Math.PI * latitude2 / 180;
            double theta = longitude1 - longitude2;
            double rtheta = Math.PI * theta / 180;
            double dist = Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) * Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;
            // return in km
            return Math.Round(dist * 1.609344,2);
            }
    }

    
}

