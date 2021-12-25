using WebStore.Domain.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebStore.Domain.Entities.Base;

public abstract class NamedEntity : Entity, INamedEntity
{
    [Required]
    public string Name { get; set; }
}


