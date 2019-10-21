using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public abstract class BaseRepository<E> : IDisposable
         where E : BaseEntity, new()
    {
        

        protected DbContext Context { get; private set; }
        protected UnitOfWork UnitOfWork { get; set; }
        protected DbSet<E> Items { get; set; }

        protected virtual void MarkChildEntitiesAsDeleted(E item) { }


        public BaseRepository() : this(null)
        {

        }

        public BaseRepository(UnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                Context = new TestTaskDbContext();
            }
            else
            {
                UnitOfWork = unitOfWork;
                Context = UnitOfWork.Context;
            }

            Items = Context.Set<E>();
        }

        public List<E> GetAll(Expression<Func<E, bool>> filter, int page = 1, int itemsPerPage = int.MaxValue, Func<IQueryable<E>, IOrderedQueryable<E>> OrderByDescending = null,string childEntity=null,int skip=0)
        {
            IQueryable<E> query = Items;
           

            if (filter != null)
                query = query.Where(filter);

            if (childEntity != null)
            {
                query = query.Include(childEntity);
            }

            if (OrderByDescending == null)
                OrderByDescending = i => i.OrderBy(x => x.Id);

            query = OrderByDescending(query);

            if (skip == 0 && page > 0 && itemsPerPage > 0)
                query = query.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            else if (skip != 0 && itemsPerPage > 0)
                query = query.Skip(skip).Take(itemsPerPage);

            return query.ToList();
        }

        public E GetById(int Id,string childEntity= null)
        {
            if(childEntity==null)
            return Items.Find(Id);

            
            var query = Items.Include(childEntity)
                .Where(I=>I.Id==Id)
                .FirstOrDefault();
            return query;
        }

       

        public int Count(Expression<Func<E, bool>> filter)
        {
            IQueryable<E> query = Items;

            if (filter != null)
                query = query.Where(filter);

            return query.Count();
        }

        public virtual void Save(E item)
        {
            if (item.Id <= 0)
                Insert(item);
            else
                Update(item);
        }

        private void Insert(E item)
        {
            Items.Add(item);
            Context.SaveChanges();
        }

        private void Update(E item)
        {
            Context.Entry(item).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void Delete(E item)
        {
            MarkChildEntitiesAsDeleted(item);
            Items.Remove(item);
            Context.SaveChanges();
        }

       

        #region IDisposable Implementation

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (UnitOfWork == null)
                        Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
