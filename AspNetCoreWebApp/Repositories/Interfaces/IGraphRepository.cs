using System.Threading.Tasks;

namespace AspNetCoreWebApp.Repositories.Interfaces
{
    public interface IGraphRepository
    {
      string Ping(string message);
      bool ChangePassword(string password);
    }
}
