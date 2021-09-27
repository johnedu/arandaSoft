using ArandaSoft.EntityFramework.Model;
using System.Threading.Tasks;

namespace ArandaSoft.EntityFramework.Interfaces
{
    public interface IAppUserRepository
    {
        Task<AppUser> LoginUser(string userName, string password);
        void Save();
    }
}