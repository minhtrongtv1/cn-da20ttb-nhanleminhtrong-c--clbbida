using CLB_Bida.Domain;
using CLB_Bida.Dto;
using CLB_Bida.Infrastructure;
using CLB_Bida.Ultils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLB_Bida.Services
{
    public class ProductServices
    {
        public List<ProductDto> GetAll()
        {
            using (var context = new BilliardContext())
            {
                List<ProductDto> data = new List<ProductDto>();

                data = context.Products
                     .GroupJoin
                                 (
                                         context.Categories,
                                         prod => prod.CategoryId,
                                         cat => cat.Id,
                                         (prod, cat) =>  new ProductDto
                                         {

                                             Id = prod.Id,
                                             Name = prod.Name,
                                             CategoryId = cat.FirstOrDefault().Id,
                                             CategoryName = cat.FirstOrDefault().Name,
                                             UnitPrice = prod.UnitPrice
                                         }
                                   )
                                 .ToList()
                                 .Select((x,index) => 
                                                        new ProductDto 
                                                        { 
                                                            Index = index + 1,
                                                            Id = x.Id,
                                                            CategoryId = x.CategoryId,
                                                            CategoryName = x.CategoryName,
                                                            Name = x.Name,
                                                            UnitPrice = x.UnitPrice 
                                                        })

                                 .ToList();

                return data;
            }             
        }
        public List<ProductDto> Get(CommonFilterDto filter)
        {
            using (var context = new BilliardContext())
            {
                List<ProductDto> data = new List<ProductDto>();

                IQueryable<Product> query = context.Products;

                if (filter.CatId != -1)
                {
                    query = query.Where(x => x.CategoryId == filter.CatId);
                }
                if (filter.ProductCode != -1)
                {
                    query = query.Where(x => x.Id == filter.ProductCode);
                }
                  data = query.AsEnumerable()
                    .Select((x,index) => new ProductDto
                        {
                            Index = index +1,
                            CategoryId = x.CategoryId,
                            Id = x.Id,
                            Name = x.Name,
                            UnitPrice = x.UnitPrice
                        })
                    .ToList();
                return data;
            }
        }
        public bool IsProductExist(int ProductId)
        {
            using (var context = new BilliardContext())
            {
                return context.Products.Any(x => x.Id == ProductId);
            }
        }
        public bool CreateProduct(ProductDto product)
        {
            try
            {
                using (var context = new BilliardContext())
                {
                    context.Products.Add(new Product
                    {
                        CategoryId = product.CategoryId,
                        Name = product.Name,
                        UnitPrice = product.UnitPrice                        
                    });
                    context.SaveChanges();
                    return true;
                }
            }
            catch(Exception)
            {
                return false;
            }
        }
        public bool EditProduct(ProductDto product)
        {
            try
            {
                using (var context = new BilliardContext())
                {
                    Product p = context.Products.Find(product.Id);
                    p.Name = product.Name;
                    p.UnitPrice = product.UnitPrice;
                    p.CategoryId = product.CategoryId;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public string IsInUse(List<int> ProductId)
        {
            using (var context = new BilliardContext())
            {
                var data = context.OrderDetails
                                    .Where(
                                                x => ProductId.Contains(x.ProductCode)).Select(x=>x.ProductCode
                                           )
                                                .Distinct()
                                                 .GroupJoin
                                                 (
                                                    context.Products,
                                                    od => od,
                                                    prod => prod.Id,
                                                    (od,prod) => new {ProductName = prod.FirstOrDefault().Name}
                                                   )
                                                 .Select(x=>x.ProductName)
                                           .ToList();
                var dataOutSideOrder = context.OutsideOrders
                    .Where(x => ProductId.Contains(x.ProductId)).Select(x => x.ProductId).Distinct()
                    .GroupJoin
                    (
                    context.Products,
                    od => od,
                    prod => prod.Id,
                    (od, prod) => new { ProductName = prod.FirstOrDefault().Name }
                    )
                    .Select(x=> x.ProductName)
                    .ToList();

                data.AddRange(dataOutSideOrder);

                data = data.Distinct().ToList();

                if (data.Count > 0)
                {
                    return $"Danh sách sản phẩm đang được sử dụng \nKhông thể xoá:\n{string.Join(Environment.NewLine, data)}";
                }
                else
                {
                    return Constants.OK;
                }
            }
        }
        public bool DeleteProduct(List<int> ProductId)
        {
            try
            {
                using (var context = new BilliardContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        foreach (var i in ProductId)
                        {
                            Product p = context.Products.Find(i);

                            context.Products.Remove(p);
                        }
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
