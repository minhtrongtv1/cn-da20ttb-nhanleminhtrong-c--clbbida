using CLB_Bida.Domain;
using CLB_Bida.Dto;
using CLB_Bida.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLB_Bida.Services
{
    public class OutsideOrderServices
    {
        public List<OutsideOrderDto> GetAll()
        {
            using (var context = new BilliardContext())
            {
                List<Category> cate = new List<Category>();
                List<OutsideOrderDto> data = new List<OutsideOrderDto>();

                data = context.OutsideOrders
                    .GroupJoin(
                    context.Products,
                    oo => oo.ProductId,
                    prod => prod.Id,
                    (oo, prod) => new OutsideOrderDto
                    {
                        ProductId = oo.ProductId,
                        ProductName = prod.FirstOrDefault().Name,
                        OrderQty = oo.OrderQty,
                        UnitPrice = prod.FirstOrDefault().UnitPrice,
                        CategoryId = oo.CategoryId,
                        OrderDate = oo.OrderDate
                    }                    
                    )
                    .ToList();
                var IdCats = data.Select(x => x.CategoryId).ToList();
                cate = context.Categories.Where(x => IdCats.Contains(x.Id)).ToList();

                int index = 1;
                foreach (var i in data)
                {
                    i.Index = index++;
                    i.CategoryName = cate.Where(y => y.Id == i.CategoryId)
                                                .Select(z => z.Name)
                                                .FirstOrDefault();
                    i.TotalPrice = i.UnitPrice * i.OrderQty;
                }

                return data;
            }
        }
        public List<OutsideOrderDto> Get(CommonFilterDto filter)
        {
            using (var context = new BilliardContext())
            {
                List<Category> cate = new List<Category>();
                List<OutsideOrderDto> data = new List<OutsideOrderDto>();

                data = context.OutsideOrders                    
                    .GroupJoin(
                    context.Products,
                    oo => oo.ProductId,
                    prod => prod.Id,
                    (oo, prod) => new OutsideOrderDto
                    {
                        ProductId = oo.ProductId,
                        ProductName = prod.FirstOrDefault().Name,
                        OrderQty = oo.OrderQty,
                        UnitPrice = prod.FirstOrDefault().UnitPrice,
                        CategoryId = oo.CategoryId
                    }
                    )
                    .ToList();

                cate = context.Categories.Where(x => data.Select(y => y.ProductId).Distinct().Contains(x.Id)).ToList();

                int index = 1;
                foreach (var i in data)
                {
                    i.Index = index++;
                    i.CategoryName = cate.Where(y => y.Id == i.CategoryId)
                                                .Select(z => z.Name)
                                                .FirstOrDefault();
                    i.TotalPrice = i.UnitPrice * i.OrderQty;
                }

                return data;
            }
        }
        public bool CreateOutsideOrder(OutsideOrderDto entity)
        {
            try
            {
                using (var context = new BilliardContext())
                {
                    var UnitPrice =  context.Products.Where(x => x.Id == entity.ProductId).Sum(x=>x.UnitPrice);
                    context.OutsideOrders.Add(new OutsideOrder
                    {
                        CategoryId = entity.CategoryId,
                        ProductId = entity.ProductId,
                        OrderQty = entity.OrderQty,
                        UnitPrice = UnitPrice,
                        TotalPrice = UnitPrice * entity.OrderQty,
                        OrderDate = DateTime.Now
                    });
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteOutsideOrder(List<int> Ids)
        {
            try
            {
                using (var context = new BilliardContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        foreach (var i in Ids)
                        {
                            OutsideOrder p = context.OutsideOrders.Find(i);

                            context.OutsideOrders.Remove(p);
                        }
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
