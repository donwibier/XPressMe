using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XPressMe.Data.XPressMeDemoDB;

namespace XPressMe.Mvc.Models
{
	 public class PostAttachmentModel:BaseModel<XPOPostAttachment>
	 {
		  public PostAttachmentModel()
		  {
				
		  }
		  public PostAttachmentModel(XPOPostAttachment model)
				: base(model)
		  {
				
		  }
		  public string FileName{get; set;}
		  public string MimeType{get; set;}
		  public byte[] Data{get; set;}
		  public Guid Post { get; set; }

		  public override void AssignFrom(XPOPostAttachment model)
		  {
				base.AssignFrom(model);
				FileName = model.FileName;
				MimeType = model.MimeType;
				Data = model.Data;
				Post = model.PostID;
		  }

		  public override void AssignTo(XPOPostAttachment model)
		  {
				base.AssignTo(model);
				model.FileName = FileName;
				model.MimeType = MimeType;
				model.Data = Data;
				model.Post = model.Session.GetObjectByKey<XPOPost>(Post);
		  }
	 }
}