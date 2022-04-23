using System;
using System.Collections.Generic;
using System.Text;

namespace GG2
{
    public class Note
    {
        public int Id { get; set; }
        public string Нeader { get; set; }
        public string Body { get; set; }

        public override bool Equals(object obj)
        {
            Note note = obj as Note;
            return this.Id == note.Id;
        }
    }
}
