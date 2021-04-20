using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Uplift.DataAccess.Data.Repository.IRepository
{
    public interface IStoredProcedureRepository : IDisposable
    {
        IEnumerable<T> List<T>(string procedureName, DynamicParameters parameters = null);

        void ExecuteWithoutReturn(string procedureName, DynamicParameters parameters = null);

        T ExecuteReturnScaller<T>(string procedureName, DynamicParameters parameters = null);
    }
}
