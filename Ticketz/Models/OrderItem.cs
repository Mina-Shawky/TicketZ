using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ticketz.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public double Price { get; set; }
    public int MovieId { get; set; }
    public virtual Movie Movie { get; set; }
    public int OrderId { get; set; }
    public virtual Order Order { get; set; }
}
