using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BudgetApplication.Models;

namespace BudgetApplication.Data
{ 
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Item> Items { get; set; }
    public virtual DbSet<Subcategory> Subcategories { get; set; }
    public virtual DbSet<Transaction> Transactions { get; set; }
}
}
