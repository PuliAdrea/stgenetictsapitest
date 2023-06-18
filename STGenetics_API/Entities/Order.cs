namespace STGenetics_API.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public bool ChargeFreight { get; set; }
    }
}
