using Hialpesa.Shared.Domain.Model;
using Hialpesa.Shared.Domain.Interfaces;

namespace Sales.Domain
{
    /// <summary>
    /// Representa una orden de compra registrada por un cliente.
    /// </summary>
    /// <remarks>
    /// Autor: [Tu Nombre]
    /// Fecha: Noviembre 2024
    /// </remarks>
    public class PurchaseOrder : Entity<int>, IAggregateRoot
    {
        // Propiedades básicas
        public string Customer { get; private set; }
        public string PoNumber { get; private set; }
        public Fabric Fabric { get; private set; }
        public string Vendor { get; private set; }
        public string ShipTo { get; private set; }
        public int Quantity { get; private set; }

        // Campos de auditoría
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        // Constructor privado para DDD
        private PurchaseOrder() { }

        /// <summary>
        /// Crea una nueva instancia de PurchaseOrder con validaciones de negocio.
        /// </summary>
        public static PurchaseOrder Create(
            string customer,
            string poNumber,
            int? fabricId,
            string vendor,
            string shipTo,
            int quantity)
        {
            var fabric = new Fabric(fabricId ?? 1); // 1 = Algodon por defecto
            Validate(customer, poNumber, fabric, vendor, shipTo, quantity);
            return new PurchaseOrder
            {
                Customer = customer,
                PoNumber = poNumber,
                Fabric = fabric,
                Vendor = vendor,
                ShipTo = shipTo,
                Quantity = quantity,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Valida las reglas de negocio antes de crear o actualizar una PurchaseOrder.
        /// </summary>
        private static void Validate(
            string customer,
            string poNumber,
            Fabric fabric,
            string vendor,
            string shipTo,
            int quantity)
        {
            if (string.IsNullOrWhiteSpace(customer))
                throw new ArgumentException("Customer no puede ser nulo o vacío.");

            if (customer.Length < 4 || customer.Length > 50)
                throw new ArgumentException("Customer debe tener entre 4 y 50 caracteres.");

            if (string.IsNullOrWhiteSpace(poNumber))
                throw new ArgumentException("PoNumber no puede ser nulo o vacío.");

            if (string.IsNullOrWhiteSpace(vendor))
                throw new ArgumentException("Vendor no puede ser nulo o vacío.");

            if (vendor.Length < 4 || vendor.Length > 50)
                throw new ArgumentException("Vendor debe tener entre 4 y 50 caracteres.");

            if (string.IsNullOrWhiteSpace(shipTo))
                throw new ArgumentException("ShipTo no puede ser nulo o vacío.");

            if (quantity <= 0)
                throw new ArgumentException("Quantity debe ser mayor que cero.");

            // Validación de Fabric ya no es necesaria, se maneja en el value object
        }
    }
}