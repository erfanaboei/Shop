using System;

namespace Shop.Application.Mappings
{
    public class MapperOption
    {
        public bool SetManualCreateDate { get; set; } = true;
        public DateTime Now { get; set; } = DateTime.Now;
    }
}