using Microsoft.EntityFrameworkCore;
using Sales.Domain;

namespace Sales.Infrastructure.Repositories
{
    public class PurchaseOrderRepository : Sales.Domain.Repositories.IPurchaseOrderRepository
    {
        private readonly HialpesaDbContext _context;
        public PurchaseOrderRepository(HialpesaDbContext context)
        {
            _context = context;
        }

        public async Task<PurchaseOrder?> FindByCustomerAndPoNumberAsync(string customer, string poNumber)
        {
            return await _context.PurchaseOrders
                .FirstOrDefaultAsync(x => x.Customer == customer && x.PoNumber == poNumber);
        }

        public async Task AddAsync(PurchaseOrder order)
        {
            await _context.PurchaseOrders.AddAsync(order);
            await _context.SaveChangesAsync();
        }
    }
}
