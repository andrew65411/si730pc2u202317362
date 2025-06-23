namespace Sales.Application.Dto
{
    public class PurchaseOrderResponseDto
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public string PoNumber { get; set; }
        public int FabricId { get; set; }
        public string Vendor { get; set; }
        public string ShipTo { get; set; }
        public int Quantity { get; set; }
    }
}

