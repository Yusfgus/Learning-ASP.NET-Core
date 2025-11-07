using System.Collections.Generic;

namespace EF_Core.SaveData.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public List<Book> Books { get; set; } = new();

        public override string ToString()
        {
            return $"[{Id}] {FName} {LName}";
        }

    }
}
