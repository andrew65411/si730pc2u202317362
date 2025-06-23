using Sales.Domain;
using System.Threading.Tasks;

namespace Sales.Domain.Repositories
{
    public interface IPurchaseOrderRepository
    {
        Task<PurchaseOrder?> FindByCustomerAndPoNumberAsync(string customer, string poNumber);
        Task AddAsync(PurchaseOrder order);
    }
}

