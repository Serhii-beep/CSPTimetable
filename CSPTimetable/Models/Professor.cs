namespace CSPTimetable.Models
{
    public class Professor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Subject> Subjects { get; set; }
    }
}
