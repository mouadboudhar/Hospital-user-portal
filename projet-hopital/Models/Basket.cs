using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projet_hopital.Models;

public class Basket
{
    public int Id { get; set; }
    
    [Required]
    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUser? User { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public virtual ICollection<BasketItem> Items { get; set; } = new List<BasketItem>();
    
    [NotMapped]
    public decimal TotalPrice => Items.Sum(i => i.SubTotal);
    
    [NotMapped]
    public int TotalItems => Items.Count;
}

