using Library.Domain.Entities;
using Library.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public GenreDTO Genre { get; set; }
        public AuthorDTO Author { get; set; }
        public string ISBN { get; set; }
        public string Name { get; set; }
        public DateTime? TakeDateTime { get; set; }
        public DateTime? ReturnDateTime { get; set; }
        public BookAvailability Status { get; set; }
    }
}
