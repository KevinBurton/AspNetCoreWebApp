namespace AspNetCoreWebApp.Models.Interfaces
{
    public interface IAddress
    {
      string Street { get; set; }
      string City { get; set; }
      string Region { get; set; }
      string PostalCode { get; set; }
      ICoordinates Coordinates { get; set; }
    }
}
