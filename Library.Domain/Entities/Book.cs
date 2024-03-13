using Library.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public string ISBN { get; set; }
        public string Name { get; set; }
        public DateTime? TakeDateTime { get; set; }
        public  DateTime? ReturnDateTime { get; set; }
        public  BookAvailability Status { get; set; }
    }
}
