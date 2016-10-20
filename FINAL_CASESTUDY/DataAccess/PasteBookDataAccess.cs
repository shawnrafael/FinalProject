using PasteBookEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.PasteBookAccessLayer
{
    public class DataAccess<T> where T : class
    {
        public bool Create(T newEntity)
        {
            int status = 0;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.Entry(newEntity).State = System.Data.Entity.EntityState.Added;
                    status = context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return status != 0;
        }

        public List<T> EntityList()
        {
            List<T> entityList = new List<T>();
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    entityList = context.Set<T>().ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return entityList;
        }

        public bool Edit(T newEntity)
        {
            int status = 0;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.Entry(newEntity).State = System.Data.Entity.EntityState.Modified;
                    status = context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return status != 0;
        }

        public bool Delete(T newEntity)
        {
            int status = 0;
            try
            {
                using (var context = new PASTEBOOKEntities())
                {
                    context.Entry(newEntity).State = System.Data.Entity.EntityState.Deleted;
                    status = context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return status != 0;
        }       
    }

    //JOIN PARENT CHILD
    public interface IChildOf<T>
    {
        T Parent { get; set; }
    }

    public interface IParent<T>
    {
        List<T> Children { get; set; }
    }

    public abstract class BaseItem<T1, T2> : IParent<T1>, IChildOf<T2>
    {
        public List<T1> Children { get; set; }
        public T2 Parent { get; set; }
    }
    //This class handles the top level parent
    public class ItemA : IParent<ItemB>
    {
        public List<ItemB> Children { get; set; }
    }
    public class ItemB : BaseItem<ItemC, ItemA>
    {
    }
    public class ItemC : BaseItem<ItemD, ItemB>
    {
    }
    public class ItemD : IChildOf<ItemC>
    {
        public ItemC Parent { get; set; }
    }

}
