using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using DHRSoftphone.AgentExceptions;

namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    /// <summary>
    /// Copyright (C) 1985-2012 Vincent Qiu, Direct HR
    /// Http://hoho.im 版权所有。
    /// NameSpace: DirectHRSoftphoneAgent_QZJ
    /// FullName: DirectHRSoftphoneAgent_QZJ.AgentDatabaseProvider
    /// Class Created: 2011/11/25 16:42
    /// On Computer: NOV30TH-LAPTOP - Administrator
    ///
    /// </summary>
    public static class AgentDatabaseProvider
    {
        public static string DataSource { get; set; }

        public static List<AgentCityItem> CityItems { get; set; }

        public static List<AgentCityItem> AllData { get; set; }

        public static string CurrentAreaCode { get; set; }

        // public AgentDatabaseProvider()
        //{
        //    DataSource = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=cellphone_agentdatabase.dat";
        //}

        public class AgentCityItem
        {
            public string City { get; set; }

            public string CellphoneNumber { get; set; }

            public string AreaCode { get; set; }

            public string CardType { get; set; }

            public string DataSource { get; set; }
        }

        static public List<AgentCityItem> GetDataList()
        {
            if (AllData != null && AllData.Count > 0)
                return AllData;
            string sql = "select [code],[city],[num],[cardtype] from [list]";
            OleDbConnection connection = new OleDbConnection(DataSource);
            OleDbCommand cmd = new OleDbCommand(sql, connection);
            connection.Open();
            var reader = cmd.ExecuteReader();

            if (AllData == null)
                AllData = new List<AgentCityItem>();
            if (AllData.Count() != 0)
                AllData.Clear();
            while (reader.Read())
            {
                AgentCityItem item = new AgentCityItem()
                {
                    AreaCode = reader["code"].ToString(),
                    City = reader["city"].ToString(),
                    CardType = reader["cardtype"].ToString(),
                    CellphoneNumber = reader["num"].ToString()
                };
                AllData.Add(item);
            }
            if (AllData.Count == 0)
                throw new AgentException("Loaded city list with no data");
            reader.Close();
            cmd.Dispose();
            connection.Close();
            return AllData;
        }

        /// <summary>
        /// Gets the city list.
        /// </summary>
        /// <returns></returns>
        static public List<AgentCityItem> GetCityList()
        {
            if (CityItems != null && CityItems.Count > 0)
                return CityItems;
            string sql = "select [code],[city] from [list] group by [city],[code]";
            string sqlFix0 = "select [code] from [list] where [city] like \"%{0}%\" and code <> \"0\"";
            string sqlFix1 = "update [list] set [code]={0} where code =\"0\" and city like '%{1}%'";
            OleDbConnection connection = new OleDbConnection(DataSource);
            OleDbCommand cmd = new OleDbCommand(sql, connection);
            connection.Open();
            var reader = cmd.ExecuteReader();

            if (CityItems == null)
                CityItems = new List<AgentCityItem>();
            if (CityItems.Count() != 0)
                CityItems.Clear();
            while (reader.Read())
            {
                if (reader["code"].ToString() == "0")
                {
                    ////fix database
                    //var city = reader["city"].ToString().Length > 5 ?
                    //    reader["city"].ToString().Substring(0, 5) :
                    //    reader["city"].ToString();
                    //var sqlfixing0 = string.Format(sqlFix0, city);
                    //OleDbCommand cmd1 = new OleDbCommand(sqlfixing0, connection);
                    //var code = cmd1.ExecuteScalar();
                    //if (code != null)
                    //{
                    //    var sqlfixing1 = string.Format(sqlFix1, code.ToString(), city);
                    //    OleDbCommand cmd2 = new OleDbCommand(sqlfixing1, connection);
                    //    cmd2.ExecuteNonQuery();
                    //}
                    continue;
                }
                if (CityItems.Any(c=>c.City == reader["city"].ToString() ||
                    c.AreaCode == reader["code"].ToString()))
                    continue;
                AgentCityItem item = new AgentCityItem()
                {
                    AreaCode = reader["code"].ToString(),
                    City = reader["city"].ToString(),
                };
                CityItems.Add(item);
            }
            if (CityItems.Count == 0)
                throw new AgentException("Loaded city list with no data");
            reader.Close();
            cmd.Dispose();
            connection.Close();
            return CityItems;
        }

        /// <summary>
        /// Gets the city by area code.
        /// </summary>
        /// <param name="areaCode">The area code.</param>
        /// <returns></returns>
        static public AgentCityItem GetCityByAreaCode(string areaCode)
        {
            //var city = CityItems.Where(c => c.AreaCode == areaCode);
            return CityItems.FirstOrDefault(c => c.AreaCode == areaCode);
        }

        /// <summary>
        /// Gets the area code by city.
        /// </summary>
        /// <param name="cityname">The cityname.</param>
        /// <returns></returns>
        static public AgentCityItem GetAreaCodeByCity(string cityname)
        {
            return CityItems.First(c => c.City == cityname);
        }
    }
}