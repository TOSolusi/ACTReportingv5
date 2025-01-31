using ACTReportingTools.Models;
using Syncfusion.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ACTReportingTools.Helpers
{
    public class CustomAggregate : ISummaryAggregate
    {

        public CustomAggregate()
        {

        }

        public string remarksCount { get; set; }
        public string calculatedTotalHours { get; set; }
        public string countPerson {  get; set; }

        public Action<IEnumerable, string, PropertyDescriptor> CalculateAggregateFunc()
        {
            return (items, property, pd) =>
            {
                var enumerableItems = items as IEnumerable<DisplayRecordModel>;
                if (pd.Name == "remarksCount")
                {
                    //this.remCount = enumarableItems
                    //this.remCount = enumerableItems.RemarkCount<DisplayRecordModel>(q => q.Remarks.Distinct);
                    this.remarksCount = enumerableItems.Where(q => ((q.Remarks != "") ||(q.Remarks ==null))).Count().ToString();
                }

                if (pd.Name == "calculatedTotalHours")
                {
                    this.calculatedTotalHours = enumerableItems.CountTotalHours<DisplayRecordModel>(h => h.TotalHours);
                }

                if(pd.Name == "countPerson")
                {
                    
                    this.countPerson = enumerableItems.Select(o => new { o.Name }).Distinct().Count().ToString();
                }
            };



            throw new NotImplementedException();
        }
    }

    public static class LinqExtensions
    {
        public static string RemarkCount<T>(this IEnumerable<T> values, Func<T, string?> selector)
        {
            int ret = 0;
            var count = values.Count();

            if (count > 0)
            {
                ret = 2;
            }

            

            return ret.ToString();

        }

        public static string CountTotalHours<T>(this IEnumerable<DisplayRecordModel> values, Func<T, string?> selector)
        {
            var stringResult = "";

            var count = values.Count();

            TimeSpan totalCalculatedHours = TimeSpan.Zero;

            if (count > 0)
            {
                foreach (DisplayRecordModel o in values)
                {
                    if (o.DailyTotal == null)
                    {
                        continue;
                    }
                    else
                    {
                        TimeSpan timeCounter = new TimeSpan(int.Parse(o.DailyTotal.Substring(0, 2)), int.Parse(o.DailyTotal.Substring(3, 2)), int.Parse(o.DailyTotal.Substring(6, 2)), 0);
                        totalCalculatedHours = totalCalculatedHours.Add(timeCounter);
                    }
                }

            }

            //stringResult = totalCalculatedHours.ToString(@"dd\:hh\:mm\");
            stringResult = $"{((totalCalculatedHours.Days * 24) + (totalCalculatedHours.Hours)).ToString("00")}:{totalCalculatedHours.Minutes.ToString("00")}";

            //stringResult = "test";
            return stringResult;
        }
    }
}