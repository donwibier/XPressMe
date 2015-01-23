using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XPressMe.Data.XPressMeDemoDB;
using DevExpress.Web.Mvc;
using XPressMe.Mvc.ViewModels;
using XPressMe.Mvc.Models;

namespace XPressMe.Mvc.Controllers
{
    public class HomeController : BaseController<XPOPost, HomeViewModel>
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View("Index", GetModel());
        }

		  override protected HomeViewModel GetModel()
		  {
				HomeViewModel result = new HomeViewModel()
				{
					 Groups = (from g in XPOSession.Query<XPOGroup>()
								  orderby g.Title
								  select new GroupModel(g)).ToList(),
					 Posts = (from p in XPOSession.Query<XPOPost>()
								 where p.Active == true
								 orderby p.AddStamp descending
								 select new PostModel(p)).ToList(),
					 Tags = (from t in XPOSession.Query<XPOTag>()
								select new TagModel(t)).ToList()
				};
				return result;
		  }

		  [ValidateInput(false)]
		  public ActionResult PostViewPartial()
		  {
				return PartialView("_PostViewPartial", GetModel());
		  }

		  [ValidateInput(false)]
		  public ActionResult DashboardPopup()
		  {
				return PartialView("_DashboardPartial", new PostDetailsModel());
		  }

		  [HttpPost, ValidateInput(false)]
		  public ActionResult DashboardCallback([ModelBinder(typeof(DevExpressEditorsBinder))] PostModel post)
		  {
				post.LookupTags = (from t in XPOSession.Query<XPOTag>()
										 select new TagModel(t)).ToList();

				List<GroupModel> groups = (from g in XPOSession.Query<XPOGroup>()
											orderby g.Title
											select new GroupModel(g)).ToList();
				groups.Insert(0, new GroupModel(){ ID=Guid.Empty, Title="--Select group--"});
				post.LookupGroups = groups;

				return PartialView("_DashboardPartial", post);
		  }

		  [HttpPost, ValidateInput(false)]
		  public ActionResult DashboardSubmit([ModelBinder(typeof(DevExpressEditorsBinder))] PostModel post) 
		  {
				if (ModelState.IsValid)
				{
					 try
					 {
						  DBInsert(post);
					 }
					 catch (Exception e)
					 {
						  ViewData["EditError"] = e.Message;
						  ViewData["CurrentItem"] = post;
					 }
					 
				}
				else
					 ViewData["EditError"] = "Please, correct all errors.";

			  return PartialView("_DashboardPartial", post);
		  }
	 }
}
