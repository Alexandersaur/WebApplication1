using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Enums;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BlogUser> _userManager;

        public DataService(ApplicationDbContext dbContext,
                           RoleManager<IdentityRole> roleManager,
                           UserManager<BlogUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task ManageDataAsync()
        {
            await _dbContext.Database.MigrateAsync();
            await SeedRolesAsync();
            await SeedUsersAsync();
        }

        private async Task SeedRolesAsync()
        {
            if (_dbContext.Roles.Any())
            {
                return;
            }

            foreach (var role in Enum.GetNames(typeof(BlogRole)))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private async Task SeedUsersAsync()
        {
            if (_dbContext.Users.Any())
            {
                return;
            }

            var adminUser = new BlogUser()
            {
                Email = "Jeremy.a.steward@gmail.com",
                UserName = "Jeremy.a.steward@gmail.com",
                FirstName = "Jeremy",
                LastName = "Steward",
                DisplayName = "Blog Admin",
                EmailConfirmed = true,
                PhoneNumber = "(800) 555-5555"
            };

            await _userManager.CreateAsync(adminUser, "Abc&123!");
            await _userManager.AddToRoleAsync(adminUser, BlogRole.Administrator.ToString());

            var modUser = new BlogUser()
            {
                Email = "Gianni.r.esposito@gmail.com",
                UserName = "Gianni.r.esposito@gmail.com",
                FirstName = "Gianni",
                LastName = "Esposito",
                DisplayName = "Blog Mod",
                EmailConfirmed = true,
                PhoneNumber = "(800) 444-4444"
            };

            await _userManager.CreateAsync(modUser, "Abc&123!");
            await _userManager.AddToRoleAsync(modUser, BlogRole.Moderator.ToString());
        }
    }
}
