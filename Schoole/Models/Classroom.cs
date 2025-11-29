namespace Schoole.Models
{
    public class Classroom
    {
        public Classroom()
        {
            Students = new List<Student>();
            Courses = new List<Course>();
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }

        public ICollection<Course> Courses { get; set; }

    }

}
