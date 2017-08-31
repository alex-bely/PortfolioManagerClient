using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// Works with portfolio database
    /// </summary>
    public class PortfolioRepository 
    {
        CompanyContext db;
        /// <summary>
        /// Creates the repository
        /// </summary>
        public PortfolioRepository()
        {
            db = new CompanyContext();
        }

        /// <summary>
        /// Gets all Company items from database
        /// </summary>
        /// <returns>The list of Company items</returns>
        public IList<Company> GetAll()
        {
            return db.Companies.ToList();
        }

        /// <summary>
        /// Gets one portfolio item by name
        /// </summary>
        /// <param name="name">Company name</param>
        /// <returns>Company item with certain name</returns>
        public Company GetCompany(string name)
        {
            return db.Companies.FirstOrDefault(p => p.CompanyName == name);
        }

        /// <summary>
        /// Adds Company item to database
        /// </summary>
        /// <param name="item">Company item</param>
        /// <returns>True if the item was added, false otherwise</returns>
        public bool AddCompany(Company item)
        {
            
            if(!db.Companies.Any<Company>(c=>c.CompanyName==item.CompanyName))
            {
                db.Companies.Add(item);
                return true;
            }
            return false; 
        }

        /// <summary>
        /// Updates Company item in database
        /// </summary>
        /// <param name="item">Company item with updated values</param>
        /// <returns>True if item was updated, false otherwise</returns>
        public bool UpdateCompany(Company item)
        {
            var element = db.Companies.Where(s => s.CompanyId == item.CompanyId).FirstOrDefault<Company>();
            if (element != null)
            {
                element.CompanyName = item.CompanyName;
                element.Amount = item.Amount;
                db.Entry(element).State = System.Data.Entity.EntityState.Modified;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deletes the Company item from database.
        /// </summary>
        /// <param name="id">The Company item Id to delete</param>
        /// <returns>True if item was deleted, false otherwise</returns>
        public bool Delete(int id)
        {
            var element = db.Companies.Where(s => s.CompanyId == id).FirstOrDefault<Company>();
            if(element != null)
            {
                db.Entry(element).State = System.Data.Entity.EntityState.Deleted;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes all items from database
        /// </summary>
        public void RemoveAll()
        {
            using (var ctx = new CompanyContext())
            {
                IQueryable<Company> allItems = ctx.Companies;
                ctx.Companies.RemoveRange(allItems);
            }
        }

        /// <summary>
        /// Applies changes to database
        /// </summary>
        public void Save()
        {
            db.SaveChanges();
            
        }
    }

   
    
}
