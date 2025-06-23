using Sales.Domain.ValueObjects;

namespace Sales.Application.Commands.PurchaseOrders
{
    /// <summary>
    /// Comando para crear una nueva orden de compra
    /// </summary>
    /// <remarks>Creado por el estudiante U202317362</remarks>
    public class CreatePurchaseOrderCommand
    {
        public string Customer { get; set; }
        public string PoNumber { get; set; }
        public EFabric FabricId { get; set; }
        public string Vendor { get; set; }
        public string ShipTo { get; set; }
        public int Quantity { get; set; }
    }
}