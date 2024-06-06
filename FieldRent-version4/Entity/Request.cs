using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FieldRent.Entity
{
    public class Request
    {
        public int RequestId { get; set; }
        public String? RequestName { get; set; }
        public int RequestPrice { get; set; }
        /*public DateTime RequestStart { get; set; }
        public DateTime RequestStop { get; set; }
        public bool RequestIsActive { get; set; }
*/
        public List<Map> Maps { get; set; } = new List<Map>();

    }
}