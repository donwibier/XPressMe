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
using DevExpress.Web.ASPxUploadControl;
using System.IO;
using DevExpress.Web.ASPxClasses.Internal;
using DevExpress.Web.ASPxFormLayout;
using DevExpress.Web.ASPxImageGallery;
using System.Web.Hosting;
using DevExpress.Web.ASPxCallbackPanel;

namespace XPressMe.Web.CustomCtrls
{
    public class TempUploadItem
    {
        public string LocalFile { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public byte[] Content
        {
            get
            {
                if (File.Exists(LocalFile))
                    return File.ReadAllBytes(LocalFile);
                return null;
            }
        }

        public void CleanupLocalFile()
        {
            if (File.Exists(LocalFile))
            {
                try
                {
                    File.Delete(LocalFile);
                }
                catch { }
                finally { }
            }
        }
    }

    public partial class PostControl : System.Web.UI.UserControl, IEditorInterface
    {
        const string SessionName = "DXPostUp";
        
        protected void DSGroups_Init(object sender, EventArgs e)
        {
            ((XpoDataSource)sender).Session = this.XPOSession();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void upxImages_FilesUploadComplete(object sender, DevExpress.Web.ASPxUploadControl.FilesUploadCompleteEventArgs e)
        {
            ASPxUploadControl ctrl = sender as ASPxUploadControl;
            if (ctrl != null)
            {
                List<TempUploadItem> fileInfo = (Session[SessionName] as List<TempUploadItem>) ?? new List<TempUploadItem>();                
                foreach (UploadedFile f in ctrl.UploadedFiles)
                {
                    FileStream sourceContent = f.FileContent as FileStream;
                    if (sourceContent != null && f.IsValid)
                    {
                        string newLocalFile = Path.ChangeExtension(sourceContent.Name, f.FileName);
                        using (FileStream destContent = File.Create(newLocalFile))
                        {
                            sourceContent.CopyTo(destContent);
                            fileInfo.Add(new TempUploadItem { LocalFile = destContent.Name, FileName = f.FileName, MimeType = f.ContentType });
                        }
                    }
                }
                Session[SessionName] = fileInfo;
            }
        }

        public void LoadFromObject(object id)
        {
            if (id != null)
            {
                XPOPost post = this.XPOUnitOfWork().FindObject<XPOPost>(XPOPost.Fields.ID == (Guid)id);
                if (post != null)
                {
                    tbxTitle.Text = post.Title;
                    cbxActive.Checked = post.Active;
                    mmoArticle.Text = post.Article;
                    cbxGroup.Value = post.Group.ID.ToString();
                    tkbxTags.Tokens.Clear();
                    foreach (XPOTag tag in post.Tags)
                    {
                        tkbxTags.Tokens.Add(tag.Name);
                    }
                    return;
                }
            }

            tbxTitle.Text = String.Empty; ;
            cbxActive.Checked = false;
            mmoArticle.Text = string.Empty;
            cbxGroup.Value = null;
            tkbxTags.Tokens.Clear();
        }

        public void SaveToDataObject(object id)
        {
            UnitOfWork wrk = this.XPOUnitOfWork();
            XPOPost post;
            if (id == null || (Guid)id == Guid.Empty)
                post = new XPOPost(wrk);
            else
                post = wrk.FindObject<XPOPost>(CriteriaOperator.Parse("ID == ?", id));

            if (post != null)
            {
                post.Title = tbxTitle.Text;
                post.Active = cbxActive.Checked;
                post.Article = mmoArticle.Text;

                Guid groupID;
                if (Guid.TryParse((string)cbxGroup.Value, out groupID))
                    post.Group = wrk.FindObject<XPOGroup>(XPOGroup.Fields.ID == new Guid((string)cbxGroup.Value));
                else if (!String.IsNullOrEmpty((string)cbxGroup.Text))
                {
                    post.Group = wrk.FindObject<XPOGroup>(XPOGroup.Fields.Title == cbxGroup.Text);
                    if (post.Group == null)
                        post.Group = new XPOGroup(wrk) { Title = cbxGroup.Text };
                }

                this.XPOCommit(post); // make sure we have the post id

                // CHANGE: There is a little error in video
                // 
                XPCollection<XPOTag> tags = new XPCollection<XPOTag>(wrk, XPOTag.Fields.Posts[XPOPost.Fields.ID == post.ID], null);
                foreach (XPOTag tag in tags)
                {
                    post.Tags.Remove(tag); //remove all tags from relation with post
                }
                this.XPOCommit(post);

                foreach (string token in tkbxTags.Tokens)
                {
                    XPOTag tag = wrk.FindObject<XPOTag>(XPOTag.Fields.Name == token);
                    if (tag == null)
                    {
                        tag = new XPOTag(wrk) { Name = token };
                    }
                    post.Tags.Add(tag);
                }
                this.XPOCommit(post);

                List<TempUploadItem> fileInfo = Session[SessionName] as List<TempUploadItem>;                
                if (fileInfo != null)
                {
                    foreach (TempUploadItem file in fileInfo)
                    {
                        if (File.Exists(file.LocalFile))
                        {
                            XPOPostAttachment attach = new XPOPostAttachment(wrk)
                            {
                                Data = File.ReadAllBytes(file.LocalFile),
                                FileName = file.FileName,
                                MimeType = file.MimeType,
                                Post = post
                            };
                            file.CleanupLocalFile();
                        }
                    }
                    Session.Remove(SessionName);
                }
                this.XPOCommit(post);
            }
        }
        protected void DataBindPreviewGallery()
        {
            List<TempUploadItem> fileInfo = Session[SessionName] as List<TempUploadItem>;
            if (fileInfo != null)
            {
                PreviewSlider.DataSource = fileInfo;
                PreviewSlider.DataBind();
            }
        }
        protected void PreviewSlider_CustomCallback(object sender, CallbackEventArgsBase e)
        {
            DataBindPreviewGallery();
        }

    }
}