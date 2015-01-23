using DevExpress.Xpo;
using DevExpress.Xpo.DB.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace XPressMe.Shared.Mvc
{
	 public abstract class XPOBaseController<TXPOClass, TKeyType> : Controller
		  where TXPOClass : XPBaseObject
	 {
		  enum DBOperation { Insert, Update, Delete };

		  public XPOBaseController()
				: base()
		  {
		  }

		  protected UnitOfWork XPOSession
		  {
				get { return XPOUtils.GetUnitOfWork(HttpContext); }
		  }

	 	  private bool DBAction(XPOBaseDTOModel<TXPOClass, TKeyType> model, DBOperation operation)
		  {
				if (model == null)
					 throw new ArgumentNullException("model");

				TXPOClass dataItem = operation == DBOperation.Insert ?
					 (TXPOClass)XPOSession.GetClassInfo<TXPOClass>().CreateNewObject(XPOSession) :
					 XPOSession.GetObjectByKey<TXPOClass>(model.ID);

				if (dataItem == null)
					 throw new Exception(String.Format("{0} dataItem with ID = '{1}' could not be located", typeof(TXPOClass).Name, model.ID));

				if (operation == DBOperation.Delete)
					 XPOSession.Delete(dataItem);
				else
					 model.AssignTo(dataItem);
				try
				{
					 XPOSession.CommitChanges();
					 return true;
				}
				catch (LockingException) { return false; }
		  }

		  protected virtual bool DBInsert(XPOBaseDTOModel<TXPOClass, TKeyType> model)
		  {
				return DBAction(model, DBOperation.Insert);
		  }

		  protected virtual bool DBUpdate(XPOBaseDTOModel<TXPOClass, TKeyType> model)
		  {
				return DBAction(model,DBOperation.Update);
		  }

		  protected virtual bool DBDelete(XPOBaseDTOModel<TXPOClass, TKeyType> model)
		  {
				return DBAction(model, DBOperation.Delete);
		  }
	 }

	 public abstract class XPOBaseIntKeyController<TXPOClass> : XPOBaseController<TXPOClass, int>
		  where TXPOClass : XPBaseObject
	 {
	 }

	 public abstract class XPOBaseGuidKeyController<TXPOClass> : XPOBaseController<TXPOClass, Guid>
		  where TXPOClass : XPBaseObject
	 {
	 }
}