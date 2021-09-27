using ArandaSoft.EntityFramework.Interfaces;
using ArandaSoft.EntityFramework.Model;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ArandaSoft.EntityFramework.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly ArandaSoftModel arandaSoftModel;

        public AppUserRepository(ArandaSoftModel arandaSoftModel)
        {
            this.arandaSoftModel = arandaSoftModel;
        }

        public async Task<AppUser> LoginUser(string userName, string password)
        {
            return await arandaSoftModel.AppUser.FirstOrDefaultAsync(x => x.Name == userName && x.Password == password);
        }

        public void Save()
        {
            arandaSoftModel.SaveChanges();
        }
    }
}