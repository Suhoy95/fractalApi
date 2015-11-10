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
    public class NoteRepository : ItemRepository, INoteRepository, IDisposable
    {
        public Item Create(Item note)
        {
            note = CreateItem(note);
            CreateNote(note);
            return note;
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
            UpdateItem(note);
            ClearCommand();

            cmd.CommandText = "EXEC UpdateNote @id, @title, @text;";
            CreateIntParameter(note.id, "id");
            CreateTextParameter(note.title, "title");
            CreateTextParameter(note.text, "text");
            cmd.ExecuteNonQuery();
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public bool Exist(int Id)
        {
            ClearCommand();
            cmd.CommandText = "EXEC ExistNote @id;";
            CreateIntParameter(Id, "id");
            return 1 == (int)cmd.ExecuteScalar();
        }

        public void Dispose()
        {
            cmd.Dispose();
        }
    }
}
