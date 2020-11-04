using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Models;
using System.Linq;

namespace Tema5{
 
    
class ItemEqualityComparer : IEqualityComparer<StudentEntity>
{
    public bool Equals(StudentEntity x, StudentEntity y)
    {
    
        string a,b;
        a=x.RowKey;
        b=y.RowKey;
        return  a.Equals(b);   
    }

    public int GetHashCode(StudentEntity obj)
    {
        return obj.RowKey.GetHashCode();
    }
}
    public class MetricRepo 
    {
       private CloudTableClient tableClient;
        private CloudTable studentTable;
        private CloudTable metricTable;

        public MetricRepo()
        {
            Task.Run(async () => {await InitTable() ;})
            .GetAwaiter()
            .GetResult();
        
        }

   
          public async Task Update15m()
         {

            var students = new List<StudentEntity>();
            TableQuery<StudentEntity> query=new TableQuery<StudentEntity>();
            TableContinuationToken token=null;
            do{
                TableQuerySegment<StudentEntity> resultSegment=await studentTable.ExecuteQuerySegmentedAsync(query,token);
                students.AddRange(resultSegment.Results);
            }while(token!=null);
           List<StudentEntity> l=students;
            var result = l.Distinct(new ItemEqualityComparer());
            int total=0;
            foreach (StudentEntity s in result)
            {
                 int count=0;
            foreach(StudentEntity i in students)
            {
               
                string row1=s.RowKey;
                string row2=i.RowKey;
                if(row1.Equals(row2))
                count++;
               
                
            }
             MetricEntity m= new MetricEntity(s.RowKey,count);
                var insertop =TableOperation.Insert(m);
                await metricTable.ExecuteAsync(insertop);
                total=total+count;
            }
            MetricEntity m1= new MetricEntity("TOTAL",total);
                var insertop1 =TableOperation.Insert(m1);
                await metricTable.ExecuteAsync(insertop1);

         }
         private async  Task InitTable()
        {
             string conect ="DefaultEndpointsProtocol=https;AccountName=datc;AccountKey=8pbnojexAajFnr3kViZTZkP5Ww6jM4et0ykryBPii9/paddY7eKCRTnXcEckVdZD7AHJszBlPsNHTT0taJhfTw==;EndpointSuffix=core.windows.net";
            var acc=CloudStorageAccount.Parse(conect);
            tableClient=acc.CreateCloudTableClient();
            studentTable=tableClient.GetTableReference("studenti");
            await studentTable.CreateIfNotExistsAsync();
            metricTable=tableClient.GetTableReference("metrici");
            await metricTable.CreateIfNotExistsAsync();
        }
    }
}
