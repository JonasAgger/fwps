using System.Data.Entity;

namespace DABHandin2SQL
{
    public class Entity { } //Base class for repository

    public interface IRepository<T>
    {
        void Create(T t);
        T Read(int id);
        void Update(int id, T t);
        void Delete(T t);
    }


    public class Repository<T> : IRepository<T> where T : Entity
    { 
        private readonly Context _context;

        public Repository(Context context)
        {
            _context = context;
        }

        public void Create(T t)
        {
            _context.Entry<T>(t).State = EntityState.Added;
            _context.SaveChanges();
        }

        public T Read(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Update(int id, T t)
        {
            _context.Entry<T>(t).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(T t)
        {
            _context.Entry<T>(t).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}