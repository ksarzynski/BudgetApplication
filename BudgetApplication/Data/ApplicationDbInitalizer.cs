using System.Linq;
using BudgetApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BudgetApplication.Data;

namespace BudgetApplication.Data
{
    public class ApplicationDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationDbInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void Seed()
        {
           
              _context.Database.Migrate();


            if (!_context.Roles.Any())
            {
                var roleNames = new[]
                {
                    Roles.Roles.User,

                };

                foreach (var roleName in roleNames)
                {
                    var role = new IdentityRole(roleName) { NormalizedName = roleName.ToUpper() };
                    _context.Roles.Add(role);
                }
            }


            if (!_context.ApplicationUsers.Any())
            {
                const string userName = "user@user.com";                                   
                const string userPass = "p@sw1ooorD";

                var user = new ApplicationUser { UserName = userName, Email = userName };
                _userManager.CreateAsync(user, userPass).Wait();
                _userManager.AddToRoleAsync(user, Roles.Roles.User).Wait();
                             

            }

            _context.SaveChanges();
        }

    }

}