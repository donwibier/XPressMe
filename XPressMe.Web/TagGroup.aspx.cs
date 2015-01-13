using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Web;
using XPressMe.Shared.WebForms;
using XPressMe.Data.XPressMeDemoDB;
using System.Text;

namespace XPressMe.Web
{
    public partial class TagGroupPage : BasePage
    {
        private Guid tagID = Guid.Empty;
        private Guid groupID = Guid.Empty;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            string s = Request.QueryString["Tag"];
            if (!String.IsNullOrEmpty(s))
                tagID = new Guid(s);

            s = Request.QueryString["Group"];
            if (!String.IsNullOrEmpty(s))
                groupID = new Guid(s);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateTagLabel();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void UpdateTagLabel()
        {
            StringBuilder sb = new StringBuilder();
            if (tagID != Guid.Empty)
            {
                XPOTag tag = this.XPOSession().FindObject<XPOTag>(XPOTag.Fields.ID == tagID);
                if (tag != null)
                    sb.AppendFormat("<h2>Tag: {0}</h2>\n", tag.Name);
            }
            if (groupID != Guid.Empty)
            {
                XPOGroup group = this.XPOSession().FindObject<XPOGroup>(XPOGroup.Fields.ID == groupID);
                if (group != null)
                    sb.AppendFormat("<h2>Group: {0}</h2>\n", group.Title);
            }
            hdr.Text = sb.ToString();
        }

        protected override ASPxDataView DataViewCtrl
        {
            get { return dxPosts; }
        }

        protected override CriteriaOperator DataViewCriteriaOperator
        {
            get
            {
                List<CriteriaOperator> crit = new List<CriteriaOperator>();
                crit.Add(CriteriaOperator.Parse("Active == ?", true));
                if (tagID != Guid.Empty)
                    crit.Add(XPOPost.Fields.Tags[XPOTag.Fields.ID == tagID]);
                if (groupID != Guid.Empty)
                    crit.Add(CriteriaOperator.Parse("[Group!Key] == ?", groupID));
                return BinaryOperator.And(crit);
            }
        }

    }
}