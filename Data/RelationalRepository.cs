using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace Data
{

    // Relationsrepository

    public class RelationalRepository<TEntity> where TEntity : class
    {
        internal DataContext context;
        internal DbSet<TEntity> dbSet;

        public RelationalRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual ICollection<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }

        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

      
        public virtual TEntity GetSingle(Expression<Func<TEntity, bool>> func)
        {
            return dbSet.FirstOrDefault(func);
        }

        public virtual void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

    }

    // Mongorepository
    public class MongoRepository<TEntity> where TEntity : MongoBase
    {
        private MongoDatabase db;
        private MongoCollection<TEntity> collection;

        public MongoRepository()
        {
            GetDatabase();
            GetCollection();
        }


        public bool Add(TEntity entity)
        {
            //entity.Id = Guid.NewGuid();
            return collection.Insert(entity).Ok;
        }

        public bool Update(TEntity entity)
        {
            if (entity.Id == null)
                return Add(entity);

            return collection
                .Save(entity)
                    .DocumentsAffected > 0;
        }

        public bool Delete(TEntity entity)
        {
            return collection
                .Remove(Query.EQ("_id", entity.Id))
                    .DocumentsAffected > 0;
        }

        //public TEntity GetSingle(Expression<Func<TEntity, bool>> func)
        //{
        //    return collection.AsQueryable<TEntity>().FirstOrDefault(); 
        //}

        public IList<TEntity>
            Get(Expression<Func<TEntity, bool>> predicate)
        {
            return collection
                .AsQueryable<TEntity>()
                    .Where(predicate.Compile())
                        .ToList();
        }

        public IList<TEntity> GetAll()
        {
            return collection.FindAllAs<TEntity>().ToList();
        }

        public TEntity GetById(Guid id)
        {
            return collection.FindOneByIdAs<TEntity>(id);
        }

        private void GetDatabase()
        {

            // MongoDB server for test
            var client = new MongoClient("mongodb://wicsysuser:wicsys1!@ds062797.mongolab.com:62797/wicsys");
            var server = client.GetServer();
            db = server.GetDatabase("wicsys");

            ////Live MongoDB server
            //var client = new MongoClient("mongodb://wicsysuser:wicsys1!@ds056727.mongolab.com:56727/wicsysdb");
            //var server = client.GetServer();
            //db = server.GetDatabase("wicsysdb");
        }

        private void GetCollection()
        {
            collection = db
                .GetCollection<TEntity>(typeof(TEntity).Name);
        }

    }

}
