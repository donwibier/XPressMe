using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using XPressMe.Shared.WebForms;
using XPressMe.Data.XPressMeDemoDB;
using DevExpress.Web;
using DevExpress.Xpo;

namespace XPressMe.Web
{
    public partial class XPressMe : System.Web.UI.MasterPage
    {
        public Guid CurrentPostID { get; set; }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
                CurrentPostID = new Guid(Request.QueryString["ID"]);
            else
                CurrentPostID = Guid.Empty;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateMenu(dxMainMenu);
        }

        protected void UpdateMenu(ASPxMenu menu)
        {
            if (menu == null)
                return;

            MenuItem item = menu.Items.FindByName("Insert");
            if (item != null) item.Visible = this.Page is IHomepage;
            item = menu.Items.FindByName("Edit");
            if (item != null) item.Visible = this.Page is IPostPage;

            if (menu.Items.Count > 2)
            {
                for (int i = menu.Items.Count - 1; i > 1; i--)
                    menu.Items.RemoveAt(i);
            }

            using (XPCollection<XPOGroup> groups = new XPCollection<XPOGroup>(this.XPOSession()))
            {
                foreach (XPOGroup group in groups)
                {
                    menu.Items.Add(group.Title, String.Empty, string.Empty, String.Format("~/TagGroup.aspx?Group={0}", group.ID));
                }
            }
        }
        
        enum callbackCommand { None, Insert, Edit, Store };
        protected void pcxDash_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            callbackCommand cmd = callbackCommand.None;
            Enum.TryParse<callbackCommand>(e.Parameter, out cmd);

            IEditorInterface edt = null;
            foreach (System.Web.UI.Control ctrl in e.Window.Controls)
            {
                if (ctrl is IEditorInterface)
                {
                    edt = ctrl as IEditorInterface;
                    break;
                }
            }
            if (edt != null)
            {
                switch (cmd)
                {
                    case callbackCommand.None:
                        break;
                    case callbackCommand.Insert:
                        edt.LoadFromObject(null);
                        break;
                    case callbackCommand.Edit:
                        edt.LoadFromObject(CurrentPostID);
                        break;
                    case callbackCommand.Store:
                        edt.SaveToDataObject(CurrentPostID);
                        break;
                    default:
                        break;
                }
            }
        }

        protected void navContainer_Callback(object sender, CallbackEventArgsBase e)
        {
            if (e.Parameter == "Reload")
            {
                UpdateMenu(dxMainMenu);
            }
        }


        protected void navContainer_CustomJSProperties(object sender, CustomJSPropertiesEventArgs e)
        {
            e.Properties["cpCallbackCmd"] = "Reload";
        }

    }
}