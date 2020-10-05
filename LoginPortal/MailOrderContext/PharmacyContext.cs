using LoginPortal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace LoginPortal.MailOrderContext
{
    public class PharmacyContext:DbContext
    {
        public PharmacyContext(DbContextOptions<PharmacyContext> options) : base(options)
        {

        }
        public DbSet<Drug> DrugDetails { get; set; }
        public DbSet<Refill> RefillDetails { get; set; }
    }
}
