using AspNetCoreWebApp.Models.Interfaces;

namespace AspNetCoreWebApp.Models.Implementations
{
    public class Address : IAddress
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public ICoordinates Coordinates { get; set; }
    }
}