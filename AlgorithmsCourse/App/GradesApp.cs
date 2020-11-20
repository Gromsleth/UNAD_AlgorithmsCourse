using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmsCourse
{
    public class GradesApp
    {
        readonly Logic _Bl;

        public GradesApp()
        {
            _Bl = new Logic();
        }

        public void Ejecutar()
        {
            int ini = 1;
            string rta;
            int iAux;

            do
            {
                _Bl.MainMenu(false); //Método para mostrar menu principal

                rta = Console.ReadLine().Trim();
                iAux = _Bl.ReadValue(rta);

                switch (iAux)
                {
                    case 0: //Salir
                        ini = 0;
                        break;
                    case 1: //Ingresar Estudiante.
                        ini = _Bl.InsertStudent();
                        break;
                    case 2: //Consultar Promedio.
                        _Bl.GetSummary();
                        break;
                    case 3: //Listar Aprobados.
                        _Bl.Students(true);
                        break;
                    case 4: //Listar Reprobados.
                        _Bl.Students(false);
                        break;
                    case 5: //Listar todos.
                        _Bl.GetStudents();
                        break;
                    case 6: //Consultar estudiante por codigo.
                        ini = _Bl.GetStudent();
                        break;
                    case 7: //Consultar estudiantes por nombres.
                        ini = _Bl.GetStudentsByName();
                        break;
                    case 8: //Modificar nota.
                        ini = _Bl.ModifyGrade();
                        break;
                    case 9: //Eliminar estudiante.
                        ini = _Bl.RemoveStudent();
                        break;
                    case 10: //Modificar la nota parametro
                        ini = _Bl.ModifyParamGrade();
                        break;
                    case -1:
                        break;
                    default:
                        _Bl.InvalidOption();
                        break;
                }

            } while (ini == 1);

            _Bl.Exit();
        }

    }
}
