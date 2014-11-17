using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxDataView;
using DevExpress.Web.ASPxClasses;
using XPressMe.Shared;
using XPressMe.Data.XPressMeDemoDB;

namespace XPressMe.Web
{
    public abstract class BasePage : System.Web.UI.Page
    {
        protected abstract ASPxDataView DataViewCtrl { get; }
        protected abstract CriteriaOperator DataViewCriteriaOperator { get; }
        protected virtual SortProperty[] DataViewSortProperty
        {
            get
            {
                return new SortProperty[] 
                { 
                    new SortProperty("AddStamp", SortingDirection.Descending) 
                };
            }
        }
        protected virtual string CallbackReloadCommand { get { return "Reload"; } }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DatabindView(DataViewCtrl);
        }
        
        protected void DatabindView(ASPxDataView view)
        {
            if (view == null)
                return;

            XPCollection<XPOPost> result = new XPCollection<XPOPost>(
                this.XPOSession(),
                DataViewCriteriaOperator, 
                DataViewSortProperty
            );

            view.DataSource = result;
            view.DataBind();
        }

        protected void DataViewPosts_CustomJSProperties(object sender, CustomJSPropertiesEventArgs e)
        {
            e.Properties["cpCallbackCmd"] = CallbackReloadCommand;
        }

        protected void DataViewPosts_CustomCallback(object sender, CallbackEventArgsBase e)
        {
            if (e.Parameter == CallbackReloadCommand)
            {
                ASPxDataView view = sender as ASPxDataView;
                if (view != null)
                {
                    DatabindView(view);
                }
            }
        }
    }
}