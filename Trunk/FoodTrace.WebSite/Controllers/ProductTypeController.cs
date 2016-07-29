﻿using FoodTrace.IService;
using FoodTrace.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodTrace.WebSite.Controllers
{
    public class ProductTypeController : BaseController
    {
        IProductTypeService productTypeService;
        public ProductTypeController(IProductTypeService pTypeService)
        {
            productTypeService = pTypeService;
        }

        // GET: ProductType
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetList(int page, int rows)
        {
            var productSpecList = productTypeService.GetPagerProductType(string.Empty, page, rows).Select(m => new
            {
                ProductTypeID = m.ProductTypeID,
                ProductTypeEN = m.ProductTypeEN,
                Remark = m.Remark,
                IsLocked = m.IsLocked,
                IsShow = m.IsShow,
            });
            return Json(productSpecList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return PartialView(new ProductTypeModel());
        }

        [HttpPost]
        public ActionResult Create(ProductTypeModel productTypeModel)
        {
            var result = productTypeService.InsertSingleProductType(productTypeModel);
            var flag = result.Status == MessageStatus.Success ? true : false;
            var msg = result.Message;
            return Json(new { flag = flag, msg = msg });
        }

        public ActionResult Edit(int id)
        {
            var productType = productTypeService.GetProductTypeById(id);
            return PartialView(productType);
        }

        [HttpPost]
        public ActionResult Edit(ProductTypeModel productTypeModel)
        {
            var result = productTypeService.UpdateSingleProductType(productTypeModel);
            var flag = result.Status == MessageStatus.Success ? true : false;
            var msg = result.Message;
            return Json(new { flag = flag, msg = msg });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var result = productTypeService.DeleteSingleProductType(id);
            var flag = result.Status == MessageStatus.Success ? true : false;
            var msg = result.Message;
            return Json(new { flag = flag, msg = msg });
        }
    }
}