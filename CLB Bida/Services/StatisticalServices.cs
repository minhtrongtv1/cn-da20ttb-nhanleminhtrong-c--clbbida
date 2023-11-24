using CLB_Bida.Domain;
using CLB_Bida.Dto;
using CLB_Bida.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLB_Bida.Services
{
    public class StatisticalServices
    {
        public List<StatisticalDto> Get(CommonFilterDto filter)
        {
            var fromNewDate = new DateTime(filter.fromDate.Year, filter.fromDate.Month, filter.fromDate.Day, 0, 0, 0);
            var toNewDate = new DateTime(filter.toDate.Year, filter.toDate.Month, filter.toDate.Day, 0, 0, 0);
            using (var context = new BilliardContext())
            {
                List<StatisticalDto> data = new List<StatisticalDto>();

                data =
                            context.OrderDetails
                            .Where(x => DbFunctions.TruncateTime(x.OrderDate) >= fromNewDate && DbFunctions.TruncateTime(x.OrderDate) <= toNewDate)
                            .GroupBy(x => x.ProductCode)
                            .GroupJoin
                                        (
                                            context.Products.DefaultIfEmpty(),
                                            od => od.Key,
                                            prod => prod.Id,
                                            (od, prod) => new { ProductName = prod.FirstOrDefault().Name, ProductCode = od.Key, UnitPrice = prod.FirstOrDefault().UnitPrice, CatId = prod.FirstOrDefault().CategoryId, TotalQty = od.Sum(x=>x.OrderQty) }
                                        )
                            .GroupJoin
                                        (
                                            context.Categories.DefaultIfEmpty(),
                                            group => group.CatId,
                                            cat => cat.Id,
                                            (group, cat) => new StatisticalDto
                                            {
                                                CatId = cat.FirstOrDefault().Id,
                                                CatName = cat.FirstOrDefault().Name,
                                                ProductCode = group.ProductCode,
                                                ProductName = group.ProductName,
                                                TotalQty = group.TotalQty,
                                                UnitPrice = group.UnitPrice,
                                            }
                                        )
                                        .ToList()
                                         .Select((x) => new StatisticalDto
                                         {                                             
                                             CatId = x.CatId,
                                             CatName = x.CatName,
                                             ProductCode = x.ProductCode,
                                             ProductName = x.ProductName,
                                             TotalQty = x.TotalQty,
                                             UnitPrice = x.UnitPrice,
                                             TotalPrice = x.TotalQty * x.UnitPrice
                                         })                                         
                                        .ToList();

                var outsideOrders = context.OutsideOrders
                     .Where(x => DbFunctions.TruncateTime(x.OrderDate) >= fromNewDate && DbFunctions.TruncateTime(x.OrderDate) <= toNewDate)
                    .GroupBy(x => new { x.ProductId, x.CategoryId, x.UnitPrice })
                    .Select(y => new StatisticalDto
                    {
                        CatId = y.Key.CategoryId,
                        ProductCode = y.Key.ProductId,
                        TotalQty = y.Sum(z => z.OrderQty),
                        TotalPrice = y.Sum(z => z.TotalPrice),
                        UnitPrice = y.Key.UnitPrice,
                    }).ToList();

                var listCatId = outsideOrders.Select(x => x.CatId);
                var listProId = outsideOrders.Select(x => x.ProductCode);
                var categories = context.Categories.Where(x => listCatId.Contains(x.Id));
                var products = context.Products.Where(x => listProId.Contains(x.Id));

                foreach (var i in outsideOrders)
                {
                    i.ProductName = products.Where(x => x.Id == i.ProductCode).Select(x => x.Name).FirstOrDefault();
                    i.CatName = categories.Where(x => x.Id == i.CatId).Select(x => x.Name).FirstOrDefault();
                }
                // Combine Outside Order and Internal Order 

                data.AddRange(outsideOrders);

                int indexs = 1;
                data = data.GroupBy(x => new { x.CatId, x.ProductCode}).Select(x => new StatisticalDto
                {
                    Index = indexs++,
                    CatId = x.Key.CatId,
                    CatName = x.FirstOrDefault().CatName,
                    ProductCode = x.Key.ProductCode,
                    ProductName = x.FirstOrDefault().ProductName,
                    TotalPrice = x.Sum(y=>y.TotalPrice),
                    TotalQty = x.Sum(y=>y.TotalQty),
                    UnitPrice = x.FirstOrDefault().UnitPrice
                }).ToList();
                return data;
            }
        }
        public List<StatisticalDto> Get(int InternalOrderNum)
        {
            using (var context = new BilliardContext())
            {                
                List<StatisticalDto> data = new List<StatisticalDto>();                

                data = context.OrderDetails.Where(x => x.InternalOrderNum == InternalOrderNum)
                    .GroupBy(x => x.ProductCode)
                    .Select(x => new StatisticalDto
                    {
                        ProductCode = x.Key,
                        TotalQty = x.Sum(y => y.OrderQty),
                    })
                    .ToList()
                    .Join
                    (
                        context.Products,
                        od => od.ProductCode,
                        prod => prod.Id,
                        (od, prod) => new StatisticalDto
                        {
                            ProductCode = od.ProductCode,
                            ProductName = prod.Name,
                            TotalQty = od.TotalQty,
                            UnitPrice = prod.UnitPrice,
                            TotalPrice = od.TotalQty * prod.UnitPrice,
                            CatId = prod.CategoryId
                        }
                    )
                    .Join
                    (
                        context.Categories,
                        od => od.CatId,
                        cat => cat.Id,
                        (od, cat) => new StatisticalDto
                        {
                            ProductCode = od.ProductCode,
                            ProductName = od.ProductName,
                            CatId = od.CatId,
                            CatName = cat.Name,
                            TotalPrice = od.TotalPrice,
                            TotalQty = od.TotalQty,
                            UnitPrice = od.UnitPrice
                        }
                    )
                    .Select((x, index) => new StatisticalDto
                     {
                         Index = index + 1,
                         CatId = x.CatId,
                         CatName = x.CatName,
                         ProductCode = x.ProductCode,
                         ProductName = x.ProductName,
                         TotalQty = x.TotalQty,
                         TotalPrice = x.TotalPrice,
                         UnitPrice = x.UnitPrice
                     })
                    .ToList();


                return data;
            }
        }
    }
}
