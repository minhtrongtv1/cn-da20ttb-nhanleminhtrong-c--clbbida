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
    public class CategoryServices
    {
        public List<CategoryDto> GetAll()
        {
            using (var context = new BilliardContext())
            {
                List<CategoryDto> data = new List<CategoryDto>();

               data = context.Categories.Select(x => new CategoryDto
                {                    
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
                return data;
            }             
        }
        public List<CategoryDto> Get(CommonFilterDto filter)
        {
            using (var context = new BilliardContext())
            {
                List<CategoryDto> data = new List<CategoryDto>();

                data = context.Categories
                    .Where(x=>x.Id == filter.ProductCode)
                    .Select(x => new CategoryDto
                        {
                        Id = x.Id,
                        Name = x.Name
                    })
                    .ToList();
                return data;
            }
        }
        public bool IsCategoryExist(int CatId)
        {
            using (var context = new BilliardContext())
            {
                return context.Categories.Any(x => x.Id == CatId);
            }
        }
        public bool CreateCategory(CategoryDto cat)
        {
            try
            {
                using (var context = new BilliardContext())
                {
                    context.Categories.Add(new Category
                    {
                       Name = cat.Name
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
        public bool EditCategory(CategoryDto cat)
        {
            try
            {
                using (var context = new BilliardContext())
                {
                    Category p = context.Categories.Find(cat.Id);
                    p.Name = cat.Name;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteCategory(int CatId)
        {
            try
            {
                using (var context = new BilliardContext())
                {
                    Category p = context.Categories.Find(CatId);
                    context.Categories.Remove(p);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
