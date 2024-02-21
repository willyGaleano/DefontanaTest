using Purchase.Core.Domain.Entities;

namespace Purchase.Core.Application.Purchase
{
    public interface IRepositoryPurchase
    {
        Task<List<VentaDetalle>> GetSalesByLastDays(int days);
    }
}
