using FieldRent.Data.Abstract;
using FieldRent.Data.Concrete.EfCore;
using FieldRent.Entity;

namespace FieldRent.Data.Concrete
{
    public class EfRequestRepository : IRequestRepository
    {
        private BlogContext _context;
        public EfRequestRepository(BlogContext context)
        {
            _context = context;
        }
        public IQueryable<Request> Requests => _context.Requests;

        public void CreateRequest(Request request)
        {
            _context.Requests.Add(request);
            _context.SaveChanges();
        }


    }
}