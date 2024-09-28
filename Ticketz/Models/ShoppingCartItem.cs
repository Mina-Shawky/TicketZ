namespace Ticketz.Models;

public class ShoppingCartItem
{
    public int Id { get; set; }

    public virtual Movie Movie { get; set; }
    public int Amount { get; set; }


    public string ShoppingCartId { get; set; }
}
