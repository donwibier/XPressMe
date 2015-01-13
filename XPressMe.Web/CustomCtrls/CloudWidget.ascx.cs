using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Web;
using XPressMe.Shared.WebForms;
using XPressMe.Data.XPressMeDemoDB;
using DevExpress.Web.Internal;

namespace XPressMe.Web.CustomCtrls
{
    public partial class CloudWidget : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DatabindCloud(dxCloud);
        }
        protected void DatabindCloud(ASPxCloudControl cloud)
        {
            if (cloud == null)
                return;
            XPView view = new XPView(this.XPOSession(), typeof(XPOTag));
            view.Properties.AddRange(
                new ViewProperty[] { 
						new ViewProperty("ID", DevExpress.Xpo.SortDirection.None, XPOTag.Fields.ID, false, true),
						new ViewProperty("Name", SortDirection.None, XPOTag.Fields.Name, false, true),
						new ViewProperty("PostCount", SortDirection.None, CriteriaOperator.Parse("Posts[Active== true].Count"), false, true)
					});

            cloud.TextField = "Name";
            cloud.ValueField = "PostCount";
            cloud.NavigateUrlField = "ID";
            cloud.NavigateUrlFormatString = "~/TagGroup.aspx?Tag={0}";
            cloud.DataSource = view;
            cloud.DataBind();
        }
        protected void dxCloudContainer_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            if (e.Parameter == "Reload")
                DatabindCloud(dxCloud);
        }

        protected void dxCloudContainer_CustomJSProperties(object sender, DevExpress.Web.CustomJSPropertiesEventArgs e)
        {
            e.Properties["cpCallbackCmd"] = "Reload";

        }
    }
}