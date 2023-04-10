using CSPTimetable.Models;

namespace CSPTimetable
{
    public class CSPSolver
    {
        public List<Subject> Subjects { get; set; }
        public List<Classroom> Classrooms { get; set; }
        public List<Professor> Professors { get; set; }
        public List<Group> Groups { get; set; }
        public List<Timeslot> Timeslots { get; set; }

        public CSPSolver(List<Subject> subjects, List<Classroom> classrooms, List<Professor> professors, List<Group> groups, List<Timeslot> timeslots)
        {
            Subjects = subjects;
            Classrooms = classrooms;
            Professors = professors;
            Groups = groups;
            Timeslots = timeslots;
        }

        public Timetable GenerateTimetable()
        {
            Timetable timetable = new Timetable();

            foreach(var group in Groups)
            {
                if(!Backtrack(timetable, group))
                {
                    Console.WriteLine($"Unable to generate a complete timetable for group {group.Name}.");
                }
            }

            return timetable;
        }


        private bool Backtrack(Timetable timetable, Group group)
        {
            int maxSubjects = Math.Min(group.Subjects.Count, Timeslots.Count);

            if(timetable.ScheduledClasses.Count(sc => sc.Group.Id == group.Id) == maxSubjects)
            {
                return true;
            }

            foreach(var subject in group.Subjects)
            {
                if(timetable.ScheduledClasses.Any(sc => sc.Group.Id == group.Id && sc.Subject.Id == subject.Id))
                {
                    continue;
                }

                foreach(var professor in Professors.Where(p => p.Subjects.Any(s => s.Id == subject.Id)))
                {
                    foreach(var classroom in Classrooms)
                    {
                        foreach(var timeslot in Timeslots)
                        {
                            var scheduledClass = new ScheduledClass
                            {
                                Subject = subject,
                                Professor = professor,
                                Classroom = classroom,
                                Group = group,
                                Timeslot = timeslot
                            };

                            if(IsValidAssignment(timetable, scheduledClass))
                            {
                                timetable.ScheduledClasses.Add(scheduledClass);

                                if(Backtrack(timetable, group))
                                {
                                    return true;
                                }

                                timetable.ScheduledClasses.Remove(scheduledClass);
                            }
                        }
                    }
                }
            }

            return false;
        }


        private bool IsValidAssignment(Timetable timetable, ScheduledClass scheduledClass)
        {
            if(scheduledClass.Classroom.Capacity < scheduledClass.Group.NumberOfStudents)
            {
                return false;
            }

            foreach(var existingClass in timetable.ScheduledClasses)
            {
                if(existingClass.Timeslot.Id == scheduledClass.Timeslot.Id)
                {
                    if(existingClass.Professor.Id == scheduledClass.Professor.Id)
                    {
                        return false;
                    }

                    if(existingClass.Group.Id == scheduledClass.Group.Id)
                    {
                        return false;
                    }

                    if(existingClass.Classroom.Id == scheduledClass.Classroom.Id)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
