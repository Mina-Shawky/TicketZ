using System.ComponentModel.DataAnnotations.Schema;

namespace Ticketz.Models;

public class Order
{
    public int Id { get; set; }
    public string Email { get; set; }
    [ForeignKey("UserId")]
    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }

    public virtual List<OrderItem> OrderItems { get; set; }
}
