using System.ComponentModel.DataAnnotations;

namespace Shop.Domain.Enums.Orders
{
    public enum OrderStatusEnum
    {
        [Display(Name = "درحال پردازش")] Processing = 1,
        [Display(Name = "ارسال شده")] Sent = 2,
        [Display(Name = "تحویل شده")] Delivered = 3,
    }
}