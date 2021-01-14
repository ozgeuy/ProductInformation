using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using ProductInformation.Models;

namespace ProductInformation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SchoolContext c = new SchoolContext();
            //c.Database.Create();
            Display();
        }

        public void Display()
        {
            using(StudentInformationEntities _entity= new StudentInformationEntities())
            {
                List<Student> _studentList = new List<Student>();
                _studentList = _entity.Students.Select(x => new Student
                {
                    StudentId = x.StudentId,
                    Name = x.Name,
                    Class = x.Class,
                    TeacherId = x.TeacherId
                }).ToList();
                dataGridView1.DataSource = _studentList;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Student stu = new Student();
            stu.Name = textBox1.Text;
            stu.Class = textBox2.Text;
            bool result = SaveStudentDetails(stu);
            ShowStatus(result, "Save");
        }

        public bool SaveStudentDetails(Student Stu)
        {
            bool result = false;
            using(StudentInformationEntities _entity = new StudentInformationEntities())
            {
                _entity.Students.Add(Stu);
                _entity.SaveChanges();
                result = true;
            }
            return result;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) //Calling Datagridview cell click to Update and Delete  
        {
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows) // foreach datagridview selected rows values  
                {
                    label1.Text = row.Cells[0].Value.ToString();
                    textBox1.Text = row.Cells[1].Value.ToString();
                    textBox2.Text = row.Cells[2].Value.ToString();

                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Student stu = SetValues(Convert.ToInt32(label1.Text), textBox1.Text, textBox2.Text); // Binding values to StudentInformationModel  
            bool result = UpdateStudentDetails(stu); // calling UpdateStudentDetails Method  
            ShowStatus(result, "Update");
        }
        public bool UpdateStudentDetails(Student Stu) // UpdateStudentDetails method for update a existing Record  
        {
            bool result = false;
            using (StudentInformationEntities _entity = new StudentInformationEntities())
            {
                Student _student = _entity.Students.Where(x => x.StudentId == Stu.StudentId).Select(x => x).FirstOrDefault();
                _student.Name = Stu.Name;
                _student.Class = Stu.Class;
                _entity.SaveChanges();
                result = true;
            }
            return result;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Student stu = SetValues(Convert.ToInt32(label1.Text), textBox1.Text, textBox2.Text); // Binding values to StudentInformationModel  
            bool result = DeleteStudentDetails(stu); //Calling DeleteStudentDetails Method  
            ShowStatus(result, "Delete");
        }
        public bool DeleteStudentDetails(Student Stu) // DeleteStudentDetails method to delete record from table  
        {
            bool result = false;
            using (StudentInformationEntities _entity = new StudentInformationEntities())
            {
                Student _student = _entity.Students.Where(x => x.StudentId == Stu.StudentId).Select(x => x).FirstOrDefault();
                _entity.Students.Remove(_student);
                _entity.SaveChanges();
                result = true;
            }
            return result;
        }

        public Student SetValues(int StudentId, string Name, string Class) //Setvalues method for binding field values to StudentInformation Model class  
        {
            Student stu = new Student();
            stu.StudentId = StudentId;
            stu.Name = Name;
            stu.Class = Class;
            return stu;
        }

        public void ShowStatus(bool result, string Action) // validate the Operation Status and Show the Messages To User  
        {
            if (result)
            {
                if (Action.ToUpper() == "SAVE")
                {
                    MessageBox.Show("Saved Successfully!..", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (Action.ToUpper() == "UPDATE")
                {
                    MessageBox.Show("Updated Successfully!..", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Deleted Successfully!..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Something went wrong!. Please try again!..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            ClearFields();
            Display();
        }

        public void ClearFields() // Clear the fields after Insert or Update or Delete operation  
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

    }
}
