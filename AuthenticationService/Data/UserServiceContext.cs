using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AuthenticationService.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AuthenticationService.Data
{
    public class UserServiceContext : IdentityDbContext<UserModel>
    {
        public UserServiceContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Year> Years { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Group> Groups { get; set; }
    }
}
