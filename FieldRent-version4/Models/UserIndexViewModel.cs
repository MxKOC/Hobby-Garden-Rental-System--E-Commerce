using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FieldRent.Entity;

namespace FieldRent.Models
{
    public class UserIndexViewModel
    {

        public List<Map> maps { get; set; }
        public List<PurchaseHistory> purchaseHistories { get; set; }
    }

}