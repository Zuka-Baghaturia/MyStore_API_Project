using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication_MyStore_API_Project.Data;

public class AuthDbContext : IdentityDbContext<IdentityUser>
{
    //public AuthDbContext(DbContextOptions options) : base(options)
    //{
    //}


    public AuthDbContext(DbContextOptions <AuthDbContext> options) : base(options)
    {
    } 

}
