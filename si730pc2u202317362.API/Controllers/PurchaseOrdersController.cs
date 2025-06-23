using Microsoft.AspNetCore.Mvc;
using Sales.Application.Dto;
using Sales.Application.Services;
using System.Net.Mime;

namespace si730pc2u202317362.API.Controllers
{
    /// <summary>
    /// Controlador para operaciones sobre Purchase Orders.
    /// </summary>
    /// <remarks>
    /// Autor: Andreow Santiago, Código: u202317362
    /// </remarks>
    [ApiController]
    [Route("api/v1/purchase-orders")]
    [Produces(MediaTypeNames.Application.Json)]
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly PurchaseOrderService _service;
        public PurchaseOrdersController(PurchaseOrderService service)
        {
            _service = service;
        }

        /// <summary>
        /// Agrega una nueva orden de compra.
        /// </summary>
        /// <param name="dto">Datos de la orden de compra.</param>
        /// <returns>La orden de compra creada.</returns>
        /// <response code="201">Orden creada exitosamente.</response>
        /// <response code="400">Datos inválidos.</response>
        /// <response code="409">Orden duplicada.</response>
        [HttpPost]
        [ProducesResponseType(typeof(PurchaseOrderResponseDto), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 409)]
        public async Task<IActionResult> Post([FromBody] PurchaseOrderRequestDto dto)
        {
            // Validaciones de negocio
            if (string.IsNullOrWhiteSpace(dto.Customer) || dto.Customer.Length < 4 || dto.Customer.Length > 50)
                return BadRequest("El campo customer es obligatorio y debe tener entre 4 y 50 caracteres.");
            if (string.IsNullOrWhiteSpace(dto.Vendor) || dto.Vendor.Length < 4 || dto.Vendor.Length > 50)
                return BadRequest("El campo vendor es obligatorio y debe tener entre 4 y 50 caracteres.");
            if (string.IsNullOrWhiteSpace(dto.PoNumber))
                return BadRequest("El campo poNumber es obligatorio.");
            if (string.IsNullOrWhiteSpace(dto.ShipTo))
                return BadRequest("El campo shipTo es obligatorio.");
            if (dto.Quantity <= 0)
                return BadRequest("El campo quantity es obligatorio y debe ser mayor a 0.");

            // Validación de fabricId
            int fabricId = dto.FabricId ?? 1; // 1 = Algodon
            if (!Enum.IsDefined(typeof(Sales.Domain.EFabric), fabricId))
                return BadRequest("El campo fabricId es inválido.");
            dto.FabricId = fabricId;

            try
            {
                var result = await _service.AddAsync(dto);
                return CreatedAtAction(nameof(Post), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Ya existe una orden"))
                    return Conflict(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
