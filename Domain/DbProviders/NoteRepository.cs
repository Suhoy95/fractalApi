using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;
using System.Data.Common;

namespace Domain.DbProviders
{
    public class NoteRepository : ItemRepository, INoteRepository
    {
        public Item Create(Item note)
        {
            try
            {
                BeginTransaction();
                note = CreateItem(note);
                CreateNote(note);
                EndTransaction();
                return note;
            } catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception("Error in creating note", ex);
            }
        }

        private void CreateNote(Item note)
        {
            cmd.CommandText = "EXEC CreateNote @id, @title, @text;";
            CreateIntParameter(note.id, "id");
            CreateTextParameter(note.title, "title");
            CreateTextParameter(note.text, "text");
            cmd.ExecuteNonQuery();
            ClearCommand();
        }

        public void Update(Item note)
        {
            try
            {
                BeginTransaction();

                UpdateItem(note);

                cmd.CommandText = "EXEC UpdateNote @id, @title, @text;";
                CreateIntParameter(note.id, "id");
                CreateTextParameter(note.title, "title");
                CreateTextParameter(note.text, "text");
                cmd.ExecuteNonQuery();

                EndTransaction();
            } catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception("Error in updating note", ex);
            }
        }

        public void Delete(int Id)
        {
            try
            {
                BeginTransaction();

                ClearCommand();
                cmd.CommandText = "EXEC DeleteItem @id;";
                CreateIntParameter(Id, "id");
                cmd.ExecuteNonQuery();

                EndTransaction();
            } catch(Exception ex)
            {
                RollbackTransaction();
                throw new Exception("Error in deleting note", ex);
            }
        }

        public bool Exist(int Id)
        {
            ClearCommand();
            cmd.CommandText = "EXEC ExistNote @id;";
            CreateIntParameter(Id, "id");
            return 1 == (int)cmd.ExecuteScalar();
        }
    }
}
