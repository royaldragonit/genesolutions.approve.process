using GS.eDocument.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.eDocument.Models.Core
{
    public static class YourContextExtensions
    {
        public static DbRawSqlQuery<T> Views<T>(this eDocumentContext db) where T : class
        {
            string sql = string.Concat("SELECT * FROM dbo.", typeof(T).Name);
            var query= db.Database.SqlQuery<T>(sql);
            return query;
        }

    }
}
