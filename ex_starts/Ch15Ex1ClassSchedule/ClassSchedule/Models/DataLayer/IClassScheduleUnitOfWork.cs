namespace ClassSchedule.Models
{
    public interface IClassScheduleUnitOfWork
    {
        public IRepository<Day> Days { get; }
        public IRepository<Teacher> Teachers { get; }
        public IRepository<Class> Classes { get; }

        public void Save();
    }
}
