using Microsoft.AspNetCore.Mvc;

namespace MemeryBank.Api.Models
{
    public class Meme
    {
        public int MemeId { get; set; }
        //[FromQuery]
        public string? Author { get; set; }

        public override string ToString()
        {
            return $"Meme Object - Meme Id {MemeId}, Author {Author}";
        }
    }
}
