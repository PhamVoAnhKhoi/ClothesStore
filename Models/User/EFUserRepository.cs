using System.Linq;

namespace ClothesStore.Models
{
    public class EFUserRepository : IUserRepository
    {
        private StoreDBContext context;

        public EFUserRepository(StoreDBContext ctx)
        {
            context = ctx;
        }

        public IQueryable<User> Users => context.Users;

        public void AddUser(User user)
        {
            context.Users.Add(user);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
