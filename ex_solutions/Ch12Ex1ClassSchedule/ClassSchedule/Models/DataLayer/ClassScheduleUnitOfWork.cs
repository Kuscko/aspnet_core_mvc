namespace ClassSchedule.Models
{
    public class ClassScheduleUnitOfWork : IClassScheduleUnitOfWork
    {
        private ClassScheduleContext context { get; set; }
        public ClassScheduleUnitOfWork(ClassScheduleContext ctx) => context = ctx;

        private Repository<Day> dayData;
        public Repository<Day> Days {
            get {
                if (dayData == null)
                    dayData = new Repository<Day>(context);
                return dayData;
            }
        }

        private Repository<Teacher> teacherData;
        public Repository<Teacher> Teachers {
            get {
                if (teacherData == null)
                    teacherData = new Repository<Teacher>(context);
                return teacherData;
            }
        }

        private Repository<Class> classData;
        public Repository<Class> Classes {
            get {
                if (classData == null)
                    classData = new Repository<Class>(context);
                return classData;
            }
        }

        public void Save() => context.SaveChanges();
    }
}
