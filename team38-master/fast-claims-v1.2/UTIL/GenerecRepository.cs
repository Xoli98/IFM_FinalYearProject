using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Web;


namespace fast_claims_v1._2.UTIL
{

    
    public class GenerecRepository<tbl_Entity> :  IRepository<tbl_Entity> where tbl_Entity : class
    {


        Table<tbl_Entity> _dbSet;
        DatabaseDataContext database;



        //if it extends the DatabaseDataContext class, we can make the class use a parameterless constructor 
        public GenerecRepository(DatabaseDataContext database) {

            this.database = database;
            _dbSet = database.GetTable<tbl_Entity>();

        }
        public bool Add(tbl_Entity entity)
        {
            _dbSet.InsertOnSubmit(entity);
            try
            {
                database.SubmitChanges();
            }
            catch (Exception e)
            {
                printErr(e);
                return false;
            }
            return true;
        
        }

        public int GetAllRecordCount()
        {
            
            return _dbSet.Count();
        }

        public IEnumerable<tbl_Entity> GetAllRecords()
        {
            IEnumerable<tbl_Entity> results = null;
            try
            {
                results = _dbSet.ToList();
            }
            catch (Exception e)
            {
                printErr(e);
               
            }
            return results;
        }

        public IQueryable<tbl_Entity> GetAllRecordsIQueryable()
        {
            return _dbSet;
        }

        public tbl_Entity GetFirstOrDefault(int recordId)
        {
            return null;
        }

        public tbl_Entity GetFirstorDefaultByParameter(Expression<Func<tbl_Entity, bool>> wherePredict)
        {
            tbl_Entity results = null;
            try {
                results = _dbSet.Where(wherePredict).FirstOrDefault();
            } catch (Exception e) { printErr(e); }
            return results;
        }

        public IEnumerable<tbl_Entity> GetListParameter(Expression<Func<tbl_Entity, bool>> wherePredict)
        {

            IEnumerable<tbl_Entity> results = null;
            try
            {
                results = _dbSet.Where(wherePredict).ToList();
            }
            catch (Exception e)
            {
                printErr(e);
            }
            return results;
        }

        public IEnumerable<tbl_Entity> GetRecordsToShow(int PageNo, int PageSize, int CurrentPage, Expression<Func<tbl_Entity, bool>> wherePredict, Expression<Func<tbl_Entity, int>> orderByPredict)
        {
            IEnumerable<tbl_Entity> result = null;
            if (wherePredict != null)
            {
                try
                {
                    result = _dbSet.OrderBy(orderByPredict).Where(wherePredict).ToList();
                }
                catch (Exception e)
                {

                    printErr(e);
                }
                
            }
            else {
                try
                {
                    result = _dbSet.OrderBy(orderByPredict).ToList();
                }
                catch (Exception e)
                {

                    printErr(e);
                }

                 }

            return result;
        }

        public IEnumerable<tbl_Entity> GetResultBySqlprocedure(string query, params object[] parameters)
        {
            IEnumerable<tbl_Entity> result = null;
            if (parameters != null) {
                
                try
                {
                    result = database.ExecuteQuery<tbl_Entity>(query, parameters);
                }
                catch (Exception e)
                {
                    printErr(e);
                }

            } else {
                try
                {
                    result = database.ExecuteQuery<tbl_Entity>(query);
                }
                catch (Exception e)
                {
                    printErr(e);
                }
            }
            return result;
        }

        public bool Remove(tbl_Entity entity)
        {
            if (_dbSet.Contains(entity)) {
                try
                {
                    _dbSet.DeleteOnSubmit(entity);
                }
                catch (Exception e)
                {
                    printErr(e);
                    return false;
                }

            }
            return true;
        }

        public bool RemovebyWhereClause(Expression<Func<tbl_Entity, bool>> wherePredict)
        {
            throw new NotImplementedException();
        }

        public bool RemoveRangeBywhereClause(Expression<Func<tbl_Entity, bool>> wherePredict)
        {
            throw new NotImplementedException();
        }

        public bool Update(tbl_Entity entity)
        {
        
           return false;
            

        }

        public bool UpdateByWhereClause(Expression<Func<tbl_Entity, bool>> wherePredict, Action<tbl_Entity> ForEachPredict)
        {


            SurroundWithTry(()=>_dbSet.Where(wherePredict).ToList().ForEach(ForEachPredict));

            try
            {
                _dbSet.Where(wherePredict).ToList().ForEach(ForEachPredict);
            }
            catch (Exception e)
            {
                printErr(e);
                return false;
            }
            return true;
        }

        private void printErr(Exception e)
        {
            System.Diagnostics.Debug.Write(e.Message + e.InnerException + "\n");
        }

        private void SurroundWithTry(Action tryfunction)
        {
            try
            {
                tryfunction();
            }
            catch (Exception e)
            {

                System.Diagnostics.Debug.Write(e.Message + e.InnerException + "\n");
            }
        }

    }


    
   
}