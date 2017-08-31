using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{   
    /// <summary>
    /// Represents Company entity in portfolio
    /// </summary>
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int Amount { get; set; }
        public int ServerId { get; set; }
    }
}
