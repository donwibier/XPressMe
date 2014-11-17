using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Helpers;
using DevExpress.Xpo.Metadata;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace XPressMe.Shared
{
	public static class XPOWebExtensions
	{
        public static Session XPOSession(this System.Web.UI.TemplateControl templateControl)
		{
			return XPOSession(HttpContext.Current, false);
		}
        public static UnitOfWork XPOUnitOfWork(this System.Web.UI.TemplateControl templateControl)
        {
            return XPOSession(HttpContext.Current, true) as UnitOfWork;
        }
        public static Session XPOSession(this System.Web.UI.TemplateControl templateControl, bool transactional)
		{
            return XPOSession(HttpContext.Current, transactional);
		}

        public static void XPORollback(this System.Web.UI.TemplateControl templateControl, object obj)
		{
			XPOUtils.Rollback(obj);
		}
        public static void XPOCommit(this System.Web.UI.TemplateControl templateControl, object obj)
		{
			XPOUtils.Commit(obj);
		}

        static readonly Dictionary<bool, string> xpoNames = new Dictionary<bool, string>()
        {
            {false, "XPOSession"},
            {true, "XPOUnitOfWork"}
        };
		public static Session XPOSession(HttpContext context, bool transactional)
		{
			if (context != null)
			{
				if (context.Items[xpoNames[transactional]] == null)
				{
					Session s = XPOUtils.GetNew(transactional);
					s.Disposed += s_Disposed;
                    context.Items[xpoNames[transactional]] = s;
				}
                return context.Items[xpoNames[transactional]] as Session;
			}
			return XPOUtils.GetNew(transactional);
		}

		static void s_Disposed(object sender, EventArgs e)
		{
            bool transactional = sender is UnitOfWork;

			if ((HttpContext.Current != null) && (sender != null) &&
                (((Session)HttpContext.Current.Items[xpoNames[transactional]]).Equals((Session)sender)))
                HttpContext.Current.Items[xpoNames[transactional]] = null;
		}
	}
}
