using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;

namespace Domain.DbProviders
{
    public class GridRepository : IGridRepository, IDisposable
    {
        private DbConnection connection;
        private DbCommand cmd;

        public GridRepository()
        {
            connection = ConnectProvider.CreateConnection();
            cmd = ConnectProvider.CreateCommand();

            connection.Open();
            cmd.Connection = connection;
        }

        public Grid Get(string slug)
        {
            var grid = new Grid();
            int id;
            CreateSlugParameter(slug);
            id = GetGridInformation(grid);

            CreateIdParameter(id);
            grid.Items = GetGridItems();
            FillNoteItem(grid.Items);
            FillGridItem(grid.Items);

            return grid;
        }

        private void CreateSlugParameter(String slug)
        {
            var param = new SqlParameter(); 
            param.ParameterName = "slug"; 
            param.SqlDbType = SqlDbType.VarChar; 
            param.Value = slug; 
            param.Direction = ParameterDirection.Input; 
            cmd.Parameters.Add(param); 
        }

        private void CreateIdParameter(int id)
        {
            var param = new SqlParameter();
            param.ParameterName = "id";
            param.SqlDbType = SqlDbType.Int;
            param.Value = id;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param); 
        }

        private int GetGridInformation(Grid grid)
        {
            cmd.CommandText = "EXEC GetList @slug;";
            using (DbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    grid.Slug = dr.GetString(0);
                    grid.Id = dr.GetInt32(1);
                    grid.Title = dr.GetString(2);
                    grid.Width = dr.GetInt32(4);
                    grid.FixedWidth = dr.GetBoolean(5);
                    return dr.GetInt32(1);
                }
            }
            throw new Exception("Grid Not Found");
        }

        private Item[][] GetGridItems()
        {
            cmd.CommandText = "EXEC GetItems @id;";

            var items = new List<List<Item>>();
            using (DbDataReader dr = cmd.ExecuteReader())
            {
                var column = new List<Item>();
                var x = 0;
                while (dr.Read())
                {
                    while (dr.GetInt32(2) != x){
                        items.Add(column);
                        column = new List<Item>();
                        x++;
                    }
                    column.Add(new Item() { 
                        id = dr.GetInt32(0),
                        analogy = CreateId(dr, 4),
                        sub = CreateId(dr, 5),
                        sup = CreateId(dr, 6)
                    });
                }
                items.Add(column);
            }

            return items.Select(column => column.ToArray()).ToArray();
        }

        private int[] CreateId(DbDataReader dr, int ordinal)
        {
            if (dr.IsDBNull(ordinal))
                return new int[0];

            return dr.GetString(ordinal).Split(',').Select(int.Parse).ToArray();
        }

        private void FillNoteItem(Item[][] items)
        {
            cmd.CommandText = "EXEC GetNotes @id;";
       
            using (DbDataReader dr = cmd.ExecuteReader())
            {
                int x = 0;
                int y = 0;
                while (dr.Read())
                {
                    var found = false;
                    for (; x < items.Length; x++)
                    {
                        for (; y < items[x].Length; y++)
                            if (items[x][y].id == dr.GetInt32(0))
                            {
                                items[x][y].type = "note";
                                items[x][y].title = dr.GetString(1);
                                items[x][y].text = dr.GetString(2);
                                found = true;
                                break;
                            }
                        if (found)
                            break;
                        y = 0;
                    }
                }
            }
        }

        private void FillGridItem(Item[][] items)
        {
            cmd.CommandText = "EXEC GetListsAsItems @id;";

            using (DbDataReader dr = cmd.ExecuteReader())
            {
                int x = 0;
                int y = 0;
                while (dr.Read())
                {
                    var found = false;
                    for (; x < items.Length; x++)
                    {
                        for (; y < items[x].Length; y++)
                            if (items[x][y].id == dr.GetInt32(0))
                            {
                                items[x][y].type = "gridItem";
                                items[x][y].slug = dr.GetString(1);
                                items[x][y].title = dr.GetString(2);
                                items[x][y].text = dr.GetString(3);
                                found = true;
                                break;
                            }
                        if (found)
                            break;
                        y = 0;
                    }
                }
            }
        }


        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Exsist(int id)
        {
            throw new NotImplementedException();
        }

        public Item Create(Item grid)
        {
            throw new NotImplementedException();
        }

        public void Update(Item grid)
        {
            throw new NotImplementedException();
        }

        public void Update(PartialGrid grid)
        {
            throw new NotImplementedException();
        }
    
        public void Dispose()
        {
            connection.Dispose();
            cmd.Dispose();
        }
    }
}
