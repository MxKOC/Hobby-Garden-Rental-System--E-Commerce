using FieldRent.Data.Abstract;
using FieldRent.Data.Concrete.EfCore;
using FieldRent.Entity;

namespace FieldRent.Data.Concrete
{
    public class EfUserRepository : IUserRepository
    {
        private BlogContext _context;
        public EfUserRepository(BlogContext context)
        {
            _context = context;
        }

        public IQueryable<User> Users => _context.Users;


        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void EditUser(User user)
        {
            var entity = _context.Users.FirstOrDefault(i => i.UserId == user.UserId);

            if (entity != null)
            {


                foreach (var map in user.Maps.ToList())
                {
                    entity.Maps.Remove(map);
                }

                //entity.Maps = user.Maps;
                //entity.Maps.AddRange(user.Maps.Where(m => !entity.Maps.Contains(m)));
                _context.SaveChanges();
            }
        }



        public void EditUser2(User user)
        {
            var entity = _context.Users.FirstOrDefault(i => i.UserId == user.UserId);

            if (entity != null)
            {
                entity.Maps.AddRange(user.Maps.Where(m => !entity.Maps.Contains(m)));
                //entity.UserPrice = entity.UserPrice+user.UserPrice;
                _context.SaveChanges();
            }
        }



       
    }
}