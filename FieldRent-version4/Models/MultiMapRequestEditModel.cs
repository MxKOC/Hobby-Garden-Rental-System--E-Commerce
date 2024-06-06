using System.ComponentModel.DataAnnotations;
using FieldRent.Entity;

namespace FieldRent.Models
{


    public class MultiMapRequestEditModel
    {
        [Display(Name = "User")]
        public List<int> MapIds { get; set; } = new List<int>();

    }
}