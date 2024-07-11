using System.ComponentModel.DataAnnotations;

namespace Discount.Grpc.Models;

public class Coupon
{
    public int Id { get; set; }

    [MaxLength(50)]
    public string ProductName { get; set; } = null!;

    [MaxLength(3000)]
    public string Description { get; set; } = null!;

    public int Amount { get; set; }
}
