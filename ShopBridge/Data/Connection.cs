using System;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace ShopBridge.Connection
{
    public class AppSetting
    {
        public string DataProvider { get; set; }
        public string ConnectionString { get; set; }
    }
    public static class ShopBridgeConnection
    {
        public static AppSetting iobjAppSetting { get; set; }

        private static void RegisterFactory(string astrDataProvider)
        {
            if (astrDataProvider.Equals("System.Data.SqlClient") && !DbProviderFactories.TryGetFactory(astrDataProvider, out DbProviderFactory ldpfProvider))
            {
                DbProviderFactories.RegisterFactory(astrDataProvider, System.Data.SqlClient.SqlClientFactory.Instance);
            }
        }

        public static IDbCommand GetDBCommand(string astrQueryOrRef, IDbConnection aDBConnection, CommandType CommandType = CommandType.Text)
        {
            IDbCommand lcmdFramework = null;
            RegisterFactory(iobjAppSetting.DataProvider);
            DbProviderFactory ldpfProvider = DbProviderFactories.GetFactory(iobjAppSetting.DataProvider);
            lcmdFramework = ldpfProvider.CreateCommand();
            lcmdFramework.Connection = aDBConnection;
            lcmdFramework.CommandType = CommandType;
            lcmdFramework.CommandText = astrQueryOrRef;
            lcmdFramework.CommandTimeout = 0;
            if (aDBConnection != null && aDBConnection.GetType().Name.ToLower() == "oracleconnection")
            {
                PropertyInfo lpri = lcmdFramework.GetType().GetProperty("BindByName");
                if (lpri != null)
                    lpri.SetValue(lcmdFramework, true, null);
            }
            return lcmdFramework;
        }
        public static IDbConnection GetDBConnection()
        {
            if (string.IsNullOrEmpty(ShopBridgeConnection.iobjAppSetting.ConnectionString))
                throw new ArgumentNullException("conection string value is Null or Empty");
            if (string.IsNullOrEmpty(ShopBridgeConnection.iobjAppSetting.DataProvider))
                throw new ArgumentNullException("dbfactory value is Null or Empty");

            IDbConnection lconn = null;
            RegisterFactory(ShopBridgeConnection.iobjAppSetting.DataProvider);
            DbProviderFactory ldpfProvider = DbProviderFactories.GetFactory(ShopBridgeConnection.iobjAppSetting.DataProvider);
            lconn = ldpfProvider.CreateConnection();
            lconn.ConnectionString = ShopBridgeConnection.iobjAppSetting.ConnectionString;
            lconn.Open();
            return lconn;
        }

         public static IDbDataParameter GetDBParameter(string astrParameterName, DbType astrDataType)
        {
            IDbDataParameter ldbpFramework = null;
            RegisterFactory(ShopBridgeConnection.iobjAppSetting.DataProvider);
            DbProviderFactory ldpfProvider = DbProviderFactories.GetFactory(ShopBridgeConnection.iobjAppSetting.DataProvider);
            ldbpFramework = ldpfProvider.CreateParameter();
            if (!string.IsNullOrEmpty(astrParameterName))
            {
                ldbpFramework.ParameterName = astrParameterName;
            }
            ldbpFramework.DbType = astrDataType;
            return ldbpFramework;
        }
    }
}