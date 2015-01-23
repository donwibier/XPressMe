using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XPressMe.Mvc.Models;

namespace XPressMe.Mvc.ViewModels
{
	 public abstract class BaseViewModel
	 {		  
		  protected abstract bool MenuShowInsertItem { get;  }
		  protected abstract bool MenuShowEditItem { get;  }

		  public IEnumerable<DevExpress.Web.MenuItem> MenuItems
		  {
				get
				{
					 DevExpress.Web.MenuItemCollection result = new DevExpress.Web.MenuItemCollection();
					 if (MenuShowInsertItem)
						  result.Add("+", "Insert", String.Empty, String.Empty);
					 if (MenuShowEditItem)
						  result.Add("#", "Edit", String.Empty, String.Empty);

					 if (Groups != null)
					 {
						  result.AddRange(
								from g in Groups
								select new DevExpress.Web.MenuItem(g.Title, String.Empty, String.Empty, String.Format("~/TagGroup.aspx?Group={0}", g.ID)));
					 }
					 return result;
				}
		  }
		  public Guid ID { get; set; }

		  public IEnumerable<GroupModel> Groups { get; set; }
		  public IEnumerable<PostModel> Posts { get; set; }
		  public IEnumerable<TagModel> Tags { get; set; }
	 }
}