using Howest_Movies_EE_DAL.DTO;
using Howest_Movies_EE_DAL.Models;
using System.Linq;

namespace Howest_Movies_EE_DAL.Repositories
{
    public interface IStudentRepository
    {
        IQueryable<StudentDetailDTO> GetAll();
        StudentDetailDTO GetById(int id);
        StudentBasicDTO GetBasicById(int id);
        StudentDetailDTO GetByMail(string mail);
        IQueryable<CursusDTO> GetCoursesByStudentId(int studentId);
        StudentDTO Create(StudentDTO studentDTO);
        StudentDTO Update(StudentDTO updatedStudentDTO);
        StudentDTO Delete(int id);
    }
}