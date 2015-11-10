using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Common;

namespace Domain.DbProviders
{
    public class ConnectionConfiguration
    {
        public static String ConnectionString { get; private set; }
        public static String Provider { get; private set; }

        public static void Configure()
        {
            var connectionStringSettings = ConfigurationManager.ConnectionStrings["SQLConnection"];

            ConnectionString = connectionStringSettings.ConnectionString;
            Provider = connectionStringSettings.ProviderName;

            ConnectProvider.Init();
        }
    }

    class ConnectProvider
    {
        private static DbProviderFactory df;

        public static void Init()
        {
            df = DbProviderFactories.GetFactory(ConnectionConfiguration.Provider);
        }

        public static DbConnection CreateConnection()
        {
            var connection = df.CreateConnection();
            connection.ConnectionString = ConnectionConfiguration.ConnectionString;

            return connection;
        }

        public static DbCommand CreateCommand()
        {
            return df.CreateCommand();
        }
    }
}
