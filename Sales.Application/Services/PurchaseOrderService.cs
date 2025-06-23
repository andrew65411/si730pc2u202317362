using Sales.Application.Dto;
using Sales.Domain;
using Sales.Domain.Repositories;
using System.Threading.Tasks;

namespace Sales.Application.Services
{
    public class PurchaseOrderService
    {
        private readonly IPurchaseOrderRepository _repository;
        public PurchaseOrderService(IPurchaseOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<PurchaseOrderResponseDto> AddAsync(PurchaseOrderRequestDto dto)
        {
            // Validaciones de negocio
            if (string.IsNullOrWhiteSpace(dto.Customer) || dto.Customer.Length < 4 || dto.Customer.Length > 50)
                throw new Exception("El campo customer es obligatorio y debe tener entre 4 y 50 caracteres.");
            if (string.IsNullOrWhiteSpace(dto.Vendor) || dto.Vendor.Length < 4 || dto.Vendor.Length > 50)
                throw new Exception("El campo vendor es obligatorio y debe tener entre 4 y 50 caracteres.");
            if (string.IsNullOrWhiteSpace(dto.PoNumber))
                throw new Exception("El campo poNumber es obligatorio.");
            if (string.IsNullOrWhiteSpace(dto.ShipTo))
                throw new Exception("El campo shipTo es obligatorio.");
            if (dto.Quantity <= 0)
                throw new Exception("El campo quantity es obligatorio y debe ser mayor a 0.");

            // Validación de fabricId
            int fabricId = dto.FabricId ?? 1; // 1 = Algodon
            if (!Enum.IsDefined(typeof(EFabric), fabricId))
                throw new Exception("El campo fabricId es inválido.");
            dto.FabricId = fabricId;

            // Regla: No duplicados por customer + poNumber
            var existing = await _repository.FindByCustomerAndPoNumberAsync(dto.Customer, dto.PoNumber);
            if (existing != null)
                throw new Exception("Ya existe una orden con el mismo customer y poNumber.");

            var order = PurchaseOrder.Create(
                dto.Customer,
                dto.PoNumber,
                dto.FabricId,
                dto.Vendor,
                dto.ShipTo,
                dto.Quantity
            );

            await _repository.AddAsync(order);

            return new PurchaseOrderResponseDto
            {
                Id = order.Id,
                Customer = order.Customer,
                PoNumber = order.PoNumber,
                FabricId = order.Fabric.Id,
                Vendor = order.Vendor,
                ShipTo = order.ShipTo,
                Quantity = order.Quantity
            };
        }
    }
}
