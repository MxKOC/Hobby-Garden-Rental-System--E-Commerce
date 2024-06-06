using System.ComponentModel.DataAnnotations;
using FieldRent.Entity;

namespace FieldRent.Models
{
    public class MapEditViewModel
    {



        [Display(Name = "User")]
        public int UserId { get; set; }

        public List<Map> MapCheckIds { get; set; } = new List<Map>();


        public Duration Time { get; set; }

    }




public class MapChangeEditViewModel
    {



        [Display(Name = "Map")]
        public int MapId { get; set; }

        public string? MapCoordinate { get; set; } 






    }

}