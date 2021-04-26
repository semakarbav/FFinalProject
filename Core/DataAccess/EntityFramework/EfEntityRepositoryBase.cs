using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess;
using System.Linq;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework
{
   public class EfEntityRepositoryBase<TEntity,TContext>:IEntityRepository<TEntity>
        where TEntity : class,IEntity,new()
       where TContext:DbContext,new()
    {
        public void Add(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);//referansı yakala
                addedEntity.State = EntityState.Added;//eklenecek nesne
                context.SaveChanges();//değişikleri ekle
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);//referansı yakala
                deletedEntity.State = EntityState.Deleted;//eklenecek nesne
                context.SaveChanges();//değişikleri ekle
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);

            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null) //isterse filtre verebilir isterse vermeyebilir default null 
        {
            using (TContext context = new TContext())
            {
                return filter == null // filtre null mı? 
                    ? context.Set<TEntity>().ToList() //evetse tümünü getirir
                    : context.Set<TEntity>().Where(filter).ToList(); // hayırsa filtreleyerek verir
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);//referansı yakala
                updatedEntity.State = EntityState.Modified;//eklenecek nesne
                context.SaveChanges();//değişikleri ekle
            }
        }
    }
}
