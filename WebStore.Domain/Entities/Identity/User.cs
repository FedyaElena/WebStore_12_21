using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace WebStore.Domain.Entities.Identity;

public class User : IdentityUser
{
   public string AboutMySelf { get; set; }
}
