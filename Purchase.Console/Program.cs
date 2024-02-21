using Purchase.Infrastructure.Persistence.Contexts;
using Purchase.Infrastructure.Persistence.Purchase;

using var context = new PruebaContext();
var repository = new RepositoryPurchase(context);

var days = 30;
var sales = await repository.GetSalesByLastDays(days);

//Pregunta 1
var montoTotal30 = sales.Sum(s => s.TotalLinea);
Console.WriteLine($"Monto total: {montoTotal30}");

var cantTotal30 = sales.Sum(s => s.Cantidad);
Console.WriteLine($"Cantidad total: {cantTotal30}");

//Pregunta 2
var ventaMontoMayor = sales.OrderByDescending(s => s.IdVentaNavigation.Total)
                           .Take(1)
                           .Select(s => s.IdVentaNavigation)
                           .First();
Console.WriteLine($"Fecha venta monto mayor: {ventaMontoMayor.Fecha}");

//Pregunta 3
var productoMontoTotalMayor = sales.OrderByDescending(s => s.TotalLinea)
                                   .Take(1)
                                   .Select(s => s.IdProductoNavigation)
                                   .First();
Console.WriteLine($"Producto con mayor monto total de ventas: {productoMontoTotalMayor.Nombre}");

//Pregunta 4
var localMontoMayorVenta = ventaMontoMayor.IdLocalNavigation;
Console.WriteLine($"Local con mayor monto total de ventas: {localMontoMayorVenta.Nombre}");

//Pregunta 5
var marcaMayorMargenGanancias = sales    
    .Select(s => new
    {
        Marca = s.IdProductoNavigation.IdMarcaNavigation.Nombre,
        MargenDeGanancias = (s.Cantidad * s.PrecioUnitario - s.Cantidad) * s.IdProductoNavigation.CostoUnitario
    })
    .OrderByDescending(s => s.MargenDeGanancias)
    .FirstOrDefault()?.Marca;

Console.WriteLine($"Marca con mayor margen de ganancias: {marcaMayorMargenGanancias}");

//Pregunta 6
var productosMasVendidosPorLocal = sales
    .GroupBy(s => new { s.IdVentaNavigation.IdLocalNavigation, s.IdProductoNavigation })
    .Select(g => new
    {
        Local = g.Key.IdLocalNavigation.Nombre,
        Producto = g.Key.IdProductoNavigation.Nombre,
        TotalVendido = g.Sum(s => s.Cantidad)
    })
    .GroupBy(x => x.Local)
    .Select(g => g.OrderByDescending(x => x.TotalVendido).FirstOrDefault());

foreach (var item in productosMasVendidosPorLocal)
{
    Console.WriteLine($"En {item?.Local}, el producto más vendido es: {item?.Producto}");
}

