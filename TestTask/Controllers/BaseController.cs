using DataAccess.Entity;
using DataAccess.Repository;
using TestTask.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using TestTask.Filters;

namespace TestTask.Controllers
{
   
    public abstract class BaseController<E, R, FilterVM, IndexVM, EditVM, OrderBy> : Controller
        where E : BaseEntity, new()
        where R : BaseRepository<E>, new()
        where IndexVM : BaseIndexVM<E, FilterVM, OrderBy>, new()
        where FilterVM : BaseFilterVM<E>, new()
        where EditVM : BaseEditVM<E>, new()
        where OrderBy : BaseOrderBy<E>, new()
    {
        protected virtual void PopulateModel(IndexVM model) { }
        protected virtual void PopulateEditGetModel(EditVM model) { }
        protected virtual void PopulateEditPostModel(EditVM model) { }
        protected virtual void PopulateRelatedEntities(IndexVM model) { }
        protected virtual void CheckEditedChildEntities(EditVM model, E item) { }
        protected virtual E GetItem(int id, R repo,string childEntity) { return  repo.GetById(id); }
        protected virtual void CheckModel(EditVM model, ref bool valid) { }
        protected virtual void CheckIfIsDeleted(E item) { }
        



        [HttpGet]  
        [AuthorizeAdmin]
        public virtual ActionResult Index(IndexVM model)
        {
            model.Pager = model.Pager ?? new PagerVM();
            model.Pager.Prefix = "Pager";
            model.Pager.Page = model.Pager.Page <= 0 ? 1 : model.Pager.Page;
            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0 ? 5 : model.Pager.ItemsPerPage;

            model.Filter = model.Filter ?? new FilterVM();
            model.Filter.Prefix = "Filter";
            PopulateModel(model);
            Expression<Func<E, bool>> filter = model.Filter.GenerateFilter();



            model.OrderBy = model.OrderBy ?? new OrderBy();
            Func<IQueryable<E>, IOrderedQueryable<E>> orderBy = model.OrderBy.orderBy();
            R repo = new R();
            model.Items = repo.GetAll(filter, model.Pager.Page, model.Pager.ItemsPerPage, orderBy,model.ChildElement);

            model.Pager.PagesCount = (int)Math.Ceiling(repo.Count(filter) / (double)(model.Pager.ItemsPerPage));

            return View(model);

        }

        [HttpGet]
        [AuthorizeAdmin]
        public virtual ActionResult Edit(EditVM model, FormCollection form)
        {
            ModelState.Clear();

            model = model ?? new EditVM();

            bool valid = true;

            if (model.Id > 0)
            {
                E item = null;
                R repo = new R();
                item = repo.GetById(model.Id);

                if (item == null)
                {
                    valid = false;
                }
                else
                {
                    model.PopulateModel(item);
                }
            }

            PopulateEditGetModel(model);

           if (!valid)
            {
                ModelState.AddModelError("", "The record you attempted to Edit was not found");
                return View(model);
            }

            return View(model);
        }

        [HttpPost]
        [AuthorizeAdmin]
        public virtual ActionResult Edit(EditVM model)
        {
            bool valid = true;
            CheckModel(model, ref valid);

            if (!ModelState.IsValid || !valid)
            {
                return View(model);
            }

            R repo = new R();
            E item = new E();

            if (model.Id > 0)
            {
                item = GetItem(model.Id,repo,null); 

                if (item == null)
                {
                    ModelState.AddModelError("", "The record you attempted to Edit was not found");
                    return View(model);
                }
            }

            PopulateEditPostModel(model);
            if (model.Id > 0)
                CheckEditedChildEntities(model, item);

            model.PopulateEntity(item);

            try
            {
                repo.Save(item);

            }
            catch (Exception)
            {
                PopulateEditGetModel(model);
                ModelState.AddModelError("", "Error !");
                return View(model);

            }

            return RedirectToAction("Index");
        }

        

        [HttpGet]
        [AuthorizeAdmin]
        public virtual ActionResult Delete(int id)
        {
            R repo = new R();
            E item = repo.GetById(id);

            if (item == null)
            {
                return RedirectToAction("Index");
            }

            CheckIfIsDeleted(item);
            repo.Delete(item);

            return RedirectToAction("Index");
        }

       
    }
}