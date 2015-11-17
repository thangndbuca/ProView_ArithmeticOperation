/***********************************************************************
 * <copyright file="ArithmeticController.cs" company="m365 JSC">
 * -->    Copyright (C) statement. All right reserved
 * </copyright>
 * 
 * Created:   ThangND
 * Email:    thangnd@m365.vn
 * Website:
 * Create Date: 16 November 2015
 * Usage: 
 * 
 * RevisionHistory: 
 * Date         Author               Description 
 * 
 * ************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using ProView.Domain;
using ProView.Web.Areas.Home.Models;

namespace ProView.Web.Areas.Home.Controllers
{
    /// <summary>
    ///   ArithmeticController
    /// </summary>
    public class ArithmeticController : ApiController
    {
        static Dictionary<string, string> UserSortItems { get; set; }

        /// <summary>
        /// Initializes the <see cref="ArithmeticController"/> class.
        /// </summary>
        static ArithmeticController()
        {
            UserSortItems = new Dictionary<string, string>();
            UserSortItems.Add("create_date_asc", "Create Date");
            UserSortItems.Add("create_date_desc", "Create Date");

            Mapper.CreateMap<Arithmetic, ArithmeticModel>();

            Mapper.CreateMap<Arithmetic, ArithmeticsModel>();
        }

        /// <summary>
        /// Arithmeticses the specified sort.
        /// </summary>
        /// <param name="sort">The sort.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ArithmeticModel> Arithmetics(string sort = "create_date_asc", int page = 1, int pageSize = 10)
        {
            ValidateUsersArgs(sort, page, pageSize);

            var model = new ArithmeticsModel { Sort = sort, Page = page, PageSize = pageSize, SortItems = UserSortItems };
            var arithmetics = ProViewContext.Arithmetics.Paged(out model.TotalRows, where: "RowId <> @0", orderBy: sort.Replace("_", " "), page: page, pageSize: pageSize);
            model.Items = Mapper.Map<IEnumerable<Arithmetic>, IEnumerable<ArithmeticModel>>(arithmetics);

            return model.Items;
        }

        #region Private Helpers

        /// <summary>
        /// Validates the users arguments.
        /// </summary>
        /// <param name="sort">The sort.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <exception cref="System.ArgumentException">
        /// Invalid Sort
        /// or
        /// Invalid Page
        /// or
        /// Invalid PageSize
        /// </exception>
        void ValidateUsersArgs(string sort, int page, int pageSize)
        {
            if (!UserSortItems.ContainsKey(sort)) throw new ArgumentException("Invalid Sort");
            if (page < 1) throw new ArgumentException("Invalid Page");
            if (pageSize < 1) throw new ArgumentException("Invalid PageSize");
        }

        #endregion
    }
}
