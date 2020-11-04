using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Models;

namespace Tema4
{
    public class StudentRepo : IStudentRepo
    {
        private CloudTableClient tableClient;
        private CloudTable studentTable;
        public StudentRepo()
        {
            Task.Run(async () => {await InitTable() ;})
            .GetAwaiter()
            .GetResult();
        }

        public async Task CreateNewStudent(StudentEntity student)
        {
           var insertop =TableOperation.Insert(student);
            await studentTable.ExecuteAsync(insertop);
        }

        public async Task<string> DeleteStudent(string a)
        {
           
     TableOperation insertO = TableOperation.Retrieve(student.PartitionKey,student.RowKey);
            TableResult query = await studentTable.ExecuteAsync(insertO);
            
    
     object a=query.Result;
            if(a!=null)
            {
                 TableOperation delete = TableOperation.Delete(student);  
                    await  studentTable.ExecuteAsync(delete);
             
             return "Studentul a fost sters";
            }
            else
            return "Studentul nu exista";

            var students = new List<StudentEntity>();
            TableQuery<StudentEntity> query1=new TableQuery<StudentEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey",QueryComparisons.Equal,a));
             TableQuerySegment<StudentEntity> resultSegment=await studentTable.ExecuteQuerySegmentedAsync(query1,null);
              students.AddRange(resultSegment.Results);
              object v=students.Count;
              if(students.Count!=0){
               TableOperation delete = TableOperation.Delete(students[0]);
                    await  studentTable.ExecuteAsync(delete);
                    return "Studentul a fost sters";
              }
              else return "Studentul nu exista";

        }

        public async Task<List<StudentEntity>> GetAllStudents()
        {
            var students = new List<StudentEntity>();
            TableQuery<StudentEntity> query=new TableQuery<StudentEntity>();
            TableContinuationToken token=null;
            do{
                TableQuerySegment<StudentEntity> resultSegment=await studentTable.ExecuteQuerySegmentedAsync(query,token);
                students.AddRange(resultSegment.Results);
            }while(token!=null);
            return students;

        }

        public async Task<string> UpdateStudent( StudentEntity student)
        {
            

            TableOperation insertO = TableOperation.Retrieve(student.PartitionKey,student.RowKey);
            TableResult query = await studentTable.ExecuteAsync(insertO);
            
    
     object a=query.Result;
            if(a!=null)
            {
             TableOperation insertOp = TableOperation.InsertOrReplace(student);
             await  studentTable.ExecuteAsync(insertOp);
             return "Update cu succes ";
            }
            else
            return "Studentul introdus nu exista";
              
              
              
        }

        private async  Task InitTable()
        {
            string conect ="DefaultEndpointsProtocol=https;AccountName=datc;AccountKey=8pbnojexAajFnr3kViZTZkP5Ww6jM4et0ykryBPii9/paddY7eKCRTnXcEckVdZD7AHJszBlPsNHTT0taJhfTw==;EndpointSuffix=core.windows.net";
            var acc=CloudStorageAccount.Parse(conect);
            tableClient=acc.CreateCloudTableClient();
            studentTable=tableClient.GetTableReference("studenti");
            await studentTable.CreateIfNotExistsAsync();
        }
    }
}