using AutoMapper;
using AutoMapper.QueryableExtensions;
using Howest_Movies_EE_DAL.DTO;
using Howest_Movies_EE_DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Howest_Movies_EE_DAL.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolContext db;
        private readonly IMapper StudentMapper;

        public StudentRepository(SchoolContext schoolContext, IMapper mapper)
        {
            db = schoolContext;
            StudentMapper = mapper;
        }

        #region GetById
        public StudentDetailDTO GetById(int id)
        {
            return db.Studenten
                 .Where(s => s.Studnr == id)
                 .ProjectTo<StudentDetailDTO>(StudentMapper.ConfigurationProvider)
                 .SingleOrDefault();
        }
        #endregion

        #region GetBasicById
        public StudentBasicDTO GetBasicById(int id)
        {
            return db.Studenten
                 .Where(s => s.Studnr == id)
                 .ProjectTo<StudentBasicDTO>(StudentMapper.ConfigurationProvider)
                 .SingleOrDefault();
        }
        #endregion

        #region GetByMail
        public StudentDetailDTO GetByMail(string email)
        {
            return db.Studenten
                 .Where(s => s.Email == email)
                 .ProjectTo<StudentDetailDTO>(StudentMapper.ConfigurationProvider)
                 .SingleOrDefault();
        }
        #endregion

        #region GetCoursesByStudentId
        public IQueryable<CursusDTO> GetCoursesByStudentId(int studentId)
        {
            return db.Cursussen
                     .FromSqlRaw($"Select c.* " +
                                 $"From  cursussen c " +
                                 $"Inner join studenten_cursussen sc on c.cursusnr = sc.cursusnr " +
                                 $"Inner join studenten s on sc.studnr = s.studnr " +
                                 $"Where s.studnr = {studentId}")
                    .ProjectTo<CursusDTO>(StudentMapper.ConfigurationProvider);
        }
        #endregion

        #region GetAll
        public IQueryable<StudentDetailDTO> GetAll()
        {
            return db.Studenten
                 .ProjectTo<StudentDetailDTO>(StudentMapper.ConfigurationProvider)
                 .OrderBy(s => s.Geslacht);

        }
        #endregion

        #region Create
        public StudentDTO Create(StudentDTO studentDTO)
        {
            Studenten newStudent = StudentMapper.Map<Studenten>(studentDTO);
            db.Studenten.Add(newStudent);
            Save();

            studentDTO.Studnr = newStudent.Studnr;
            return studentDTO;
        }
        #endregion

        #region Update
        public StudentDTO Update(StudentDTO updatedStudentDTO)
        {
            Studenten student = db.Studenten
                                  .SingleOrDefault(s => s.Studnr == updatedStudentDTO.Studnr);

            if (student != null)
            {
                StudentMapper.Map(updatedStudentDTO, student);
                Save();
            }

            return updatedStudentDTO;
        }
        #endregion

        #region Delete
        public StudentDTO Delete(int id)
        {
            Studenten student = db.Studenten
                                  .SingleOrDefault(s => s.Studnr == id);

            if (student != null)
            {
                db.Studenten.Remove(student);
                Save();
            }

            return StudentMapper.Map<StudentDTO>(student);
        }
        #endregion

        #region Save
        private void Save()
        {
            db.SaveChanges();
        }
        #endregion
    }
}
