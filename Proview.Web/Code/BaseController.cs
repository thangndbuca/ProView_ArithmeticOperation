/***********************************************************************
 * <copyright file="BaseController.cs" company="m365 JSC">
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
using System.Web;
using System.Web.Mvc;

namespace ProView.Web
{
    /// <summary>
    ///  BaseController
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// Sets the success.
        /// </summary>
        /// <value>
        /// The success.
        /// </value>
        public string Success
        {
            set
            {
                TempData["Success"] = ViewData["Success"] = value;
            }
        }

        /// <summary>
        /// Sets the failure.
        /// </summary>
        /// <value>
        /// The failure.
        /// </value>
        public string Failure
        {
            set
            {
                TempData["Failure"] = ViewData["Failure"] = value;
            }
        }

        /// <summary>
        /// Called before the action method is invoked.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (TempData["Success"] != null) ViewData["Success"] = TempData["Success"];
            if (TempData["Failure"] != null) ViewData["Failure"] = TempData["Failure"];

            base.OnActionExecuting(filterContext);
        }
    }
}