using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FieldRent.Data.Concrete.EfCore;
using FieldRent.Entity;
using FieldRent.Data.Abstract;
using Microsoft.EntityFrameworkCore;

namespace FieldRent.Jobs
{
    public class FinishRent
    {



        private BlogContext _context;
        
        public FinishRent(BlogContext context ,IMapRepository _mapRepository)
        {
            _context = context;

        }

        public void PrintMessage()
        {
            Console.WriteLine("------------------------------------------\n\n--------------------------------------------");

            var maps = _context.Maps.Include(x => x.Requests).ToList();


            foreach (var entity in maps)
            {
                if (entity.MapStop < DateTime.Now.AddDays(2))
                {

                    entity.MapIsActive = true;
                    entity.MapStart = null;
                    entity.MapStop = null;
                    entity.Time = null;
                    entity.UserId = null;
                    entity.Requests = null;
                    entity.MapCondition = "xxx";
                    Console.WriteLine(entity.MapPrice+"--->"+ entity.MapStop + "+++++++" + DateTime.Now );
                    _context.SaveChanges();
                }

                else { entity.MapCondition = "yyy"; 
                Console.WriteLine(entity.MapPrice+"--->"+ entity.MapStop + "---------" + DateTime.Now );}
            }








        }
    }
}