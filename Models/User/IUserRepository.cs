using ClothesStore.Models;

public interface IUserRepository
{
    IQueryable<User> Users { get; }

    void AddUser(User user);

    void Save();
}
