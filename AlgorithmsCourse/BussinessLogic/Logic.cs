using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmsCourse
{
    public class Logic
    {
        readonly DataBase _Da;
        readonly UserInterface UInterface;
        bool ini;
        string rta;
        int iCodStudent;
        List<Student> FilteredList;

        public Logic()
        {
            _Da = new DataBase();
            UInterface = new UserInterface();
            FilteredList = new List<Student>();
        }

        #region Private
        private bool ValDuplicate(int code)
        {
            bool isDuplicate = false;

            foreach (var student in _Da.GetStudents())
            {
                if (student.codStudent == code)
                {
                    isDuplicate = true;
                    break;
                }
            }

            return isDuplicate;
        }

        private decimal CapturaNota(string auxString)
        {
            decimal auxDecimal;

            try
            {
                auxDecimal = Convert.ToDecimal(auxString);
            }
            catch (Exception)
            {
                UInterface.InvalidGrade();
                return -1; //Código de error
            }

            if (auxDecimal < 0 || auxDecimal > 5)
            {
                UInterface.InvalidGrade();
                return -2; //Código de error
            }
            return Math.Round(auxDecimal, 1); //redondear a una cifra decimal
        }

        private int GetStudentsCount()
        {
            return _Da.GetStudents().Count;
        }

        private int GetAprovedCount()
        {
            int iCount = 0;
            foreach (var student in _Da.GetStudents())
            {
                if (student.studentGrade >= _Da.GetGradeToApprove())
                    iCount++;
            }

            return iCount;
        }

        private List<Student> GetStudentsAproved()
        {
            FilteredList.Clear();
            foreach (var student in _Da.GetStudents())
            {
                if (student.studentGrade >= _Da.GetGradeToApprove())
                    FilteredList.Add(student);
            }

            return FilteredList;
        }

        private int GetReprobateCount()
        {
            int iCount = 0;
            foreach (var student in _Da.GetStudents())
            {
                if (student.studentGrade < _Da.GetGradeToApprove())
                    iCount++;
            }

            return iCount;
        }

        private List<Student> GetStudentsReproved()
        {
            FilteredList.Clear();
            foreach (var student in _Da.GetStudents())
            {
                if (student.studentGrade < _Da.GetGradeToApprove())
                    FilteredList.Add(student);
            }

            return FilteredList;
        }

        #endregion

        #region public
        public int ReadValue(string rta, int option = 1)
        {
            int iAux = -1;

            try
            {
                iAux = Convert.ToInt32(rta);
                if (iAux == -1 && option == 2)
                    UInterface.NegativeValues();
                else if (iAux == -1)
                    UInterface.InvalidOption();
            }
            catch (Exception)
            {
                UInterface.Error();
            }

            return iAux;
        }

        public int InsertStudent()
        {
            string sName;
            decimal dGrade;
            int iAux;

            do
            {
                ini = true;
                iCodStudent = 0;
                UInterface.IsertStudent(0);
                rta = Console.ReadLine().Trim();

                if (rta == "E" || rta == "e")
                    return 0;
                if (rta == "R" || rta == "r")
                    return 1;

                iAux = ReadValue(rta, 2);
                if (iAux < 0 && iAux != -1)
                    UInterface.NegativeValues();
                else if (iAux != -1)
                {
                    //Validar codigos de estudiante duplicados
                    if (ValDuplicate(iAux))
                        UInterface.DuplicateStudent();
                    else
                    {
                        iCodStudent = iAux;
                        ini = false;
                    }
                }
            } while (ini);

            //Capturar nombre del estudiante
            do
            {
                ini = true;
                UInterface.IsertStudent(1);
                sName = Console.ReadLine().Trim();

                if (rta == "E" || rta == "e")
                    return 0;
                if (rta == "R" || rta == "r")
                    return 1;

                if (sName.Length > 40)
                    UInterface.ErrorName(); 
                else
                    ini = false;

            } while (ini);

            //Capturar nota del estudiante
            do
            {
                ini = true;
                UInterface.IsertStudent(2);
                rta = Console.ReadLine().Trim();

                if (rta == "E" || rta == "e")
                    return 0;
                if (rta == "R" || rta == "r")
                    return 1;

                dGrade = CapturaNota(rta);
                if (dGrade >= 0)
                    ini = false;

            } while (ini);

            _Da.InsertStudent(new Student { codStudent = iCodStudent, name = sName, studentGrade = dGrade });
            UInterface.Student(_Da.GetStudents().FirstOrDefault(p => p.codStudent == iCodStudent).ToString());
            return 1;
        }

        public void GetSummary()
        {
            int iTotalStudents;
            int iApproved;
            int iReprobate;

            iTotalStudents = GetStudentsCount();

            if (iTotalStudents == 0)
            {
                UInterface.NoStudents();
                return;
            }

            iApproved = GetAprovedCount();
            iReprobate = GetReprobateCount();

            UInterface.GetSummary(iTotalStudents, iApproved, iReprobate);
        }

        public int ModifyGrade()
        {
            //Capturar el código del estudiante
            decimal dGrade;

            if (GetStudentsCount() == 0)
            {
                UInterface.NoStudents();
                return 1;
            }

            do
            {
                ini = true;
                iCodStudent = 0;
                UInterface.SelectStudent(_Da.GetStudents()
                      .OrderBy(s => s.codStudent)
                      .ToList());
                rta = Console.ReadLine().Trim();

                if (rta == "E" || rta == "e")
                    return 0;
                if (rta == "R" || rta == "r")
                    return 1;

                iCodStudent = ReadValue(rta, 2);
                if (iCodStudent < 0 && iCodStudent != -1)
                    UInterface.NegativeValues();
                else if (iCodStudent != -1)
                {
                    //Validar codigos de estudiante duplicados
                    if (ValDuplicate(iCodStudent))
                        ini = false;
                    else
                        UInterface.NonexistentStudent();
                }

            } while (ini);

            //Capturar nota del estudiante
            do
            {
                ini = true;
                UInterface.ModifyStudent(_Da.GetStudents().FirstOrDefault(p => p.codStudent == iCodStudent).ToString());
                rta = Console.ReadLine().Trim();

                if (rta == "E" || rta == "e")
                    return 0;
                if (rta == "R" || rta == "r")
                    return 1;

                dGrade = CapturaNota(rta);
                if (dGrade >= 0)
                    ini = false;

            } while (ini);

            //Modificar la nota del estudiante 
            _Da.ModifyGrade(iCodStudent, dGrade);
            UInterface.Student(_Da.GetStudents().FirstOrDefault(p => p.codStudent == iCodStudent).ToString());

            return 1;
        }

        public void Students(bool approved)
        {
            //Capturar el código del estudiante
            if (GetStudentsCount() == 0)
            {
                UInterface.NoStudents();
                return;
            }

            if (approved)
                UInterface.Students(GetStudentsAproved()
                        .OrderBy(s => s.codStudent)
                        .ToList(), approved);
            else
                UInterface.Students(GetStudentsReproved()
                        .OrderBy(s => s.codStudent)
                        .ToList(), approved);
        }

        public void GetStudents()
        {
            //Capturar el código del estudiante
            if (GetStudentsCount() == 0)
            {
                UInterface.NoStudents();
                return;
            }

            UInterface.GetStudents(_Da.GetStudents()
                      .OrderBy(s => s.codStudent)
                      .ToList());

        }

        public int GetStudent()
        {
            if (GetStudentsCount() == 0)
            {
                UInterface.NoStudents();
                return 1;
            }

            do
            {
                ini = true;
                UInterface.IsertStudent(0);
                rta = Console.ReadLine().Trim();

                if (rta == "E" || rta == "e")
                    return 0;
                if (rta == "R" || rta == "r")
                    return 1;

                iCodStudent = ReadValue(rta, 2);
                if (iCodStudent < 0 && iCodStudent != -1)
                    UInterface.NegativeValues();
                else if (iCodStudent != -1)
                {
                    //Validar codigos de estudiante duplicados
                    if (ValDuplicate(iCodStudent))
                    {
                        ini = false;
                        UInterface.GetStudent(_Da.GetStudents().FirstOrDefault(p => p.codStudent == iCodStudent).ToString());
                    }
                    else
                        UInterface.NonexistentStudent();
                }

            } while (ini);

            return 1;
        }

        public int GetStudentsByName()
        {
            if (GetStudentsCount() == 0)
            {
                UInterface.NoStudents();
                return 1;
            }

            do
            {
                FilteredList.Clear();
                ini = true;
                UInterface.FilterName();
                rta = Console.ReadLine().Trim();

                if (rta == "E" || rta == "e")
                    return 0;
                if (rta == "R" || rta == "r")
                    return 1;

                if (rta.Length > 1)
                {
                    foreach (var student in _Da.GetStudents()
                          .OrderBy(s => s.codStudent)
                          .ToList())
                    {
                        if (student.name.ToUpper().Contains(rta.ToUpper()))
                            FilteredList.Add(student);
                    }

                    if (FilteredList.Count > 0)
                    {
                        ini = false;
                        UInterface.StudentsFiltered(FilteredList);
                    }
                    else
                        UInterface.NoStudentsFilter();
                }
                else
                    UInterface.InvalidFilter();

            } while (ini);

            return 1;
        }

        public int RemoveStudent()
        {
            //Capturar el código del estudiante
            if (GetStudentsCount() == 0)
            {
                UInterface.NoStudents();
                return 1;
            }

            do
            {
                ini = true;
                iCodStudent = 0;
                UInterface.SelectStudent(_Da.GetStudents()
                      .OrderBy(s => s.codStudent)
                      .ToList(), true);
                rta = Console.ReadLine().Trim();

                if (rta == "E" || rta == "e")
                    return 0;
                if (rta == "R" || rta == "r")
                    return 1;

                iCodStudent = ReadValue(rta, 2);
                if (iCodStudent < 0 && iCodStudent != -1)
                    UInterface.NegativeValues();
                else if (iCodStudent != -1)
                {
                    //Validar codigos de estudiante duplicados
                    if (ValDuplicate(iCodStudent))
                        ini = false;
                    else
                        UInterface.NonexistentStudent();
                }

            } while (ini);

            //Confirmar la eliminación del estudiante
            do
            {
                ini = true;
                UInterface.ModifyStudent(_Da.GetStudents().FirstOrDefault(p => p.codStudent == iCodStudent).ToString(), true);
                rta = Console.ReadLine().Trim();

                if (rta == "E" || rta == "e")
                    return 0;
                if (rta == "R" || rta == "r")
                    return 1;

                if (rta == "y" || rta == "Y")
                    ini = false;
                else
                    UInterface.InvalidOption();

            } while (ini);

            //Modificar la nota del estudiante 
            _Da.RemoveStudent(iCodStudent);
            UInterface.SuccesRemove(ValDuplicate(iCodStudent));

            return 1;
        }

        public int ModifyParamGrade()
        {
            //Capturar el código del estudiante
            decimal dGrade;

            //Capturar nota con la que se aprueba
            do
            {
                ini = true;
                UInterface.ModifyParamGrade(_Da.GetGradeToApprove());
                rta = Console.ReadLine().Trim();

                if (rta == "E" || rta == "e")
                    return 0;
                if (rta == "R" || rta == "r")
                    return 1;

                dGrade = CapturaNota(rta);
                if (dGrade >= 0)
                    ini = false;

            } while (ini);

            //Modificar la nota del estudiante 
            _Da.ModifyGradeToApprove(dGrade);
            UInterface.ParamGrade(_Da.GetGradeToApprove());

            return 1;
        }

        //Métodos para la interfaz
        public void MainMenu(bool readOption = true)
        {
            UInterface.MainMenu(readOption);
        }

        public void InvalidOption()
        {
            UInterface.InvalidOption();
        }

        public void Exit()
        {
            _Da.SaveData();
            UInterface.Exit();
        }
        #endregion

    }
}
