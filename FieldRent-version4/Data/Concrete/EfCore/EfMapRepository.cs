using FieldRent.Data.Abstract;
using FieldRent.Data.Concrete.EfCore;
using FieldRent.Entity;
using Microsoft.AspNetCore.Http.Features;

namespace FieldRent.Data.Concrete
{
    public class EfMapRepository : IMapRepository
    {
        private BlogContext _context;
        public EfMapRepository(BlogContext context)
        {
            _context = context;
        }
        public IQueryable<Map> Maps => _context.Maps;

        public void CreateMap(Map map)
        {
            _context.Maps.Add(map);
            _context.SaveChanges();
        }

        public void EditMap(Map map)
        {
            var entity = _context.Maps.FirstOrDefault(i => i.MapId == map.MapId);

            if (entity != null)
            {

                //entity.Requests.AddRange(map.Requests.Where(m => !entity.Requests.Contains(m)));
                entity.Requests = map.Requests;
                _context.SaveChanges();
            }
        }



        public void EditMap2(Map map)
        {
            var entity = _context.Maps.FirstOrDefault(i => i.MapId == map.MapId);

            if (entity != null)
            {

                entity.MapIsActive = map.MapIsActive;
                _context.SaveChanges();
            }
        }



        public void EditMap3reqdelete(Map map)
        {
            var entity = _context.Maps.FirstOrDefault(i => i.MapId == map.MapId);


            if (entity != null)
            {
                entity.Requests.Clear();
                entity.MapIsActive = map.MapIsActive;

                _context.SaveChanges();
            }
        }



        public void EditMap4delrequser(Map map)
        {
            var entity = _context.Maps.FirstOrDefault(i => i.MapId == map.MapId);


            if (entity != null)
            {
                entity.UserId = null;
                entity.Requests = null;
                entity.MapIsActive = map.MapIsActive;
                entity.MapStart = map.MapStart;
                entity.MapStop = map.MapStop;
                entity.Time = map.Time;

                _context.SaveChanges();
            }
        }



        public void EditMap5addrequser(Map map)
        {
            var entity = _context.Maps.FirstOrDefault(i => i.MapId == map.MapId);


            if (entity != null)
            {
                entity.UserId = map.UserId;
                entity.Requests = map.Requests;
                entity.MapIsActive = map.MapIsActive;
                entity.MapStart = map.MapStart;
                entity.MapStop = map.MapStop;
                entity.Time = map.Time;
                _context.SaveChanges();
            }
        }

        public void EditMap6Duration(Map map)
        {
            var entity = _context.Maps.FirstOrDefault(i => i.MapId == map.MapId);


            if (entity != null)
            {
                entity.Time = map.Time;
                entity.MapStart = map.MapStart;
                entity.MapStop = map.MapStop;
                


                _context.SaveChanges();
            }
        }

    }
}