using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fast_claims_v1._2.UTIL
{
    public class GenericUnitOfWork:IDisposable
    {
        private DatabaseDataContext database = new DatabaseDataContext();
        public IRepository<tbl_entity> GetRepositoryInstance<tbl_entity>() where tbl_entity : class {
            return new GenerecRepository<tbl_entity>(database);
        }

        public bool SaveChanges() {
            try
            {
                database.SubmitChanges();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e.InnerException);
                return false;
            }
            return true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed) {
                if (disposing) {
                    database.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool disposed = false;


        
    }
}