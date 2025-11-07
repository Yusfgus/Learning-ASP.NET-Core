using System.Dynamic;
using Microsoft.Identity.Client;

namespace EF_Core.SaveData.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price {get; set;}
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
