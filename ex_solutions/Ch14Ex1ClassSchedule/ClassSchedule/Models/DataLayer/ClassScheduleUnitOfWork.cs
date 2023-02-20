namespace ClassSchedule.Models
{
    public class ClassScheduleUnitOfWork : IClassScheduleUnitOfWork
    {
        private ClassScheduleContext context { get; set; }
        public ClassScheduleUnitOfWork(ClassScheduleContext ctx) => context = ctx;

        private IRepository<Day> dayData;
        public IRepository<Day> Days {
            get {
                if (dayData == null)
                    dayData = new Repository<Day>(context);
                return dayData;
            }
        }

        private IRepository<Teacher> teacherData;
        public IRepository<Teacher> Teachers {
            get {
                if (teacherData == null)
                    teacherData = new Repository<Teacher>(context);
                return teacherData;
            }
        }

        private IRepository<Class> classData;
        public IRepository<Class> Classes {
            get {
                if (classData == null)
                    classData = new Repository<Class>(context);
                return classData;
            }
        }

        public void Save() => context.SaveChanges();
    }
}
