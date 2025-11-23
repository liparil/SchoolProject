namespace Schoole.Models
{
    public class Student
    {
        public int ID { get; set; }

        public string FullName { get; set; }

        public DateTime BirthDate { get; set; }

        public string NCode { get; set; }

        public Classroom? Classroom { get; set; }
        public int? ClassroomId { get; set; }

        public List<Grade> Grades { get; set; } = new();

       
    }
}
