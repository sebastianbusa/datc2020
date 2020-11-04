using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Globalization;

namespace Models
{
    public  class  MetricEntity : TableEntity
 {
        public int count{get;set;}

         public static String GetTimestamp(DateTime value)
{
    return value.ToString("yyyyMMddHHmmssffff");
}
public  MetricEntity(string universitate, int count)
        {
            DateTime localDate = DateTime.Now;
            this.PartitionKey=universitate;
            this.RowKey=GetTimestamp(DateTime.Now);
            this.count=count;

        }
       



 }


}