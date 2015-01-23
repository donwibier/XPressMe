using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XPressMe.Data.XPressMeDemoDB;
using XPressMe.Shared;

namespace XPressMe.Mvc.Models
{
	 public class PostDetailsModel : PostModel
	 {
		  public PostDetailsModel()
		  {

		  }
		  public PostDetailsModel(XPOPost model)
				: base(model)
		  {
				
		  }

		  public override bool IsCompact { get { return false; } }

		  public override void AssignFrom(XPOPost model)
		  {
				base.AssignFrom(model);

				Tags = from tag in model.Tags
						 orderby tag.Name
						 select tag.Name;

				Attachments = from att in model.PostAttachments
								  orderby att.AddStamp
								  select new PostAttachmentModel(att);
		  }
	 }
}
