/***********************************************************************
 * <copyright file="NewArithmeticModel.cs" company="m365 JSC">
 * -->    Copyright (C) statement. All right reserved
 * </copyright>
 * 
 * Created:   ThangND
 * Email:    thangnd@m365.vn
 * Website:
 * Create Date: 17 November 2015
 * Usage: 
 * 
 * RevisionHistory: 
 * Date         Author               Description 
 * 
 * ************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProView.Web.Areas.Home.Models
{
    /// <summary>
    ///  NewArithmeticModel
    /// </summary>
    public class NewArithmeticModel
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
        public double Result { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>
        /// The create date.
        /// </value>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the HTTP referer.
        /// </summary>
        /// <value>
        /// The HTTP referer.
        /// </value>
        public string HttpReferer { get; set; }

        public NewArithmeticModel()
        {
            // this prevents Automapper from blanking out default values in new User domain objects.
            CreateDate = DateTime.Now;
        }
    }
}