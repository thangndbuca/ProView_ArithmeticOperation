/***********************************************************************
 * <copyright file="ArithmeticModel.cs" company="m365 JSC">
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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProView.Web.Areas.Home.Models
{
    /// <summary>
    ///     ArithmeticModel
    /// </summary>
    public class ArithmeticModel
    {
        /// <summary>
        /// Gets or sets the row identifier.
        /// </summary>
        /// <value>
        /// The row identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the arithmetic operation.
        /// </summary>
        /// <value>
        /// The arithmetic operation.
        /// </value>
        public string ArithmeticOperation { get; set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public int Result { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>
        /// The create date.
        /// </value>
        public DateTime CreateDate { get; set; }
    }

    /// <summary>
    ///     ArithmeticsModel
    /// </summary>
    public class ArithmeticsModel : PagedList<ArithmeticModel>
    {
        /// <summary>
        /// Gets or sets the sort.
        /// </summary>
        /// <value>
        /// The sort.
        /// </value>
        public string Sort { get; set; }

        /// <summary>
        /// Gets or sets the sort items.
        /// </summary>
        /// <value>
        /// The sort items.
        /// </value>
        public IDictionary<string, string> SortItems { get; set; }
    }
}