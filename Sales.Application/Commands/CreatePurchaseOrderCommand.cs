namespace Sales.Application.Commands
{
    /// <summary>
    /// Command to create a purchase order.
    /// </summary>
    public class CreatePurchaseOrderCommand
    {
        public required string Customer { get; set; }
        public required string PoNumber { get; set; }
        public int? FabricId { get; set; }
        public required string Vendor { get; set; }
        public required string ShipTo { get; set; }
        public int Quantity { get; set; }
    }
}
