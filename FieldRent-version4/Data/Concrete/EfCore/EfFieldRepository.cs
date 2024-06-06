using FieldRent.Data.Abstract;
using FieldRent.Data.Concrete.EfCore;
using FieldRent.Entity;

namespace FieldRent.Data.Concrete
{
    public class EfFieldRepository : IFieldRepository
    {
        private BlogContext _context;
        public EfFieldRepository(BlogContext context)
        {
            _context = context;
        }
        public IQueryable<Field> Fields => _context.Fields;

        public void CreateField(Field field)
        {
            _context.Fields.Add(field);
            _context.SaveChanges();
        }


    }
}