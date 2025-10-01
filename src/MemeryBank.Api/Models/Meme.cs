using Microsoft.AspNetCore.Mvc;

namespace MemeryBank.Api.Models
{
    public class Meme
    {
        public int MemeId { get; set; }
        //[FromQuery]
        public string? Author { get; set; }
        public string? TopTextColor { get; set; }
        public string? BottomTextColor { get; set; }
        public string? Font {  get; set; }
        public override string ToString()
        {
            return $"Meme Object - Meme Id {MemeId}, Author {Author}";
        }
    }
}
