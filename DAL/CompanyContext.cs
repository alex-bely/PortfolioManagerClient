using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DAL
{
    public class CompanyContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
    }
}
