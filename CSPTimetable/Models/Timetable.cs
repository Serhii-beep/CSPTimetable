namespace CSPTimetable.Models
{
    public class Timetable
    {
        public List<ScheduledClass> ScheduledClasses { get; set; }

        public Timetable()
        {
            ScheduledClasses = new List<ScheduledClass>();
        }
    }
}
