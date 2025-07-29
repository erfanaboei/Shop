using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace Shop.Domain.Models
{
    public interface IEntity
    {
        
    }

    public class BasePureEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
    }
    
    public class BasePureEntity<TKey> : IEntity
    {
        [Key]
        public TKey Id { get; set; }
    }
    
    public class BaseEntity : BasePureEntity
    {
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }

    public class BaseEntity<TKey> : BasePureEntity<TKey>
    {
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}