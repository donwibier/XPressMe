using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Data.Filtering;
using DevExpress.Web;
using XPressMe.Shared;
using XPressMe.Data.XPressMeDemoDB;

namespace XPressMe.Web
{
    public partial class DefaultPage : BasePage, IHomepage
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
            get { return CriteriaOperator.Parse("Active == ?", true); }
        }
    }
}