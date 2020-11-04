using Microsoft.WindowsAzure.Storage.Table;
namespace Models
{
    public  class  StudentEntity : TableEntity
    {
        public  StudentEntity(string universitate,string CNP)
        {
            this.PartitionKey=CNP;
            this.RowKey=universitate;

        }
        
         public  StudentEntity(){}
       
        public string nume{get;set;}
        public string prenume{get;set;}
        public string facultate{get;set;}
        public string email{get;set;}
        public string telefon{get;set;}
        public int an{get;set;}
        




    }
}