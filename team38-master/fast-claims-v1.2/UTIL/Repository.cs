using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace fast_claims_v1._2.UTIL
{
    public interface  IRepository<tbl_Entity> where tbl_Entity:class
    {
        IEnumerable<tbl_Entity> GetAllRecords();
        IQueryable<tbl_Entity> GetAllRecordsIQueryable();
        int GetAllRecordCount();
        bool Add(tbl_Entity entity);
        bool Update(tbl_Entity entity);

        bool UpdateByWhereClause(Expression<Func<tbl_Entity, bool>> wherePredict, Action<tbl_Entity> ForEachPredict);
        tbl_Entity GetFirstOrDefault(int recordId);
        bool Remove(tbl_Entity entity);
        bool RemovebyWhereClause(Expression<Func<tbl_Entity, bool>> wherePredict);

        bool RemoveRangeBywhereClause(Expression<Func<tbl_Entity, bool>> wherePredict);
        tbl_Entity GetFirstorDefaultByParameter(Expression<Func<tbl_Entity, bool>> wherePredict);
        IEnumerable<tbl_Entity> GetListParameter(Expression<Func<tbl_Entity, bool>> wherePredict);
        IEnumerable<tbl_Entity> GetResultBySqlprocedure(string query, params object[] parameters);

        IEnumerable<tbl_Entity> GetRecordsToShow(int PageNo, int PageSize, int CurrentPage, Expression<Func<tbl_Entity,bool>> wherePredict, Expression<Func<tbl_Entity,int>> orderByPredict);
    }
}