using StudentManager.DAO;
using StudentManager.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManager
{
    public partial class Form1 : Form
    {
        private StudentBLL bll;
        public Form1()
        {
            InitializeComponent();
            bll = new StudentBLL();
            loadData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void loadData()
        {
            List<Student> list = new List<Student>();
            list = bll.GetStudents();
            TblView.DataSource = list;
        }
        private void Load_Click(object sender, EventArgs e)
        {
            List<Student> list = new List<Student>();
            list = bll.GetStudents();
            TblView.DataSource = list;
        }

        private void Insert_Click(object sender, EventArgs e)
        {
            try
            {
                Student s = new Student { ID = 0, Name = txtName.Text, Address = txtAddress.Text };
          bool check =      bll.InsertStudent(s);
                loadData();
                MessageBox.Show(check.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void Update_Click(object sender, EventArgs e)
        {
            if (txtAddress.Text.Length > 0 && txtName.Text.Length > 0)
            {
                Student s = new Student { ID = int.Parse(txtID.Text.Trim()), Name = txtName.Text, Address = txtAddress.Text };
                bool check = bll.Update(s);
                MessageBox.Show(check.ToString()+txtID.Text);

                loadData();
            }
        }

        private void TblView_Click(object sender, EventArgs e)
        {
            if (TblView.SelectedRows.Count > 0)
            {
                int id = (int)TblView.SelectedRows[0].Cells[0].Value;
                var student = bll.GetStudentByID(id);
                if (student != null)
                {
                    txtID.Text = student.ID.ToString();
                    txtName.Text = student.Name.ToString();
                    txtAddress.Text = student.Address.ToString();
                }
            }
        }

        private void Search_Click(object sender, EventArgs e)
        {
            if (txtID.Text.Length > 0)
            {
                var student = bll.GetStudentByID(int.Parse(txtID.Text));
                if (student != null)
                {
                    txtID.Text = student.ID.ToString();
                    txtName.Text = student.Name.ToString();
                    txtAddress.Text = student.Address.ToString();
                }
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (txtID.Text.Length > 0)
            {
                bool check = bll.Delete(int.Parse(txtID.Text));
                MessageBox.Show(check.ToString());

                loadData();
            }
            else { MessageBox.Show("Error"); }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
