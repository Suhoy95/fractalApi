using Domain.Entities;
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
    public class ItemRepository
    {
        protected DbConnection connection;
        protected DbCommand cmd;

        public ItemRepository()
        {
            connection = ConnectProvider.CreateConnection();
            cmd = ConnectProvider.CreateCommand();

            connection.Open();
            cmd.Connection = connection;
        }

        public Item CreateItem(Item item)
        {
            item.id = Create(item.gridId);

            cmd.CommandText = "";
            PrepareVerRelInsertion(item);
            PrepareHorRelInsertion(item);
            cmd.ExecuteNonQuery();

            return item;
        }

        private int Create(int gridId)
        {
            cmd.CommandText = "EXEC CreateItem @id;";
            CreateIntParameter(gridId, "id");
            var res = cmd.ExecuteScalar();
            return (int)(Decimal)res;
        }

        private void PrepareHorRelInsertion(Item item)
        {
            for(var i = 0; i < item.analogy.Length; i++)
            {
                cmd.CommandText += "EXEC CreateHorRel @idA"+i+", @idB"+i+";";

                CreateIntParameter(item.id, "idA" + i);
                CreateIntParameter(item.analogy[i], "idB" + i);
            }
        }

        private void PrepareVerRelInsertion(Item item)
        {
            for (var i = 0; i < item.sub.Length; i++)
            {
                cmd.CommandText += "EXEC CreateVerRel @meSup"+i+", @sub"+i+";";

                CreateIntParameter(item.id, "meSup" + i);
                CreateIntParameter(item.sub[i], "sub" + i);
            }

            for (var i = 0; i < item.sup.Length; i++)
            {
                cmd.CommandText += "EXEC CreateVerRel @sup"+i+", @meSub"+i+";";

                CreateIntParameter(item.sup[i], "sup" + i);
                CreateIntParameter(item.id, "meSub" + i);
            }
        }

        protected void CreateIntParameter(int id, String name)
        {
            var param = new SqlParameter();
            param.ParameterName = name;
            param.SqlDbType = SqlDbType.Int;
            param.Value = id;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param); 
        }

        protected void CreateTextParameter(String text, String name)
        {
            var param = new SqlParameter();
            param.ParameterName = name;
            param.SqlDbType = SqlDbType.NVarChar;
            param.Value = text;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);
        }
    }
}
