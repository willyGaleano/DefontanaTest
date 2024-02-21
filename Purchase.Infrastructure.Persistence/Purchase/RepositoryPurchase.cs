using Microsoft.EntityFrameworkCore;
using Purchase.Core.Application.Purchase;
using Purchase.Core.Domain.Entities;
using Purchase.Infrastructure.Persistence.Contexts;

namespace Purchase.Infrastructure.Persistence.Purchase
{
    public class RepositoryPurchase : IRepositoryPurchase
    {
        private readonly PruebaContext _pruebaContext;
        public RepositoryPurchase(PruebaContext pruebaContext)
        {
            _pruebaContext = pruebaContext;
        }
        public async Task<List<VentaDetalle>> GetSalesByLastDays(int days)
        {
            var startDate = DateTime.Now.AddDays(-days);
            var endDate = DateTime.Now;
            Console.WriteLine($"STARTDATE: {startDate}");
            Console.WriteLine($"ENDDATE: {endDate}");

            var sales = await _pruebaContext.VentaDetalles
                        .Where(vd => vd.IdVentaNavigation.Fecha >= startDate 
                            && vd.IdVentaNavigation.Fecha <= endDate)
                        .Include(v => v.IdVentaNavigation.IdLocalNavigation)
                        .Include(p => p.IdProductoNavigation.IdMarcaNavigation)
                        .ToListAsync();
            
            return sales;
        }
    }
}
