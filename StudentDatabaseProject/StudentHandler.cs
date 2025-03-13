
namespace StudentDatabaseProject
{
    internal class StudentHandler
    {
        StudentDbContext dbCtx = new StudentDbContext();
        MenuSystem menuSystem;

        internal StudentHandler()
        {
            menuSystem = new MenuSystem(this);
        }

        internal void Start()
        {
            menuSystem.StartStudentProgram();
        }

        internal void NewStudent(string firstName, string lastName, string city)
        {
            dbCtx.Add(new Student(firstName, lastName, city));
            dbCtx.SaveChanges();
        }

        internal Student? GetStudentWithStudentId(int studentID)
        {
            var student = dbCtx.Students.FirstOrDefault(s => s.StudentId == studentID);
            return student;
        }

        internal void UpdateFirstName(Student student, string? newFirstName)
        {
            if (String.IsNullOrEmpty(newFirstName))
                return;
            student.FirstName = newFirstName;
            dbCtx.SaveChanges();
        }

        internal void UpdateLastName(Student student, string? newLastName)
        {
            if (String.IsNullOrEmpty(newLastName))
                return;
            student.LastName = newLastName;
            dbCtx.SaveChanges();
        }

        internal void UpdateCity(Student student, string? newCity)
        {
            if (String.IsNullOrEmpty(newCity))
                return;
            student.City = newCity;
            dbCtx.SaveChanges();
        }

        internal List<Student> SearchStudentDb(string firstName, string lastName, string city)
        {
            var studentSearchResult = dbCtx.Students.Where(s => s.FirstName.Contains(firstName) &
                                                        s.LastName.Contains(lastName) & s.City.Contains(city));
            return studentSearchResult.ToList();
        }

        internal List<Student> GetAllStudentsInDb()
        {
            return dbCtx.Students.ToList();
        }
    }
}
