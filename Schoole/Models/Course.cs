namespace Schoole.Models
{
    public class Course
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public int TeacherId { get; set; }

        public Teacher teacher { get; set; }

    }
}
