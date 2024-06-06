using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FieldRent.Entity;

namespace FieldRent.Models
{
    public class PaymentDataViewModel
    {
    public List<int> ShoppingCartIds { get; set; }
    public int UserId { get; set; }
    public Duration? Time { get; set; }
    public int[] MapIds { get; set; }

}
    
}