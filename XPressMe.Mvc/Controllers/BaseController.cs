using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XPressMe.Mvc.ViewModels;
using XPressMe.Shared.Mvc;

namespace XPressMe.Mvc.Controllers
{
    public abstract class BaseController<TXPOClass, TViewModelClass> : XPOBaseGuidKeyController<TXPOClass>
		  where TXPOClass: XPBaseObject
		  where TViewModelClass : BaseViewModel
    {
		  protected abstract TViewModelClass GetModel();

		  [ValidateInput(false)]
		  public ActionResult NavContainerCallback(string cpCallbackCmd)
		  {
				string cmd = cpCallbackCmd;
				return PartialView("_NavigationPartial", GetModel().MenuItems);
		  }

		  //public abstract ActionResult DashboardPopup(Guid? id);
	 }
}
