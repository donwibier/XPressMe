using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XPressMe.Data.XPressMeDemoDB;

namespace XPressMe.Mvc.Models
{
	 public class GroupModel: BaseModel<XPOGroup>
	 {
		  public GroupModel()
		  {
				
		  }

		  public GroupModel(XPOGroup model)
				: base(model)
		  {
				Title = model.Title;
		  }		  
		  
		  public string Title { get; set; }


		  public override void AssignFrom(XPOGroup model)
		  {
				base.AssignFrom(model);
				Title = model.Title;
		  }
		  public override void AssignTo(XPOGroup model)
		  {
				base.AssignTo(model);
				model.Title = Title;
		  }
	 }
}