using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
public interface IStudentRepo
{
    Task<List<StudentEntity>>  GetAllStudents();
    Task CreateNewStudent(StudentEntity student);
    Task<string> UpdateStudent( StudentEntity student);

     public  Task<string> DeleteStudent(string a);
    
    

    
}