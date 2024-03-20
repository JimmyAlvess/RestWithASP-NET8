using RestWithASPNET.Models.Base;

namespace RestWithASPNET.Data.VO
{
    
    public class BookVO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public DateTime launch_date { get; set; }
    }
}
