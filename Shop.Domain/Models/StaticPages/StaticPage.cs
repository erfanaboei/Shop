using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.Models.StaticPages
{
    [Table("StaticPages")]
    public class StaticPage : BaseEntity
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; }
    }
}