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
using XPressMe.Shared.WebForms;
using XPressMe.Data.XPressMeDemoDB;

namespace XPressMe.Web.CustomCtrls
{
    public partial class ArticleControl : System.Web.UI.UserControl
    {
        public bool CompactMode
        {
            get { return !tagPanel.Visible; }
            set
            {
                tagPanel.Visible = !value;
                galleryPanel.Visible = !value;
                linkPanel.Visible = value;
            }
        }

        protected void DSAttachments_Init(object sender, EventArgs e)
        {
            ((XpoDataSource)sender).Session = this.XPOSession();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ASPxImageSlider1_DataBound(object sender, EventArgs e)
        {
            ASPxImageGallery gallery = sender as ASPxImageGallery;
            if (gallery != null)
                gallery.Visible = gallery.Items.Count > 0;
        }
        
    }
}