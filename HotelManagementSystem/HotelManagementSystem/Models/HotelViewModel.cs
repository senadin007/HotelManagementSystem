namespace HotelManagementSystem.Models
{
    public class HotelViewModel
    {
        public HotelViewModel(HotelDTO data, double longitude, double latitude)
        {
            Data = data;
            Longitude = longitude;
            Latitude = latitude;
        }
        public HotelDTO Data { get; }
        public double Longitude { get; }
        public double Latitude { get; }
    }
}
