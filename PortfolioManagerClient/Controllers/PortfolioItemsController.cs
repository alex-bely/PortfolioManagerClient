﻿using System.Collections.Generic;
using System.Web.Http;
using PortfolioManagerClient.Models;
using PortfolioManagerClient.Services;

namespace PortfolioManagerClient.Controllers
{
    /// <summary>
    /// Processes portfolio item requests.
    /// </summary>
    public class PortfolioItemsController : ApiController
    {
        private readonly PortfolioItemsService _portfolioItemsService = new PortfolioItemsService();
        private readonly UsersService _usersService = new UsersService();
        private readonly PortfolioItemsServiceProxy _serviceProxy = new PortfolioItemsServiceProxy();

        /// <summary>
        /// Returns all portfolio items for the current user.
        /// </summary>
        /// <returns>The list of portfolio items.</returns>
        public IList<PortfolioItemViewModel> Get()
        {
            var userId = _usersService.GetOrCreateUser();
            return _serviceProxy.GetItems(userId);
        }

        /// <summary>
        /// Updates the existing portfolio item.
        /// </summary>
        /// <param name="portfolioItem">The portfolio item to update.</param>
        public void Put(PortfolioItemViewModel portfolioItem)
        {
            portfolioItem.UserId = _usersService.GetOrCreateUser();
            _serviceProxy.UpdateItemAsync(portfolioItem);
        }

        /// <summary>
        /// Deletes the specified portfolio item.
        /// </summary>
        /// <param name="id">The portfolio item identifier.</param>
        public void Delete(int id)
        {
            _serviceProxy.DeleteItemAsync(id);
        }

        /// <summary>
        /// Creates a new portfolio item.
        /// </summary>
        /// <param name="portfolioItem">The portfolio item to create.</param>
        public void Post(PortfolioItemViewModel portfolioItem)
        {
            portfolioItem.UserId = _usersService.GetOrCreateUser();
            _serviceProxy.CreateItemAsync(portfolioItem);
        }
    }
}
