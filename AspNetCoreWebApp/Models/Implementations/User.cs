
using System.ComponentModel.DataAnnotations;

using AspNetCoreWebApp.Models.Interfaces;

namespace AspNetCoreWebApp.Models.Implementations
{
    public class User<T> : IUser<T>
    {
        public T Id { get; set; }
        [Required, StringLength(20)]
        public string FirstName { get; set; }
        [Required, StringLength(20)]
        public string LastName { get; set; }
        public IAddress Address { get; set; }
        public string Phone { get; set; }
        [Required,StringLength(40)]
        public string Email { get; set; }
    }
}