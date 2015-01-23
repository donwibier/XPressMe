using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XPressMe.Data.XPressMeDemoDB;
using XPressMe.Shared;

namespace XPressMe.Mvc.Models
{
	 public class PostModel : BaseModel<XPOPost>
	 {
		  public PostModel()
		  {
						
		  }
		  public PostModel(XPOPost model)
				: base(model)
		  {
				
		  }
		  
		  public virtual bool IsCompact { get {return true; } }

		  public string Title { get; set; }
		  public bool Active { get; set; }
		  public string Article { get; set; }
		  public Guid Group { get; set; }
		  public string GroupTitle { get; set; }
		  public IEnumerable<string> Tags { get; set; }
		  public string AllTags { get; set; }
		 
		  public IEnumerable<PostAttachmentModel> Attachments { get; set; }

		  public IEnumerable<TagModel> LookupTags { get; set; }
		  public IEnumerable<GroupModel> LookupGroups { get; set; }


		  public override void AssignFrom(XPOPost model)
		  {
				base.AssignFrom(model);
				Title = model.Title;
				Active = model.Active;
				Article = model.Article;
				GroupTitle = model.GroupTitle;
				Group = model.GroupID;
		  }

		  public override void AssignTo(XPOPost model)
		  {
				base.AssignTo(model);
				model.Title = Title;
				model.Active = Active;
				model.Article = Article;
				model.Group = model.Session.GetObjectByKey<XPOGroup>(Group);
		  }
	 }
	 
}