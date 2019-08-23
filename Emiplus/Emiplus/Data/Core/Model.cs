using Emiplus.Data.Database;
using Emiplus.Data.Helpers;
using System.Data.Entity;

namespace Emiplus.Data.Core
{
    public class Model<T> where T : ModelEntityWithID
    {
        protected Log log;
        protected ContextoData contexto;
        protected Alert alert;

        protected IDbSet<T> DbSet { get; set; }
        public ContextoData Context { get; set; }
        //public DbContext Context { get; set; }

        public Model()
        {
            log = new Log();
            Context = new ContextoData();
            alert = new Alert();

            DbSet = Context.Set<T>();
        }

        public Model(ContextoData context)
        {
            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        private void Insert(T item)
        {
            this.DbSet.Add(item);
            this.Context.SaveChanges();
        }

        private void Update(T item)
        {
            this.Context.Entry(item).State = EntityState.Modified;
            this.Context.SaveChanges();
        }

        public virtual void Save(T item)
        {
            if (item.ID <= 0)
            {
                Insert(item);
            }
            else
            {
                Update(item);
            }
        }

    }
}
