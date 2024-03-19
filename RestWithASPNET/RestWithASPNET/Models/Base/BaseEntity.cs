using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNET.Models.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public int Id { get; set; }
    }
}
