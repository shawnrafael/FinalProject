using PasteBookEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.AccessLayer
{   

    public class GenericDataAccess<T> where T : class
    {
        List<Exception> errorList = new List<Exception>();

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
            catch (Exception ex)
            {
                errorList.Add(ex);
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
            catch (Exception ex)
            {
                errorList.Add(ex);
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
            catch (Exception ex)
            {
                errorList.Add(ex);
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
            catch (Exception ex)
            {
                errorList.Add(ex);
            }
            return status != 0;
        }             
          
    }
}
