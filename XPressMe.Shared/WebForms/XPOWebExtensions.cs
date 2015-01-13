using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Helpers;
using DevExpress.Xpo.Metadata;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace XPressMe.Shared.WebForms
{
	 public static class XPOWebExtensions
	 {
		  public static Session XPOSession(this System.Web.UI.TemplateControl templateControl)
		  {
				return XPOUtils.GetSession(HttpContext.Current);
		  }
		  public static UnitOfWork XPOUnitOfWork(this System.Web.UI.TemplateControl templateControl)
		  {
				return XPOUtils.GetUnitOfWork(HttpContext.Current);
		  }
		  public static Session XPOSession(this System.Web.UI.TemplateControl templateControl, bool transactional)
		  {
				if (templateControl == null)
					 throw new ArgumentNullException("templateControl");
				if (HttpContext.Current == null)
					 throw new Exception("HttpContext.Current is null");
				return XPOUtils.GetSession(HttpContext.Current, transactional);
		  }

		  public static void XPORollback(this System.Web.UI.TemplateControl templateControl, object obj)
		  {
				XPOUtils.Rollback(obj);
		  }
		  public static void XPOCommit(this System.Web.UI.TemplateControl templateControl, object obj)
		  {
				XPOUtils.Commit(obj);
		  }
	 }

}
