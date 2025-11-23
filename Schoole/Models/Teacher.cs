namespace Schoole.Models
{
    public class Teacher
    {
        public int ID { get; set; }

        public string FullName { get; set; }

        public string NCode { get; set; }

        public string Expertise { get; set; }

        public List<Course> Courses { get; set; } = new();
    }
}
