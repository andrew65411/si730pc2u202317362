using Sales.Domain.ValueObjects;

namespace Sales.Application.Dto
{
    /// <summary>
    /// Data Transfer Object for Purchase Order
    /// </summary>
    /// <remarks>Created by Student U202317362</remarks>
    public class PurchaseOrderDto
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public string PoNumber { get; set; }
        public EFabric FabricId { get; set; }
        public string Vendor { get; set; }
        public string ShipTo { get; set; }
        public int Quantity { get; set; }
    }
}
