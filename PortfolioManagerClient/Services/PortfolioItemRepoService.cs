using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using PortfolioManagerClient.Models;

namespace PortfolioManagerClient.Services
{
    /// <summary>
    /// Works with Portfolio repository
    /// </summary>
    public class PortfolioItemRepositoryService
    {
        private PortfolioRepository companyRepo;

        /// <summary>
        /// Creates the service
        /// </summary>
        public PortfolioItemRepositoryService()
        {
            companyRepo= new PortfolioRepository();
        }

        /// <summary>
        /// Gets all portfolio items from repository
        /// </summary>
        /// <returns>The list of portfolio items</returns>
        public IList<PortfolioItemViewModel> GetAll()
        {
            var temp= companyRepo.GetAll().Select(el=>new PortfolioItemViewModel() { ItemId = el.CompanyId, Symbol = el.CompanyName, SharesNumber = el.Amount,UserId=el.ServerId }).ToList();
            return temp;
        }

        /// <summary>
        /// Gets portfolio items of certain user from repository
        /// </summary>
        /// <param name="id">Id of the User</param>
        /// <returns>The list of portfolio items</returns>
        public IList<PortfolioItemViewModel> GetByUser(int id)
        {
            var temp = companyRepo.GetByUser(id).Select(el => new PortfolioItemViewModel() { ItemId = el.CompanyId, Symbol = el.CompanyName, SharesNumber = el.Amount, UserId = el.ServerId }).ToList();
            return temp;
        }

        /// <summary>
        ///  Gets one portfolio item by name
        /// </summary>
        /// <param name="name">Company name</param>
        /// <returns>Portfolio item with certain name</returns>
        public PortfolioItemViewModel GetCompany(string name, int id)
        {
            var temp = companyRepo.GetCompany(name, id);
            if(temp!=null)
                return new PortfolioItemViewModel() { ItemId = temp.CompanyId, Symbol = temp.CompanyName, SharesNumber = temp.Amount, UserId=temp.ServerId };
            return null;
        }

        /// <summary>
        ///  Adds Portfolio item to repository
        /// </summary>
        /// <param name="item">Portfolio item</param>
        public void AddCompany(PortfolioItemViewModel item)
        {
            companyRepo.AddCompany(new Company() { CompanyId = item.ItemId, CompanyName = item.Symbol, Amount = item.SharesNumber, ServerId = item.UserId });
        }

        /// <summary>
        /// Updates Portfolio item in repository
        /// </summary>
        /// <param name="item">Portfolio item with updated values</param>
        public void UpdateCompany(PortfolioItemViewModel item)
        {
            companyRepo.UpdateCompany(new Company() { CompanyId = item.ItemId, CompanyName = item.Symbol, Amount = item.SharesNumber, ServerId=item.UserId });
        }

        /// <summary>
        /// Deletes a Portfolio item from repository.
        /// </summary>
        /// <param name="id">The portfolio item Id to delete</param>
        public void Delete(int id)
        {
            companyRepo.Delete(id);
        }

        /// <summary>
        /// Applies changes to repository
        /// </summary>
        public void Commit()
        {
            companyRepo.Save();
        }

        /// <summary>
        /// Removes all items from repository
        /// </summary>
        public void RemoveAll()
        {
            companyRepo.RemoveAll();
        }


    }
}