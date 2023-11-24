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
    public class OperationServices
    {
        public List<MainOperationDto> GetMainOperations()
        {
            using (var context = new BilliardContext())
            {
                var returnData = new List<MainOperationDto>();
                var data = context.MainOperations.ToList();
                data.ForEach(x => returnData.Add(new MainOperationDto 
                {
                    Id = x.Id,
                    TableId = x.TableId,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    TableStatus = x.TableStatus == false ? "None" : "In Use"
                }));
                return returnData;
            }
        }
        public bool CreateMainOperationRecord(int tableId)
        {
            using (var context = new BilliardContext())
            {
                try
                {
                    if (context.MainOperations.Any(x => x.TableId == tableId && x.TableStatus == true))
                    {
                        return false;
                    }
                    else
                    {
                        MainOperation record = new MainOperation();
                        record.TableId = tableId;
                        record.StartTime = DateTime.Now;
                        record.TableStatus = true;
                        context.MainOperations.Add(record);
                        context.SaveChanges();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public bool UpdateEndOperationRecord(int Id)
        {
            using (var context = new BilliardContext())
            {
                try
                {
                    var OldData = context.MainOperations.Find(Id);
                    if (OldData != null)
                    {
                        OldData.TableStatus = false;
                        OldData.EndTime = DateTime.Now;
                        context.SaveChanges();
                        return true;
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
        }
        public string PriceCalculate(int id)
        {
            using (var context = new BilliardContext())
            {
                string result = "";
                var data = context.MainOperations.Find(id);
                if (data != null)
                {
                    var startTime = data.StartTime;
                    var endTime = data.EndTime;
                    var total = (endTime - startTime).Value.TotalHours;
                    result = $@"Tổng thời gian chơi là : {total.ToString("N2")} giờ";
                }
                else
                {
                    result = string.Empty;
                }
                return result;
            }
        }
    }
}
