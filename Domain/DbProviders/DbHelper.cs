using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DbProviders
{
    public class DbHelper : IDisposable
    {
        protected DbConnection connection;
        protected DbCommand cmd;
        protected DbTransaction tx;

        public DbHelper()
        {
            connection = ConnectProvider.CreateConnection();
            cmd = ConnectProvider.CreateCommand();

            connection.Open();
            cmd.Connection = connection;
        }

        protected void ClearCommand()
        {
            cmd.CommandText = "";
            cmd.Parameters.Clear();
        }

        protected void CreateIntParameter(int val, String name)
        {
            if (cmd.Parameters.IndexOf(name) >= 0)
            {
                cmd.Parameters[name].Value = val;
                return;
            }
            var param = new SqlParameter();
            param.ParameterName = name;
            param.SqlDbType = SqlDbType.Int;
            param.Value = val;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);
        }

        protected void CreateBitParameter(bool bit, String name)
        {
            if (cmd.Parameters.IndexOf(name) >= 0)
            {
                cmd.Parameters[name].Value = bit;
                return;
            }
            var param = new SqlParameter();
            param.ParameterName = name;
            param.SqlDbType = SqlDbType.Bit;
            param.Value = bit;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);
        }

        protected void CreateTextParameter(String text, String name)
        {
            if (cmd.Parameters.IndexOf(name) >= 0)
            {
                cmd.Parameters[name].Value = text;
                return;
            } 
            var param = new SqlParameter();
            param.ParameterName = name;
            param.SqlDbType = SqlDbType.NVarChar;
            param.Value = text;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);
        }

        protected void BeginTransaction()
        {
            tx = connection.BeginTransaction();
            cmd.Transaction = tx;
        }

        protected void EndTransaction()
        {
            tx.Commit();
        }

        protected void RollbackTransaction()
        {
            tx.Rollback();
        }

        public void Dispose()
        {
            connection.Dispose();
            cmd.Dispose();
        }
    }
}
