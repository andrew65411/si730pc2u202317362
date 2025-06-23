namespace Sales.Application.Dto
{
    public class PurchaseOrderRequestDto
    {
        public string Customer { get; set; }
        public string PoNumber { get; set; }
        public int? FabricId { get; set; } // Opcional, por defecto Algodon
        public string Vendor { get; set; }
        public string ShipTo { get; set; }
        public int Quantity { get; set; }
    }
}

