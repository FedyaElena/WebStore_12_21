using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace WebStore.Domain.Entities;
//[Table("Brandssss")]
[Index(nameof(Name), IsUnique = true)]
public class Brand : NamedEntity, IOrderedEntity
{
    public int Order { get; set; }

    public ICollection<Product> Products { get; set; }
}




