using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPnet.Api.Entities;

namespace ASPnet.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
        public decimal Price { get; set; }
        public DateOnly ReleaseDate { get; set; }
    }
}