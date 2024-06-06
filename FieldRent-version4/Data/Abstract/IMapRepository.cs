using FieldRent.Entity;

namespace FieldRent.Data.Abstract
{
    public interface IMapRepository
    {
        IQueryable<Map> Maps { get; }
        void CreateMap(Map map);
        void EditMap(Map map);
        void EditMap2(Map map);
        void EditMap3reqdelete(Map map);
        void EditMap4delrequser(Map map);
        void EditMap5addrequser(Map map);
        void EditMap6Duration(Map map);
        
    }
}