using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmsCourse
{
    public class Student
    {
        public int codStudent { get; set; }

        public string name { get; set; }

        public decimal studentGrade { get; set; }

        public override string ToString()
        {
            return String.Format(" Estudiante: {0} \n Código: {1} \n Calificación: {2} ", name, codStudent, studentGrade);
        }

        public string ForList()
        {
            return String.Format(" {0} \t\t\t {1} \t\t\t\t {2} \n", codStudent, studentGrade, name);
        }

        public string ForData()
        {
            return String.Format("{0}|{1}|{2}", codStudent, name, studentGrade);
        }
    }
}
