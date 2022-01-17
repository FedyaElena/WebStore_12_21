﻿using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace WebStore.Domain.Entities.Identity;

public class User : IdentityUser
{
    public const string Administrator = "Admin";
    public const string DefaultAdminPassword = "AdPAss_123";
   
}
