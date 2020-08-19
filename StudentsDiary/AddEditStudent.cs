using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace StudentsDiary
{
    public partial class AddEditStudent : Form
    {
      

        private string _filePath =
            Path.Combine(Environment.CurrentDirectory, "students.txt");
        // $@"{Environment.CurrentDirectory}\students.txt";
        private int _studentId;
        private Student _student;

        private FileHelper<List<Student>> _fileHelper =
        new FileHelper<List<Student>>(Program.FilePath);
        public AddEditStudent(int id = 0)
        {
            InitializeComponent();
            _studentId = id;
            GetStudentData();

            tbFirstName.Select();
        }



        private void btnCancel_Click(object sender, EventArgs e)

        {
            Close();

        }
        private  void btnAccept_Click(object sender, EventArgs e)
        {
            var students = _fileHelper.DeserializeFromFile();

            if (_studentId != 0)
                students.RemoveAll(x => x.Id == _studentId);

            else
                AssignIdToNewStudent(students);

            AddNewStudentsToList(students);

            _fileHelper.SerializeToFile(students);

                    
            Close();
        }
     
        private void AddNewStudentsToList(List<Student> students)
        {
            var student = new Student
            {
                Id = _studentId,
                FirstName = tbFirstName.Text,
                LastName = tbSurname.Text,
                Comments = rtbComments.Text,
                Math = tbMath.Text,
                PolishLang = tbPolishLanguage.Text,
                Physics = tbPhisic.Text,
                ForeignLang = tbForeign.Text,
                Technology = tbTechnology.Text
            };
            students.Add(student);

        }
        private void AssignIdToNewStudent(List<Student> students)
        {
            var studentWithHigestId = students
                   .OrderByDescending(x => x.Id).FirstOrDefault();

            _studentId = studentWithHigestId == null ?
                    1 : studentWithHigestId.Id + 1;
        }
        private void GetStudentData()
        {
            if (_studentId != 0)
            {

                Text = "Edytowanie danych ucznia";
                var students = _fileHelper.DeserializeFromFile();
                _student = students.FirstOrDefault(x => x.Id == _studentId);

                if (_student == null)
                    throw new Exception("Brak użytkownika o podanym identyfikatorze");
                FillTextBoxes();


            }
        }
        private void FillTextBoxes()
        {
            tbId.Text = _student.Id.ToString();
            tbFirstName.Text = _student.FirstName;
            tbSurname.Text = _student.LastName;
            tbMath.Text = _student.Math;
            tbPhisic.Text = _student.Physics;
            tbTechnology.Text = _student.Technology;
            tbPolishLanguage.Text = _student.PolishLang;
            tbForeign.Text = _student.ForeignLang;
            rtbComments.Text = _student.Comments;
        }

    }




}
