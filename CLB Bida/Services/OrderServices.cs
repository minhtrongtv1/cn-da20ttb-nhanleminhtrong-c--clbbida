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
   public class OrderServices
    {
        #region Header
        public bool CreateOrderHeader(int TableId)
        {
            try
            {
                using (var context = new BilliardContext())
                {
                    // check if exists Table id and status = InUse. If not, create order
                    if (!context.OrderHeaders.Any(x => x.TableId == TableId && x.TableStatus == true))
                    {
                        OrderHeader oh = new OrderHeader();
                        oh.TableId = TableId;
                        oh.StartDateTime = DateTime.Now;
                        oh.TableStatus = true;
                        context.OrderHeaders.Add(oh);
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                   
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool EditOrderHeader(OrderHeaderDto data)
        {
            try
            {
                using (var context = new BilliardContext())
                {
                     OrderHeader t = context.OrderHeaders.Find(data.InternalOrderNum);
                    if (t != null)
                    {
                        t.EndDateTime = DateTime.Now;
                        t.TableStatus = false;
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<OrderHeaderDto> Get(CommonFilterDto filter)
        {
            using (var context = new BilliardContext())
            {
                IQueryable<OrderHeader> listResult = context.OrderHeaders;
                if (filter.InternalOrderNum != 0)
                {
                    listResult = listResult.Where(x => x.InternalOrderNum == filter.InternalOrderNum);
                }
                var TableIds = listResult.Select(x => x.TableId).Distinct().ToList();
                var tables = context.Tables.Where(x => TableIds.Contains(x.TableId)).ToList();
                List<OrderHeaderDto> rerturnData = new List<OrderHeaderDto>();
                listResult.ToList().ForEach(x => rerturnData.Add(new OrderHeaderDto
                {
                    InternalOrderNum = x.InternalOrderNum,
                    TableId = x.TableId,
                    TableName = tables.Where(y => y.TableId == x.TableId).Select(y => y.TableName).FirstOrDefault(),
                    EndDateTime = x.EndDateTime,
                    StartDateTime = x.StartDateTime,
                    TableStatus = x.TableStatus
                }));

                return rerturnData;
            }
        }
        #endregion
        public bool ValidateTableStatus(int TableId)
        {
            using (var context = new BilliardContext())
            {
                bool lresult = context.OrderHeaders.Any(x => x.TableId == TableId && x.TableStatus == true);
                return lresult;
            }
        }
        public string PriceCalculate(int InternalOrderNum)
        {
            using (var context = new BilliardContext())
            {
                string result = "";
                var data = context.OrderHeaders.Find(InternalOrderNum);
                if (data != null)
                {
                    var startTime = data.StartDateTime;
                    var endTime = data.EndDateTime;
                    var total = (endTime - startTime).Value.TotalHours;
                    result = total.ToString("N2");
                }
                else
                {
                    result = string.Empty;
                }
                return result;
            }
        }
        #region Detail
        public List<OrderDetailDto> GetOrderDetails(CommonFilterDto filter)
        {
            using (var context = new BilliardContext())
            {
                int index = 1;
                List<OrderDetailDto> orderDetailDtos = new List<OrderDetailDto>();
                IQueryable<OrderDetail> query = context.OrderDetails;
                if (filter.InternalOrderNum != 0)
                {
                    query = query.Where(x => x.InternalOrderNum == filter.InternalOrderNum);
                }
                if (filter.InternalOrderLineNum != 0)
                {
                    query = query.Where(x => x.InternalOrderLineNum == filter.InternalOrderLineNum);
                }                
                 query.ToList().ForEach(x =>orderDetailDtos.Add( new OrderDetailDto
                {
                    Index = index++,
                    InternalOrderLineNum = x.InternalOrderLineNum,
                    InternalOrderNum = x.InternalOrderNum,
                    OrderQty = x.OrderQty,
                    ProductCode = x.ProductCode,
                    ProductName = x.ProductName,
                    OrderDate = x.OrderDate
                }));
                return orderDetailDtos;
            }
        }
        public bool CreateOrderDetail(List<OrderDetailDto> datas)
        {
            try
            {
                if (datas.Count > 0)
                {
                    using (var context = new BilliardContext())
                    {
                        var datetimeNow = DateTime.Now;
                        datas.ForEach(x => context.OrderDetails.Add(new OrderDetail
                        {
                            InternalOrderNum = x.InternalOrderNum,
                            ProductCode = x.ProductCode,
                            ProductName = x.ProductName,
                            OrderQty = x.OrderQty,
                            OrderDate = datetimeNow
                        }));
                        context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteOrderDetail(List<int> ListInternalOrderLineNum)
        {
            bool result = false;

            try
            {
                
                using (var context = new BilliardContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        foreach (var i in ListInternalOrderLineNum)
                        {
                            var dataremove = context.OrderDetails.Find(i);
                            if (dataremove != null)
                            {
                                context.OrderDetails.Remove(dataremove);                               
                            }
                        }
                        context.SaveChanges();
                        transaction.Commit();
                        result = true;
                    }
                   
                }
            }
            catch(Exception)
            {
                
            }
            return result;
        }
        #endregion
    }
}
