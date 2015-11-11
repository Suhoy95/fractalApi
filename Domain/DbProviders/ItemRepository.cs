using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;

namespace Domain.DbProviders
{
    public class ItemRepository : DbHelper, IItemRepository
    {
        public Item CreateItem(Item item)
        {
            item.id = Create(item.gridId);

            cmd.CommandText = "";
            PrepareVerRelInsertion(item);
            PrepareHorRelInsertion(item);
            if(cmd.CommandText != "")
                cmd.ExecuteNonQuery();

            ClearCommand();

            return item;
        }

        public void UpdateItem(Item item)
        {
            cmd.CommandText = "";
            PrepareClearRel(item);
            PrepareVerRelInsertion(item);
            PrepareHorRelInsertion(item);
            if(cmd.CommandText != "")
                cmd.ExecuteNonQuery();  

            ClearCommand();
        }

        public void UpdateCoord(int[][] coords)
        {
            ClearCommand();
            for(var i = 0; i < coords.Length; i++)
            {
                cmd.CommandText += "EXEC UpdateCoord @id" + i + ",@x" + i + ",@y" + i + ";";
                CreateIntParameter(coords[i][0], "id"+i);
                CreateIntParameter(coords[i][1], "x" + i);
                CreateIntParameter(coords[i][2], "y" + i);
            }
            if(cmd.CommandText == "")
                cmd.ExecuteNonQuery();
        }

        private int Create(int gridId)
        {
            cmd.CommandText = "EXEC CreateItem @id;";
            CreateIntParameter(gridId, "id");
            var res = cmd.ExecuteScalar();
            return (int)(Decimal)res;
        }


        private void PrepareClearRel(Item item)
        {
            cmd.CommandText += "EXEC ClearRel @clearid;";
            CreateIntParameter(item.id, "clearid");
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
    }
}
