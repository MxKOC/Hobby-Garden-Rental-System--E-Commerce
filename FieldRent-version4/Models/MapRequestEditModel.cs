using System.ComponentModel.DataAnnotations;
using FieldRent.Entity;

namespace FieldRent.Models
{
    public class MapRequestEditModel
    {
        [Display(Name = "User")]
        public int MapId { get; set; }

        public List<Request> MapReqCheckIds { get; set; } = new List<Request>();
    }
}