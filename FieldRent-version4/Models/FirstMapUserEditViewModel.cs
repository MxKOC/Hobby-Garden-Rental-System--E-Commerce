using System.ComponentModel.DataAnnotations;
using FieldRent.Entity;

namespace FieldRent.Models
{
    public class MapReqEditViewModel
    {

        [Display(Name = "User")]
        public int UserId { get; set; }

        public List<Request> MapReqCheckIds { get; set; } = new List<Request>();

        public bool MapIsActive { get; set; }


    }
}