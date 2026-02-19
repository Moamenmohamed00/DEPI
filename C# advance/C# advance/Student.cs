using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace C__advance
{
    internal class Student
    {
        private string[] students = new string[5] { "John", "Jane", "Doe", "Smith", "Emily" };
        private Dictionary<string, string> studentGrades = new Dictionary<string, string>();
        public string name { get; set; }//مش عارف اخليه يحفظ في مكان معين
        public void setstudent(int i, string name)
        {
            students[i] = name;
        }
        public string getstudent(int i)
        {
            return students[i];
        }
        public string this[int i]// i can search in array by index
        {
            get { return students[i]; }
            set { students[i] = value; }
        }
        public string this[string name]// i can search in array by name
        {
            get { return students[Array.IndexOf(students, name)]; }  //  get { return students.FirstOrDefault(s => s == name); }
            set
            {
                int index = Array.IndexOf(students, name);
                if (index != -1)
                {
                    students[index] = value;
                }
            }
        }
        public string this[string name, string grade]// i can search in array by name and grade
        {
            get
            {
                if (studentGrades.TryGetValue(name, out string gradeValue))
                {
                    return gradeValue;
                }
                return null;
            }
            set
            {
                studentGrades[name] = value;
            }
        }
        //public string this[string key]
        //{
        //    get { return studentGrades[key]; }
        //    set { studentGrades[key] = value; }
        //}
    }
}
