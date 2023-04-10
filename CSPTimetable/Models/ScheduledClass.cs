namespace CSPTimetable.Models
{
    public class ScheduledClass
    {
        public Subject Subject { get; set; }
        public Classroom Classroom { get; set; }
        public Professor Professor { get; set; }
        public Group Group { get; set; }
        public Timeslot Timeslot { get; set; }
    }
}
