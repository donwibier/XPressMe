using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XPressMe.Data.XPressMeDemoDB;

namespace XPressMe.Mvc.Models
{
	 public class TagModel : BaseModel<XPOTag>
	 {
		  public TagModel()
		  {				
		  }
		  public TagModel(XPOTag model)
				: base(model)
		  {				
		  }

		  public string Name { get; set; }
		  public int PostCount { get; set; }

		  public override void AssignFrom(XPOTag model)
		  {
				base.AssignFrom(model);
				Name = model.Name;
				PostCount = model.PostCount;
		  }

		  public override void AssignTo(XPOTag model)
		  {
				base.AssignTo(model);
				model.Name = Name;
		  }
	 }
}