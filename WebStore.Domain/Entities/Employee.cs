using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebStore.Domain.Entities;
[Index(nameof(LastName), IsUnique = true)]
public class Employee : NamedEntity, INamedEntity
{
    //public int Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Patronymic { get; set; }
    public int Age { get; set; }
    public DateTime EmploymentDate { get; set; }
}

