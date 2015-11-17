/***********************************************************************
 * <copyright file="HomeController.cs" company="m365 JSC">
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
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ProView.Domain;
using ProView.Web.Areas.Home.Models;
using ProView.Web.Code;

namespace ProView.Web.Areas.Home.Controllers
{
    /// <summary>
    ///     HomeController
    /// </summary>
    public class HomeController : BaseController
    {
        /// <summary>
        /// Gets or sets the arithmetic sort items.
        /// </summary>
        /// <value>
        /// The arithmetic sort items.
        /// </value>
        static Dictionary<string, string> ArithmeticSortItems { get; set; }

        /// <summary>
        /// Initializes the <see cref="HomeController"/> class.
        /// </summary>
        static HomeController()
        {
            ArithmeticSortItems = new Dictionary<string, string>();
            ArithmeticSortItems.Add("CreateDate_ASC", "Create Date ASC");
            ArithmeticSortItems.Add("CreateDate_DESC", "Create Date DESC");

            Mapper.CreateMap<Arithmetic, ArithmeticModel>();
            Mapper.CreateMap<Arithmetic, ArithmeticsModel>();
            Mapper.CreateMap<NewArithmeticModel, Arithmetic>();
        }

        [HttpGet]
        [ActionName("Arithmetic")]
        public ActionResult GetArithmetic(int? id = null)
        {
            var model = new NewArithmeticModel();

            if (id != null)
            {
                var arithmetic = ProViewContext.Arithmetics.Single(id);
                model = Mapper.Map<Arithmetic, NewArithmeticModel>(arithmetic);
            }

            model.HttpReferer = Request.UrlReferrer.PathAndQuery;
            return View("Arithmetic", model);
        }

        [HttpPost]
        [ActionName("Arithmetic")]
        public ActionResult PostArithmetic(NewArithmeticModel model)
        {
            // ** Prototype pattern. the arithmetic object which has its default values set

            var arithmetic = Mapper.Map<NewArithmeticModel, Arithmetic>(model, new Arithmetic(true));

            if (arithmetic.Id != 0)  // existing arithmetic
            {
                if (ModelState.IsValid)
                {

                    var dt = new DataTable();
                    arithmetic.Result = (int)dt.Compute(model.ArithmeticOperation, "");

                    if (!arithmetic.IsValid) // shows how custom validation would be used
                    {
                        // examine arithmetic.Errors list
                    }
                    ProViewContext.Arithmetics.Update(arithmetic);
                    Success = "Biểu thức " + arithmetic.ArithmeticOperation + " được cập nhật thành công.";

                    return RedirectToAction("Arithmetics");
                }
            }
            else // new arithmetic
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var dt = new DataTable();
                        arithmetic.Result = (double)dt.Compute(model.ArithmeticOperation, "");
                        // ** Facade pattern. Unit of Work pattern.
                        ProViewContext.Arithmetics.Insert(arithmetic);

                        Success = "Biểu thức " + arithmetic.ArithmeticOperation + " được thêm mới thành công.";

                        return RedirectToAction("Arithmetics");
                    }
                    catch (Exception ex)
                    {
                        Failure = ex.Message;
                    }
                }
            }

            return View(model);
        }

        [HttpDelete]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        [ActionName("Arithmetic")]
        public void DeleteUser(int? id)
        {
            // ** CQRS Pattern. App does not wait return values: it just assumes it works.
            var arithmetic = ProViewContext.Arithmetics.Single(id);

            // ** Facade pattern and Unit of Work pattern.
            ProViewContext.Arithmetics.Delete(arithmetic);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string sort = "CreateDate_ASC", int page = 1, int pageSize = 10, bool layout = true)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Arithmetics(string sort = "CreateDate_ASC", int page = 1, int pageSize = 10, bool layout = true)
        {
            ValidateArithmeticsArgs(sort, page, pageSize);

            var model = new ArithmeticsModel { Sort = sort, Page = page, PageSize = pageSize, SortItems = ArithmeticSortItems };
            var arithmetics = ProViewContext.Arithmetics.Paged(out model.TotalRows, where: "", orderBy: sort.Replace("_", " "), page: page, pageSize: pageSize);
            model.Items = Mapper.Map<IEnumerable<Arithmetic>, IEnumerable<ArithmeticModel>>(arithmetics);

            // exclude layout when browser history is recalled
            if (!layout && Request.IsAjaxRequest())
            {
                ViewBag.Layout = "No";  // return page without layout. 
                return View(model);
            }

            if (Request.IsAjaxRequest())
                return PartialView("_Arithmetics", model);
            else
                return View(model);
        }

        /// <summary>
        /// Abouts this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// Errors this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Error()
        {
            return View();
        }

        #region Api Json

        /// <summary>
        /// Gets the arithmetics.
        /// </summary>
        /// <param name="sort">The sort.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetArithmetics(string sort = "CreateDate_ASC", int page = 1, int pageSize = 10)
        {
            ValidateArithmeticsArgs(sort, page, pageSize);

            var model = new ArithmeticsModel { Sort = sort, Page = page, PageSize = pageSize, SortItems = ArithmeticSortItems };
            var arithmetics = ProViewContext.Arithmetics.Paged(out model.TotalRows, where: "", orderBy: sort.Replace("_", " "), page: page, pageSize: pageSize);
            model.Items = Mapper.Map<IEnumerable<Arithmetic>, IEnumerable<ArithmeticModel>>(arithmetics);
            return this.Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Validates the arithmetics arguments.
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
        void ValidateArithmeticsArgs(string sort, int page, int pageSize)
        {
            if (!ArithmeticSortItems.ContainsKey(sort)) throw new ArgumentException("Invalid Sort");
            if (page < 1) throw new ArgumentException("Invalid Page");
            if (pageSize < 1) throw new ArgumentException("Invalid PageSize");
        }

        #endregion
    }
}