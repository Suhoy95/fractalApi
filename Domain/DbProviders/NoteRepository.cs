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
            throw new NotImplementedException();
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public bool Exist(int Id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            cmd.Dispose();
        }
    }
}
