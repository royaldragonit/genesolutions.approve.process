using eDoc_Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDoc_Core.Core
{
    public static class DbContextExtension
    {
        /// <summary>
        /// Hàm để SELECT trên Views
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DbRawSqlQuery<T> Views<T>(this eDocumentContext db, string sql = null) where T : class
        {
            if (sql == null)
                sql = string.Concat("SELECT * FROM dbo.", typeof(T).Name);
            var query = db.Database.SqlQuery<T>(sql);
            return query;
        }
        public static DbRawSqlQuery<T> StoredProcedure<T>(this eDocumentContext db, string sql, params object[] param) where T : class
        {
            var query = db.Database.SqlQuery<T>(sql, param);
            return query;
        }
        private static DbRawSqlQuery<T> Exec<T>(this eDocumentContext db, params object[] param) where T : class
        {
            string paramStr = "";
            foreach (var item in param)
            {
                paramStr += string.Concat($"'{item}'", ", ");
            }
            if (paramStr != "")
            {
                paramStr = paramStr.Substring(0,paramStr.Length-2);
            }
            string sql = $"EXEC {typeof(T).Name} {paramStr} ";
            var query = db.Database.SqlQuery<T>(sql, param);
            return query;
        }
        public static async Task<List<T>> ToListAsync<T>(this eDocumentContext db, params object[] param) where T : class
        {
            var data=await Exec<T>(db,param).ToListAsync();
            return data;
        }
        public static async Task<T> FirstOrDefaultAsync<T>(this eDocumentContext db, params object[] param) where T : class
        {
            var data=await Exec<T>(db,param).FirstOrDefaultAsync();
            return data;
        }

    }
}
