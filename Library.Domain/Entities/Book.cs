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
        public Guid Id { get; set; }
        public Guid GenreId { get; set; }
        public Guid AuthorId { get; set; }
        public string ISBN { get; set; }
        public string Name { get; set; }
        public DateTime TakeDateTime { get; set; }
        public  DateTime ReturnDateTime { get; set; }
        public  BookAvailability Status { get; set; }
    }
}
