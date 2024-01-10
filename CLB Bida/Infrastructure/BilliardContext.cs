using CLB_Bida.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLB_Bida.Infrastructure
{
    public class BilliardContext : DbContext
    {
        public BilliardContext()
        {
            this.Database.Connection.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
            Database.SetInitializer<BilliardContext>(null);
        }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<MainOperation> MainOperations { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderHeader> OrderHeaders { get; set; }
        public virtual DbSet<Table> Tables { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<OutsideOrder> OutsideOrders { get; set; }

    }
}
