using FieldRent.Entity;

namespace FieldRent.Data.Abstract
{
    public interface IFieldRepository
    {
        IQueryable<Field> Fields { get; }
        void CreateField(Field field);
    }
}