using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XPressMe.Data.XPressMeDemoDB;
using XPressMe.Shared.Mvc;

namespace XPressMe.Mvc.Models
{
	 public class BaseModel<TXPOClass> : XPOBaseDTOModelGuidKey<TXPOClass>
		  where TXPOClass : XPOBaseItem
	 {
		  public BaseModel()
		  {
				
		  }
		  public BaseModel(TXPOClass model)
				: base(model)
		  {
				
		  }
		  public DateTime AddStamp { get; set; }
		  public string AddIP { get; set; }
		  public string AddUser { get; set; }
		  public DateTime ModStamp { get; set; }
		  public string ModIP { get; set; }
		  public string ModUser { get; set; }

		  public override void AssignTo(TXPOClass model)
		  {
				if (model == null)
					 throw new ArgumentNullException("model");

				model.AddStamp = AddStamp;
				model.AddIP = AddIP;
				model.AddUser = AddUser;
				model.ModStamp = ModStamp;
				model.ModIP = ModIP;
				model.ModUser = ModUser;
		  }

		  public override void AssignFrom(TXPOClass model)
		  {
				if (model == null)
					 throw new ArgumentNullException("model");

				ID = model.ID;
				AddStamp = model.AddStamp;
				AddIP = model.AddIP;
				AddUser = model.AddUser;
				ModStamp = model.ModStamp;
				ModIP = model.ModIP;
				ModUser = model.ModUser;
		  }
	 }
}