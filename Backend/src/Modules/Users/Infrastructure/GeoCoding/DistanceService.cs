using System.Globalization;
using Users.Application.Contracts;
using Users.Domain.Users;

namespace Users.Infrastructure.GeoCoding
{
    internal class DistanceService : IDistanceService
    {
        private const double EarthRadius = 6.371e6;

        public int GetDistance(Coords coords1, Coords coords2)
        {
            double lat1 = Parse(coords1.Latitude) * (Math.PI / 180);
            double lat2 = Parse(coords2.Latitude) * (Math.PI / 180);
            double lon1 = Parse(coords1.Longitude) * (Math.PI / 180);
            double lon2 = Parse(coords1.Longitude) * (Math.PI / 180);

            double a = Math.Pow(Math.Sin((lat1 - lat2) / 2), 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin((lon1 - lon2) / 2), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a));

            return (int)(EarthRadius * c);
        }

        private double Parse(string s)
        {
            return double.Parse(s, CultureInfo.InvariantCulture);
        }
    }
}
