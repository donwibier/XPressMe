using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxDataView;
using XPressMe.Shared;
using XPressMe.Data.XPressMeDemoDB;
using DevExpress.Web.ASPxClasses;

namespace XPressMe.Web
{
    public partial class PostPage : BasePage, IPostPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override ASPxDataView DataViewCtrl
        {
            get { return dxPosts; }
        }

        protected override CriteriaOperator DataViewCriteriaOperator
        {
            get { return CriteriaOperator.Parse("ID == ?", Request.QueryString["ID"]); }
        }
    }
}