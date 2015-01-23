using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace XPressMe.Shared.Mvc
{
	 public abstract class XPOBaseDTOModel<TXPOClass, TKeyType>
		  where TXPOClass : XPBaseObject
	 {
		  public XPOBaseDTOModel()
		  {

		  }

		  public XPOBaseDTOModel(TXPOClass model)
				: this()
		  {
				if (model != null)
					 AssignFrom(model);
		  }

		  public virtual TKeyType ID { get; set; }

		  public abstract void AssignTo(TXPOClass model);
		  public abstract void AssignFrom(TXPOClass model);
	 }

	 public abstract class XPOBaseDTOModelIntKey<TXPOClass>: XPOBaseDTOModel<TXPOClass, int>
		  where TXPOClass : XPBaseObject
	 {
		  public XPOBaseDTOModelIntKey()
		  {
				
		  }
		  public XPOBaseDTOModelIntKey(TXPOClass model)
				: base(model)
		  {
				
		  }
	 }

	 public abstract class XPOBaseDTOModelGuidKey<TXPOClass>: XPOBaseDTOModel<TXPOClass, Guid>
		  where TXPOClass : XPBaseObject
	 {
		  public XPOBaseDTOModelGuidKey()
		  {
				
		  }
		  public XPOBaseDTOModelGuidKey(TXPOClass model)
				: base(model)
		  {
				
		  }
	 }

}