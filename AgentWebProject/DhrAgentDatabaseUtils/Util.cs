using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DhrAgentDatabaseUtils
{
    public static class Util
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> varlist)
        {
            if (varlist == null)
                return null;
            DataTable dtReturn = new DataTable();
            // column names 
            PropertyInfo[] oProps = null;
            // Could add a check to verify that there is an element 0 
            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, 
                // Only first time, others will follow 
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        // Note that we must check a nullable type 
                        // else method will throw and error 
                        Type colType = pi.PropertyType;
                        if ((colType.IsGenericType) &&
                            (colType.GetGenericTypeDefinition() == typeof(Nullable)))
                        {
                            // Since all the elements have same type 
                            // you can just take the first element and get type 
                            colType = colType.GetGenericArguments()[0];
                        }
                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }
                DataRow dr = dtReturn.NewRow();
                // Iterate through each property in PropertyInfo 
                foreach (PropertyInfo pi in oProps)
                {
                    // Handle null values accordingly 
                    dr[pi.Name] = pi.GetValue(rec, null) == null
                                  ? DBNull.Value
                                  : pi.GetValue(rec, null);
                }
                dtReturn.Rows.Add(dr);
            }
            return (dtReturn);
        }
    }

}
