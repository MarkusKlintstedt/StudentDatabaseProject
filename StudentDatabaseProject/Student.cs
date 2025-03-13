namespace StudentDatabaseProject
{
    public class Student
    {
        public int StudentId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String City { get; set; } = "";
        public Student(String firstName, String lastName, String city)
        {
            FirstName = firstName;
            LastName = lastName;
            City = city;
        }
    }
}
