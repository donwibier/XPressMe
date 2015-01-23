using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XPressMe.Mvc.ViewModels
{
	 public class PostViewModel : BaseViewModel
	 {
		  protected override bool MenuShowInsertItem { get { return false; } }
		  protected override bool MenuShowEditItem { get { return true; } }
		  
	 }
}