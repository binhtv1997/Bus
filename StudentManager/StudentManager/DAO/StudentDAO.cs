using StudentManager.DAO;
using StudentManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManager.DAO
{
    class StudentDAO
    {
        private DBConnection conn;

        public StudentDAO()
        {
            conn = new DBConnection();
        }

        private Student GetStudentFromDataRow(DataRow row)
        {
            Student student = new Student();

            student.ID = int.Parse(row["Id"].ToString());
            student.Name = row["Name"].ToString().Trim();
            student.Address = row["Address"].ToString().Trim();
            return student;
        }

        public bool Delete(int id)
        {
            string sql = " delete from Students where id = @id";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@id", SqlDbType.Int) { Value = id };

            try
            {
                conn.ExecuteUpdateQuery(sql, sqlParameters);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public List<Student> GetAll()
        {
            string query = string.Format("select * from Students");
            SqlParameter[] sqlParameters = new SqlParameter[0];
            DataTable dt = conn.ExecuteSelectQuery(query, sqlParameters);
            List<Student> list = new List<Student>();

            foreach (DataRow r in dt.Rows)
            {
                Student student = GetStudentFromDataRow(r);
                list.Add(student);
            }

            return list;
        }

        public Student SearchById(int _id)
        {
            string query = "select * from Students where Id = @Id";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", SqlDbType.Int);
            sqlParameters[0].Value = _id;

            DataTable dt = conn.ExecuteSelectQuery(query, sqlParameters);
            if (dt.Rows.Count > 0)
            {
                return GetStudentFromDataRow(dt.Rows[0]);
            }

            return null;
        }

        public bool Add(Student student)
        {
            string query = "INSERT INTO Students values(@Name,@Address)";
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = student.Name };
            sqlParameters[1] = new SqlParameter("@Address", SqlDbType.NVarChar) { Value = student.Address };

            try
            {
                conn.ExecuteInsertQuery(query, sqlParameters);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(Student student)
        {
            string query = "UPDATE Students SET name=@Name, address=@Address WHERE id=@Id";
            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = student.Name };
            sqlParameters[1] = new SqlParameter("@Address", SqlDbType.NVarChar) { Value = student.Address };
            sqlParameters[2] = new SqlParameter("@Id", SqlDbType.Int) { Value = student.ID };
            try
            {
                conn.ExecuteUpdateQuery(query, sqlParameters);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
