namespace StudentDatabaseProject
{
    internal class MenuSystem
    {
        StudentHandler handler;

        internal MenuSystem(StudentHandler sHandler)
        {
            handler = sHandler;
        }

        internal void StartStudentProgram()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Black;
            MainMenu();
        }

        private void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("\n       *Studentregistret*\n");
            Console.WriteLine(" 1 - Registrera ny student");
            Console.WriteLine(" 2 - Uppdatera information om student");
            Console.WriteLine(" 3 - Skriv ut alla studenter");
            Console.WriteLine(" 0 - Avsluta");
            Console.Write("\n Välj ett alternativ: ");
            HandleMenuChoice();
        }

        private void HandleMenuChoice()
        {
            switch (Console.ReadLine() ?? "")
            {
                case "1":
                    RegisterNewStudent();
                    break;
                case "2":
                    SearchStudentBeforeUpdateInDb();
                    break;
                case "3":
                    PrintAllStudents();
                    break;
                case "0":
                    Console.WriteLine("\n Programmet avslutas\n");
                    return;
                default:
                    Console.WriteLine(" Menyval ogiltigt. Försök igen");
                    break;
            }
            Console.Write("\n Tryck på valfri tangent för att fortsätta");
            Console.ReadKey();
            MainMenu();
        }

        private void RegisterNewStudent()
        {
            Console.Write("\n Ange förnamn: ");
            String firstName = GetUserInput();
            Console.Write("\n Ange efternamn: ");
            String lastName = GetUserInput();
            Console.Write("\n Ange hemort: ");
            String city = GetUserInput();
            handler.NewStudent(firstName, lastName, city);
            Console.WriteLine("\n Student registrerad i databasen");
        }

        private string GetUserInput()
        {
            while (true)
            {
                String input = Console.ReadLine() ?? "";
                if (input != "")
                    return input;
                else
                    Console.Write(" Ogiltig input. Försök igen: ");
            }
        }

        private void SearchStudentBeforeUpdateInDb()
        {
            Console.Write("\n Ange efternamn (del av namn räcker): ");
            String lastName = Console.ReadLine() ?? "";
            if (ManageStudentSearchBeforeUpdate("", lastName, ""))
                return;
            Console.Write("\n Ange förnamn (del av namn räcker): ");
            String firstName = Console.ReadLine() ?? "";
            if (ManageStudentSearchBeforeUpdate(firstName, lastName, ""))
                return;
            Console.Write("\n Ange hemort (del av ortnamn räcker): ");
            String city = Console.ReadLine() ?? "";
            if (ManageStudentSearchBeforeUpdate(firstName, lastName, city))
                return;
            FindStudentWithStudentId();
        }

        private bool ManageStudentSearchBeforeUpdate(String firstName, String lastName, String city)
        {
            List<Student> students = handler.SearchStudentDb(firstName, lastName, city);
            if (students.Count > 1)
            {
                PrintStudentList(students);
                return false;
            }
            else if (students.Count == 1)
            {
                UpdateStudent(students[0]);
            }
            else if (students.Count == 0)
            {
                Console.WriteLine(" Sökkriterierna gav ingen träff i databasen.");
            }
            return true;
        }

        private void PrintStudentList(List<Student> students)
        {
            foreach (Student student in students.OrderBy(s => s.LastName).ThenBy(s => s.FirstName).ThenBy(s => s.City))
            {
                PrintStudentEntry(student);
            }
        }

        private void FindStudentWithStudentId()
        {
            Console.Write("\n Ange Student-ID: ");
            if (int.TryParse(Console.ReadLine(), out int studentId))
            {
                Student? student = handler.GetStudentWithStudentId(studentId);
                if (student == null)
                {
                    Console.WriteLine(" Student-ID saknas i databasen"); ;
                }
                else
                {
                    UpdateStudent(student);
                }
            }
            else
            {
                Console.WriteLine(" Ogiltigt format för student-ID");
            }
        }

        private void UpdateStudent(Student student)
        {
            Console.WriteLine("\n Student i dabasen som uppdateras");
            PrintStudentEntry(student);
            Console.Write($"\n Nuvarande förnamn är {student.FirstName}. Ange nytt förnamn (eller tryck enter för att behålla): ");
            handler.UpdateFirstName(student, Console.ReadLine());
            Console.Write($"\n Nuvarande förnamn är {student.LastName}. Ange nytt efternamn (eller tryck enter för att behålla): ");
            handler.UpdateLastName(student, Console.ReadLine());
            Console.Write($"\n Nuvarande hemort är {student.City}. Ange ny hemort (eller tryck enter för att behålla): ");
            handler.UpdateCity(student, Console.ReadLine());
            Console.WriteLine("\n Sparad information om studenten i databasen:");
            PrintStudentEntry(student);
        }

        private void PrintStudentEntry(Student student)
        {
            Console.WriteLine($" * {student.FirstName} {student.LastName} från {student.City} (student-ID {student.StudentId})");
        }

        private void PrintAllStudents()
        {
            Console.Clear();
            Console.WriteLine("\n       *Registrerade studenter*\n");
            PrintStudentList(handler.GetAllStudentsInDb());
        }
    }
}
