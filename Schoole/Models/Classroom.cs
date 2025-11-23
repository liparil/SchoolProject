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

        public List<Student> Students { get; set; }

        public List<Course> Courses { get; set; }

    }

}
