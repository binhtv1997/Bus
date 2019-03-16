using StudentManager.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManager.DAO
{
    class StudentBLL
    {
        private StudentDAO dao;

        public StudentBLL()
        {
            dao = new StudentDAO();
        }

        public bool InsertStudent(Student s)
        {
            if (s.Name.Length > 0)
            {
                return dao.Add(s);
            }
            else { return false; }
        }
        public bool Delete(int id)
        {
            return id != null ? dao.Delete(id) : false;
        }
        public List<Student> GetStudents()
        {
            return dao.GetAll();
        }
        public bool Update(Student s)
        {
            if (s.Name.Length>0 && s.Address.Length>0)
            {
                dao.Update(s);
                return true;
            }
            return false;
        }
        public Student GetStudentByID(int s)
        {

            if (s < 0)
            {
                return null;
            }
            return dao.SearchById(s);
        }
    }
}
