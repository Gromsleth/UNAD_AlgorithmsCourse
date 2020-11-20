using System.Collections.Generic;
using System;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace AlgorithmsCourse
{
    public class DataBase
    {
        /// <summary>
        /// Esta clase simula el acceso a base de datos.
        /// </summary>

        string path = string.Format("{0}\\DB.txt", Directory.GetCurrentDirectory());
        char[] delimiterChars = { '|' };
        readonly List<Student> _DataStudents;
        decimal _gradeToApprove;

        public DataBase()
        {
            _DataStudents = new List<Student>();
            _gradeToApprove = 3; //valor por defecto 
            LoadData();
        }

        public void InsertStudent(Student student)
        {
            _DataStudents.Add(student);
        }

        public List<Student> GetStudents()
        {
            return _DataStudents;
        }

        public void ModifyGradeToApprove(decimal grade)
        {
            _gradeToApprove = grade;
        }

        public decimal GetGradeToApprove()
        {
            return _gradeToApprove;
        }

        public void ModifyGrade(int studentCode, decimal newGrade)
        {
            foreach (var student in _DataStudents)
            {
                if (student.codStudent == studentCode)
                {
                    student.studentGrade = newGrade;
                    break;
                }
            }
        }

        public void RemoveStudent(int studentCode)
        {
            foreach (var student in _DataStudents)
            {
                if (student.codStudent == studentCode)
                {
                    _DataStudents.Remove(student);
                    break;
                }
            }
        }

        public void SaveData()
        {
            try
            {
                File.Delete(path);
                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    foreach (var student in _DataStudents)
                    {
                        writer.WriteLine(student.ForData());
                    }
                }
            }
            catch (Exception)
            {
                File.Delete(path);
            }
        }

        public void LoadData()
        {
            string line;
            string[] delimiteLine;
            try
            {
                if (File.Exists(path))
                    using (StreamReader reader = new StreamReader(path))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            delimiteLine = line.Split(delimiterChars);
                            _DataStudents.Add(new Student { codStudent = int.Parse(delimiteLine[0]), name = delimiteLine[1], studentGrade = decimal.Parse(delimiteLine[2]) });
                        }
                    }
            }
            catch (Exception)
            {
                File.Delete(path);
            }
        }

    }
}
