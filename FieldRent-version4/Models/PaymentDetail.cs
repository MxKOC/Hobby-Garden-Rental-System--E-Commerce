using FieldRent.Entity;

namespace FieldRent.Models
{
    public class PaymentDetail
    {
        public string CardNumber { get; set; }
        public string FullName { get; set; }
        public int LastMonth { get; set; }
        public int LastYear { get; set; }
        public int Cvc { get; set; }
        public int[] MapIds { get; set; }
        public List<int> ShoppingCartIds { get; set; }
        public int UserId { get; set; }
        public Duration? Time { get; set; }

    }
}
