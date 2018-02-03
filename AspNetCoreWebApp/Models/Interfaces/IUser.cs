
namespace AspNetCoreWebApp.Models.Interfaces
{
    public interface IUser<T>
    {
        T Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        IAddress Address { get; set; }
        string Email { get; set; }
    }
}
