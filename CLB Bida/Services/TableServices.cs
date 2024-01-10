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
    public class TableServices
    {
        public bool CreateTable(TableDto data)
        {
            try
            {
                using (var context = new BilliardContext())
                {
                    Table t = new Table();
                    t.TableName = data.TableName;
                    t.UnitPrice = data.UnitPrice;
                    t.TableStatus = false;
                    context.Tables.Add(t);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool UpdateTableStatus(int TableId, bool Status)
        {
            try
            {
                using (var context = new BilliardContext())
                {
                    var data = context.Tables.Find(TableId);
                    if (data != null)
                    {
                        data.TableStatus = Status;
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
        public string ValidateTableAction(int TableId)
        {
            using (var context = new BilliardContext())
            {
                var table = context.Tables.Find(TableId);
                if (table != null)
                {
                    if (table.TableStatus == true)
                    {
                        return Constants.IN_USE;
                    }
                    else
                    {
                        return Constants.OK;
                    }
                }
                else
                {
                    return Constants.NOT_FOUND;
                }
            }
        }
        public bool EditTable(TableDto data)
        {
            try
            {
                using (var context = new BilliardContext())
                {
                    Table t = context.Tables.Find(data.TableId);
                    if (t != null && t.TableStatus == false)
                    {
                        t.TableName = data.TableName;
                        t.UnitPrice = data.UnitPrice;                        
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
        public bool DeleteTable(int TableId)
        {
            try
            {
                using (var context = new BilliardContext())
                {

                    var data = context.Tables.Find(TableId);
                    context.Tables.Remove(data);
                    context.SaveChanges();
                    return true;

                }
            }
            catch (Exception)
            {
                return false;
            }
           
        }
        public List<TableDto> Get(bool tableStatus)
        {
            using (var context = new BilliardContext())
            {
                List<Table> data = new List<Table>();

                var listTable = context.Tables
                    .Where(x => x.TableStatus == tableStatus)
                    .Select(x => new TableDto { TableId = x.TableId, TableName = x.TableName })
                    .ToList();

                return listTable;

            }
        }
        public TableDto GetById(int TableId)
        {
            using (var context = new BilliardContext())
            {
                return context.Tables.Where(x => x.TableId == TableId)
                    .Select(x => new TableDto
                    {
                        TableId = x.TableId,
                        TableName = x.TableName,
                        TableStatus = x.TableStatus == false ? Constants.OK : Constants.IN_USE,
                        UnitPrice = x.UnitPrice
                    }).FirstOrDefault();
            }
        }
        public List<Table> GetAll()
        {
            using (var context = new BilliardContext())
            {
                return context.Tables.ToList();
            }
        }
    }
}
