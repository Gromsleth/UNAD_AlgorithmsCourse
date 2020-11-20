using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmsCourse
{
    public class UserInterface
    {
        readonly List<string> _list;

        public UserInterface()
        {
            _list = new List<string>();
        }

        void DrawInterface(bool readOption = true)
        {
            Console.Clear();
            Console.WriteLine("\n*****    Problema 4: Curso de Algoritmos    *****\n \n");
            foreach (var item in _list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(" \n");
            _list.Clear();
            if (readOption)
                Console.ReadKey();
        }

        public void MainMenu(bool readOption = true)
        {
            _list.Add(" Menú principal. ");
            _list.Add("\n Seleccione la opción: ");
            _list.Add("\n     1. Ingresar Estudiante. ");
            _list.Add("\n     2. Consultar promedio. ");
            _list.Add("\n     3. Lista de estudiantes aprobados. ");
            _list.Add("\n     4. Lista de estudiantes reprobados. ");
            _list.Add("\n     5. Lista todos los estudiantes. ");
            _list.Add("\n     6. Consultar estudiante por código. ");
            _list.Add("\n     7. Filtrar estudiantes por nombre. ");
            _list.Add("\n     8. Modificar nota de estudiante. ");
            _list.Add("\n     9. Eliminar registro de estudiante. ");
            _list.Add("\n     10. Modificar la nota mínima para aprobar el curso. ");
            _list.Add("\n     0. Guardar y Salir. \n");
            DrawInterface(readOption);
        }

        public void NegativeValues()
        {
            _list.Add(" Por favor ingrese valores que no sean negativos ");
            _list.Add(" presione cualquier tecla para continuar. \n");
            DrawInterface();
        }

        public void InvalidOption()
        {
            _list.Add(" Por favor ingrese una opción válida ");
            _list.Add(" presione cualquier tecla para continuar. \n");
            DrawInterface();
        }

        public void Error()
        {
            _list.Add(" Por favor ingrese un valor entero valido ");
            _list.Add(" presione cualquier tecla para continuar. \n");
            DrawInterface();
        }

        public void Exit()
        {
            _list.Add(" Fin del programa. \n");
            DrawInterface();
        }

        public void IsertStudent(int option)
        {
            switch (option)
            {
                case 0:
                    _list.Add(" Por favor ingrese el código del estudiante ");
                    _list.Add(" o presione 'e' para salir 'r' para volver  ");
                    break;
                case 1:
                    _list.Add(" Por favor ingrese el nombre del estudiante ");
                    _list.Add(" o presione 'e' para salir 'r' para volver  ");
                    break;
                case 2:
                    _list.Add(" Por favor ingrese la calificación (Utilice la coma ',' para los decimales) ");
                    _list.Add(" o presione 'e' para salir 'r' para volver  ");
                    break;
                default:
                    break;
            }
            DrawInterface(false);
        }

        public void Student(string student)
        {
            _list.Add(" Estudiante ingresado: \n ");
            _list.Add(student);
            _list.Add("\n presione cualquier tecla para continuar. \n");
            DrawInterface();
        }

        public void InvalidGrade()
        {
            _list.Add(" Por favor ingrese una Nota válida (entre 0 y 5) ");
            _list.Add(" presione cualquier tecla para continuar. \n");
            DrawInterface();
        }

        public void ErrorName()
        {
            _list.Add(" El nombre no puede tener mas de 40 carácteres ");
            _list.Add(" Puede ingresar solo el nombre o el apellido. \n");
            _list.Add(" presione cualquier tecla para continuar. \n");
            DrawInterface();
        }

        public void FilterName()
        {
            _list.Add(" Ingrese un nombre para filtrar ");
            _list.Add(" o presione 'e' para salir 'r' para volver ");
            DrawInterface(false);
        }

        public void NoStudents()
        {
            _list.Add(" No hay estudiantes ingresados. ");
            _list.Add(" presione cualquier tecla para continuar. \n");
            DrawInterface();
        }

        public void NoStudentsFilter()
        {
            _list.Add(" No hay estudiantes que cumplan con el filtro ingresado. ");
            _list.Add(" presione cualquier tecla para continuar. \n");
            DrawInterface();
        }

        public void InvalidFilter()
        {
            _list.Add(" Por favor ingrese un filtro de al menos dos carácteres. ");
            _list.Add(" presione cualquier tecla para continuar. \n");
            DrawInterface();
        }

        public void GetSummary(int TotalStudents, int iApproved, int ireprobate)
        {
            _list.Add(string.Format(" Total de estudiantes: {0}. \n", TotalStudents));
            _list.Add(string.Format(" Estudiantes que aprobaron: {0}. ", iApproved));
            _list.Add(string.Format(" Estudiantes que reprobaron: {0}. ", ireprobate));
            _list.Add(" presione cualquier tecla para continuar. \n");
            DrawInterface();
        }

        public void DuplicateStudent()
        {
            _list.Add(" Ya existe un estudiante con el código ingresado. ");
            _list.Add(" presione cualquier tecla para continuar. \n");
            DrawInterface();
        }

        public void NonexistentStudent()
        {
            _list.Add(" No existe un estudiante con el código ingresado. ");
            _list.Add(" presione cualquier tecla para continuar. \n");
            DrawInterface();
        }

        public void SelectStudent(List<Student> students, bool isDelete = false)
        {
            _list.Add(" Lista de Estudiantes: \n ");
            _list.Add("\n Código \t\t Calificación \t\t Nombre \n\n ");
            foreach (var item in students)
            {
                _list.Add(item.ForList());
            }
            if (isDelete)
                _list.Add("\n Por favor ingrese el código del estudiante que se va a eliminar ");
            else
                _list.Add("\n Por favor ingrese el código del estudiante que se va a modificar ");
            _list.Add(" o presione 'e' para salir 'r' para volver ");
            DrawInterface(false);
        }

        public void GetStudents(List<Student> students)
        {
            _list.Add(" Lista de Estudiantes: \n ");
            _list.Add("\n Código \t\t Calificación \t\t Nombre \n\n ");
            foreach (var item in students)
            {
                _list.Add(item.ForList());
            }
            _list.Add("\n presione cualquier tecla para continuar. \n");
            DrawInterface();
        }

        public void ModifyStudent(string student, bool isDelete = false)
        {
            _list.Add(" Estudiante ingresado: \n ");
            _list.Add(student);
            if (isDelete)
                _list.Add("\n Para confirmar por favor presione la tecla 'y' ");
            else
                _list.Add("\n Por favor ingrese la nueva calificación (Utilice la coma ',' para los decimales) ");
            _list.Add(" o presione 'e' para salir 'r' para volver al menú principal ");
            DrawInterface(false);
        }

        public void GetStudent(string student)
        {
            _list.Add(" Estudiante: \n ");
            _list.Add(student);
            _list.Add("\n presione cualquier tecla para continuar. \n");
            DrawInterface();
        }

        public void SuccesRemove(bool isError)
        {
            if (isError)
                _list.Add(" No se logró eliminar el registro ");
            else
                _list.Add(" El registro se eliminó correctamente ");
            _list.Add(" presione cualquier tecla para continuar. \n");
            DrawInterface();
        }

        public void ModifyParamGrade(decimal gradeParam)
        {
            _list.Add(" Por favor ingrese la calificación mínima para aprobar el curso (Utilice la coma ',' para los decimales) ");
            _list.Add(" o presione 'e' para salir 'r' para volver  ");
            _list.Add(String.Format("\n Calificación mínima para aprobar: {0} \n", gradeParam));
            DrawInterface(false);
        }

        public void ParamGrade(decimal gradeParam)
        {
            _list.Add(" La calificación mínima para aprobar el curso se modificó correctamente \n");
            _list.Add(String.Format("\n Calificación mínima para aprobar: {0} \n", gradeParam));
            _list.Add("\n presione cualquier tecla para continuar. \n");
            DrawInterface();
        }

        public void Students(List<Student> students, bool Aprroved)
        {
            if (Aprroved)
                _list.Add(" Lista de Estudiantes aprobados: \n ");
            else
                _list.Add(" Lista de Estudiantes reprobados: \n ");
            _list.Add("\n Código \t\t Calificación \t\t Nombre \n\n ");
            foreach (var item in students)
            {
                _list.Add(item.ForList());
            }
            _list.Add("\n presione cualquier tecla para continuar. \n");
            DrawInterface();
        }

        public void StudentsFiltered(List<Student> students)
        {
            _list.Add(" Lista de Estudiantes que cumplen el filtro: \n ");
            _list.Add("\n Código \t\t Calificación \t\t Nombre \n\n ");
            foreach (var item in students)
            {
                _list.Add(item.ForList());
            }
            _list.Add("\n presione cualquier tecla para continuar. \n");
            DrawInterface();
        }

    }
}
