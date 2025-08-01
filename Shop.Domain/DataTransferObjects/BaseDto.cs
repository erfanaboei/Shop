using System;

namespace Shop.Domain.DataTransferObjects
{
    public interface IDto
    {
        
    }

    public class BasePureDto : IDto
    {
        public int Id { get; set; }
    }
    
    public class BasePureDto<TKey> : IDto
    {
        public TKey Id { get; set; }
    }
    
    public class BaseDto : BasePureDto
    {
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
    
    public class BaseDto<TKey> : BasePureDto<TKey>
    {
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}