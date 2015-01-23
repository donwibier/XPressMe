using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XPressMe.Mvc.Models;

namespace XPressMe.Mvc.ViewModels
{
	 public class HomeViewModel : BaseViewModel
	 {
		  protected override bool MenuShowInsertItem { get { return true; } }
		  protected override bool MenuShowEditItem { get { return false; } }

	 }
}