using CSPTimetable.Models;
using System.Runtime.InteropServices;
using System.Text;

namespace CSPTimetable
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var subjects = new List<Subject>
            {
                new Subject { Id = 1, Name = "Інформаційні технології менеджменту", IsLecture = true },
                new Subject { Id = 2, Name = "Вибрані розділи трудового права", IsLecture = true },
                new Subject { Id = 3, Name = "Основи підприємницької діяльності", IsLecture = true },
                new Subject { Id = 4, Name = "Композиційна семантика SQL-подібних мов", IsLecture = true },
                new Subject { Id = 5, Name = "Розробка бізнес-аналітичних систем", IsLecture = true },
                new Subject { Id = 6, Name = "Інтелектуальні системи", IsLecture = true },
                new Subject { Id = 7, Name = "Інтелектуальні системи", IsLecture = false },
                new Subject { Id = 8, Name = "Коректність програм та логіки програмування", IsLecture = false },
                new Subject { Id = 9, Name = "Основи Data Mining", IsLecture = true },
                new Subject { Id = 10, Name = "Методи специфікації програм", IsLecture = true }
            };

            var classrooms = new List<Classroom>
            {
                new Classroom { Id = 1, Name = "306", Capacity = 100 },
                new Classroom { Id = 2, Name = "43", Capacity = 70 },
                new Classroom { Id = 3, Name = "221", Capacity = 40 },
                new Classroom { Id = 4, Name = "01", Capacity = 100 },
                new Classroom { Id = 5, Name = "203", Capacity = 32 },
                new Classroom { Id = 6, Name = "202", Capacity = 32 }
            };

            var professors = new List<Professor>
            {
                new Professor { Id = 1, Name = "Вергунова І.М.", Subjects = new List<Subject> { subjects[0] } },
                new Professor { Id = 2, Name = "Богуславський О.В.", Subjects = new List<Subject> { subjects[1] } },
                new Professor { Id = 3, Name = "Панченко Т.В.", Subjects = new List<Subject> { subjects[4] } },
                new Professor { Id = 4, Name = "Богдан І.А.", Subjects = new List<Subject> { subjects[2] } },
                new Professor { Id = 5, Name = "Глибовець М.М.", Subjects = new List<Subject> { subjects[5] } },
                new Professor { Id = 6, Name = "Ткаченко О.М.", Subjects = new List<Subject> { subjects[7] } },
                new Professor { Id = 7, Name = "Федорус О.М.", Subjects = new List<Subject> { subjects[6] } },
                new Professor { Id = 8, Name = "Криволап А.В.", Subjects = new List<Subject> { subjects[8] } },
                new Professor { Id = 9, Name = "Шишацька О.В.", Subjects = new List<Subject> { subjects[9] } },
                new Professor { Id = 10, Name = "Нікітченко М.С.", Subjects = new List<Subject> { subjects[3] } },
            };

            var timeslots = new List<Timeslot>
            {
                new Timeslot { Id = 1, TimeSlot = "Mon 09:00-10:30" },
                new Timeslot { Id = 2, TimeSlot = "Mon 10:40-12:10" },
                new Timeslot { Id = 3, TimeSlot = "Mon 12:20-13:50" },
                new Timeslot { Id = 4, TimeSlot = "Tue 09:00-10:30" },
                new Timeslot { Id = 5, TimeSlot = "Tue 10:40-12:10" },
                new Timeslot { Id = 6, TimeSlot = "Tue 12:20-13:50" }
            };


            var groups = new List<Group>
            {
                new Group { Id = 1, Name = "ТТП-41", NumberOfStudents = 27, Subjects = subjects },
                new Group { Id = 2, Name = "ТТП-42", NumberOfStudents = 30, Subjects = subjects }
            };

            CSPSolver solver = new CSPSolver(subjects, classrooms, professors, groups, timeslots);
            Timetable timetable = solver.GenerateTimetable();

            Console.WriteLine("Generated timetable:");
            Console.WriteLine(new string('-', 134));
            Console.WriteLine("| {0, -10} | {1, -40} | {2, -23} | {3, -15} | {4, -10} | {5, -17} |", "Group", "Subject", "Professor", "Classroom", "Capacity", "Timeslot");
            Console.WriteLine(new string('-', 134));

            foreach(var scheduledClass in timetable.ScheduledClasses)
            {
                Console.WriteLine("| {0, -10} | {1, -40} | {2, -23} | {3, -15} | {4, -10} | {5, -17} |",
                    scheduledClass.Group.Name,
                    scheduledClass.Subject.Name,
                    scheduledClass.Professor.Name,
                    scheduledClass.Classroom.Name,
                    scheduledClass.Classroom.Capacity,
                    scheduledClass.Timeslot.TimeSlot);
            }

            Console.WriteLine(new string('-', 134));
        }
    }
}