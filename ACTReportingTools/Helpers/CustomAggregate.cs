using ACTReportingTools.Models;
using ACTReportingTools.ViewModels;
using Caliburn.Micro;
using Newtonsoft.Json.Linq;
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
            fileReportSettings = IoC.Get<FileLocationViewModel>().FileReportSettings;
            SettingsConfig = ConfigHelper.LoadConfig(fileReportSettings);
            weeklyHours = (string)SettingsConfig["TotalWeeklyHours"];
            timeWeeklyHours = new TimeSpan(int.Parse(weeklyHours.Substring(0, 2)), 0, 1);

        }
        public string fileReportSettings { get; set; }
        public string remarksCount { get; set; }
        public string calculatedTotalHours { get; set; }
        public TimeSpan calcTotalHours { get; set; }
        public string countPerson {  get; set; }
        public string countWeeklyHours { get; set; }
        public JObject SettingsConfig { get; set; }
        public string weeklyHours { get; set; }
        public TimeSpan timeWeeklyHours { get; set; }

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
                    calcTotalHours = enumerableItems.CountTotalHours<DisplayRecordModel>(h => h.TotalHours);
                    var stringResult = $"{((calcTotalHours.Days * 24) + (calcTotalHours.Hours)).ToString("00")}:{calcTotalHours.Minutes.ToString("00")}";
                    this.calculatedTotalHours = stringResult;
                }

                if(pd.Name == "countPerson")
                {
                    
                    this.countPerson = enumerableItems.Select(o => new { o.Name }).Distinct().Count().ToString();
                }
                if(pd.Name == "countWeeklyHours")
                {
                    if (this.calcTotalHours < timeWeeklyHours)
                    {
                        this.countWeeklyHours = $" Weekly Total under {timeWeeklyHours.TotalHours.ToString("00")} hours";
                        
                        
                    }
                    else
                    {
                        this.countWeeklyHours = "";
                    }

                   
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

        public static TimeSpan CountTotalHours<T>(this IEnumerable<DisplayRecordModel> values, Func<T, string?> selector)
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
                        TimeSpan timeCounter = new TimeSpan(0);
                        if (o.DailyTotal.Substring(0,1) == "-")
                        {
                            timeCounter = new TimeSpan(int.Parse(o.DailyTotal.Substring(1, 2)), int.Parse(o.DailyTotal.Substring(4, 2)), 0).Negate();  //.Negate();  //, int.Parse(o.DailyTotal.Substring(7, 2)), 0).Negate();
                        }
                        else
                        {
                             timeCounter = new TimeSpan(int.Parse(o.DailyTotal.Substring(0, 2)), int.Parse(o.DailyTotal.Substring(3, 2)), 0);
                            
                        }
                        totalCalculatedHours = totalCalculatedHours.Add(timeCounter);
                    }
                }

            }

            return totalCalculatedHours;
            //stringResult = totalCalculatedHours.ToString(@"dd\:hh\:mm\");
            

            //stringResult = "test";
            //return stringResult;
        }
    }
}