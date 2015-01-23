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
	 public class PostController : BaseController<XPOPost, PostViewModel>
    {
		  override protected PostViewModel GetModel()
		  {
				PostViewModel result = new PostViewModel()
				{
					 ID = _PostID,
					 Groups = (from g in XPOSession.Query<XPOGroup>()
								  orderby g.Title
								  select new GroupModel(g)).ToList(),
					 Posts = (from p in XPOSession.Query<XPOPost>()
								 where (p.ID == _PostID) && (p.Active == true)								 
								 select new PostDetailsModel(p)).ToList(),
					 Tags = (from t in XPOSession.Query<XPOTag>()
								select new TagModel(t)).ToList()
				};
				return result;
		  }

        //
        // GET: /Post/

        public ActionResult Index()
        {
				return View();
        }


		  private Guid _PostID = Guid.Empty;
        //
        // GET: /Post/Details/5

        public ActionResult Details(Guid id)
        {
				_PostID = id;
				ViewData["PostID"] = id;
				return View("Index", GetModel());
        }
		  
		  [ValidateInput(false)]
		  public ActionResult PostViewPartial(Guid id)
		  {
				_PostID = id;
				
				return PartialView("_PostViewPartial", GetModel());
		  }

		  [ValidateInput(false)]
		  public ActionResult DashboardPopup(Guid id)
		  {
				_PostID = id;
				var m = GetModel().Posts.First();
				return PartialView("_DashboardPartial", m);
		  }

		  [HttpPost, ValidateInput(false)]
		  public ActionResult DashboardCallback([ModelBinder(typeof(DevExpressEditorsBinder))] PostModel post)
		  {
				if (post.ID != Guid.Empty)
				{
					 Guid id = post.ID;
					 post = (from p in XPOSession.Query<XPOPost>()
								where p.ID == id
								select new PostDetailsModel(p)).FirstOrDefault();

				}
				post.LookupTags = (from t in XPOSession.Query<XPOTag>()
										 select new TagModel(t)).ToList();
				post.LookupGroups = (from g in XPOSession.Query<XPOGroup>()
											orderby g.Title
											select new GroupModel(g)).ToList();


				return PartialView("_DashboardPartial", post);
		  }

		  [HttpPost, ValidateInput(false)]
		  public ActionResult DashboardSubmit([ModelBinder(typeof(DevExpressEditorsBinder))] PostModel post)
		  {
				if (ModelState.IsValid)
				{
					 try
					 {
						  DBUpdate(post);
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
