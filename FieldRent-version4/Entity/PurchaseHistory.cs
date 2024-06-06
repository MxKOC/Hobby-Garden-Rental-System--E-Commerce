namespace FieldRent.Entity
{
    public class PurchaseHistory
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
        public Duration? Time { get; set; }
        public int MapId { get; set; }
        public Map Map { get; set; }

        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }


        public int UserId { get; set; }
        public User User { get; set; }
    }
}
