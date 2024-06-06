using FieldRent.Entity;

namespace FieldRent.Data.Abstract
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        void CreateUser(User user);
        void EditUser(User user);
        void EditUser2(User user);

    }
}