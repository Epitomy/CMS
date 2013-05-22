﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kooboo.CMS.Sites.Services;
using Kooboo.Web.Mvc.Paging;
using Kooboo.Globalization;
using System.Web.Routing;
using Kooboo.Web.Mvc;
using Kooboo.Extensions;
using Kooboo.CMS.Web.Models;
using Kooboo.CMS.Sites;
using Kooboo.Web;
namespace Kooboo.CMS.Web.Areas.Sites.Controllers
{
    [Kooboo.CMS.Web.Authorizations.Authorization(AreaName = "Sites", Group = "Development", Name = "Label", Order = 1)]
    public class LabelController : AdminControllerBase
    {
        #region Manager
        private LabelManager manager = new LabelManager();
        public LabelManager Manager
        {
            get
            {
                return manager;
            }
            set { this.manager = value; }
        }

        #endregion

        #region Index,Create,Edit
        public virtual ActionResult Index(string category, string search, string culture, int? page, int? pageSize)
        {
            var queryable = Manager.GetLabels(Site, category);
            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim();
                queryable = queryable.Where(it => it.Name.Contains(search, StringComparison.CurrentCultureIgnoreCase) || it.Value.Contains(search, StringComparison.CurrentCultureIgnoreCase));
            }
            if (!string.IsNullOrEmpty(culture))
            {
                queryable = queryable.Where(it => it.Culture.Equals(culture, StringComparison.CurrentCultureIgnoreCase));
            }
            var pagedList = queryable.OrderBy(it => it.Name).ToPagedList<Element>(page ?? 1, pageSize ?? 50);

            if (string.IsNullOrEmpty(category))
            {
                ViewData["CategoryList"] = Manager.GetCategories(Site);
            }

            return View(pagedList);
        }


        public virtual ActionResult Create(Element model)
        {
            ModelState.Clear();
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Create(Element model, FormCollection forms)
        {
            try
            {
                Add(model);
                //return RedirectToIndex();
            }
            catch (FriendlyException e)
            {
                ModelState.AddModelError("", e.Message);

            }
            return View(model);
        }
        protected virtual void Add(Element model)
        {
            Manager.Add(Site, model);
        }

        public virtual ActionResult Edit(string category, string name, string culture)
        {
            var o = Get(category, name, culture);
            return View(o);
        }
        protected virtual Element Get(string category, string name, string culture)
        {
            return Manager.Get(Site, category, name, culture);
        }


        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult Edit(Element model, FormCollection collection)
        {
            JsonResultEntry resultEntry = new JsonResultEntry(ModelState);
            try
            {
                //if (ModelState.IsValid)
                //{
                Update(model);
                //}                
                //return RedirectToIndex();
            }
            catch (Exception e)
            {
                resultEntry.AddException(e);
            }
            return Json(resultEntry);
        }
        protected virtual void Update(Element model)
        {
            Manager.Update(Site, model);
        }

        #endregion

        #region Translate,Delete,Category
        public virtual ActionResult Translate(string category, string name, string culture)
        {
            return Edit(category, name, culture);
        }
        [HttpPost]
        public virtual ActionResult Translate(Element model)
        {
            return Create(model);
        }

        public virtual ActionResult Delete(Element[] model, ElementCategory[] categoryList)
        {
            JsonResultEntry entry = new JsonResultEntry();

            try
            {
                var category = ControllerContext.RequestContext.GetRequestValue("Category");

                if (model != null)
                {
                    foreach (var m in model)
                    {
                        m.Category = category;
                        Remove(m);
                    }
                }


                if (categoryList != null)
                {
                    foreach (var c in categoryList)
                    {
                        Manager.RemoveCategory(Site, c.Category);
                    }
                }
            }
            catch (Exception e)
            {
                entry.AddException(e);
            }

            return Json(entry);
        }
        protected virtual void Remove(Element model)
        {
            Manager.Remove(Site, model);
        }


        public virtual ActionResult RemoveCategory(string category)
        {
            Manager.RemoveCategory(Site, category);
            if (!string.IsNullOrEmpty(category))
            {
                var index = category.LastIndexOf('.');
                if (index != -1)
                {
                    return RedirectToIndex(category.Substring(0, index));
                }
            }
            return RedirectToIndex("");
        }
        protected ActionResult RedirectToIndex()
        {
            return RedirectToIndex("");
        }
        protected virtual ActionResult RedirectToIndex(string category)
        {
            var routeValues = this.ControllerContext.RequestContext.AllRouteValues();
            if (!string.IsNullOrEmpty(category))
            {
                routeValues["category"] = category;
            }
            return RedirectToAction("Index", routeValues);
        }
        #endregion

        #region Import/Export
        public void Export()
        {
            //var fullNameArray = model.Select(o => o.FullName);
            //var selected = Manager.All(Site, "").Where(o => fullNameArray.Contains(o.FullName));

            var fileName = GetZipFileName();
            Response.AttachmentHeader(fileName);

            Manager.Export(Site, Response.OutputStream);
        }

        protected string GetZipFileName()
        {
            return "Labels.zip";
        }



        public virtual ActionResult Import(string fullName, bool @override)
        {
            JsonResultEntry resultEntry = new JsonResultEntry(ModelState);
            try
            {
                if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                {
                    Manager.Import(Site, Request.Files[0].InputStream);
                }
            }
            catch (Exception e)
            {
                resultEntry.AddException(e);
            }
            return Json(resultEntry, "text/plain", System.Text.Encoding.UTF8);
        }

        #endregion

    }
}
