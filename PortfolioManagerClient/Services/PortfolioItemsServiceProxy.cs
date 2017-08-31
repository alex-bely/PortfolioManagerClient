using PortfolioManagerClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PortfolioManagerClient.Services
{
    /// <summary>
    /// Proxy for PortfolioItemsService
    /// </summary>
    public sealed class PortfolioItemsServiceProxy
    {
        private PortfolioItemRepositoryService serv;
        private PortfolioItemsService _portfolioItemsService;
        /// <summary>
        /// Keeps the Id of last item from the remote server
        /// </summary>
        private static int lastId;

        /// <summary>
        /// Creates the Proxy
        /// </summary>
        public PortfolioItemsServiceProxy()
        {
            _portfolioItemsService = new PortfolioItemsService();
            serv = new PortfolioItemRepositoryService();
        }

        /// <summary>
        /// Gets all portfolio items for the user.
        /// </summary>
        /// <param name="userId">The User Id.</param>
        /// <returns>The list of portfolio items</returns>
        public IList<PortfolioItemViewModel> GetItems(int userId)
        {
            var allItems = serv.GetAll();
            if(allItems.Count==0)
            {
                Synchronize(userId);
                allItems = serv.GetAll();
                lastId = allItems.Last().ItemId;
            }
            lastId = allItems.Last().ItemId;
            return allItems;
        }

        /// <summary>
        /// Creates a portfolio item. UserId is taken from the model.
        /// </summary>
        /// <param name="item">The portfolio item to create</param>
        /// <returns>Type: System.Threading.Tasks.Task</returns>
        public async Task CreateItemAsync(PortfolioItemViewModel item)
        {
            if (serv.GetCompany(item.Symbol) != null)
                return;
            item.ItemId = ++lastId;
            serv.AddCompany(item);
            serv.Commit();
            await Task.Run(() => _portfolioItemsService.CreateItem(item));
        }

        /// <summary>
        /// Updates a portfolio item.
        /// </summary>
        /// <param name="item">The portfolio item to update</param>
        /// <returns>Type: System.Threading.Tasks.Task</returns>
        public async Task UpdateItemAsync(PortfolioItemViewModel item)
        {
            serv.UpdateCompany(item);
            serv.Commit();
            await Task.Run(() => _portfolioItemsService.UpdateItem(item));
        }

        /// <summary>
        /// Deletes a portfolio item.
        /// </summary>
        /// <param name="id">The portfolio item Id to delete.</param>
        /// <returns>Type: System.Threading.Tasks.Task</returns>
        public async Task DeleteItemAsync(int id)
        {
            serv.Delete(id);
            serv.Commit();
            await Task.Run(() => _portfolioItemsService.DeleteItem(id));
        }

        /// <summary>
        /// Synchronizes the Portfolio service with remote source
        /// </summary>
        /// <param name="userId">The User Id.</param>
        public void Synchronize(int userId)
        {
            serv.RemoveAll();
            var temp = _portfolioItemsService.GetItems(userId).ToList();
            foreach (var elem in temp)
            {
                serv.AddCompany(elem);
            }
            serv.Commit();
        }


    }
}