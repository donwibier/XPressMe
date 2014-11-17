<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArticleControl.ascx.cs" Inherits="XPressMe.Web.CustomCtrls.ArticleControl" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxImageGallery" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Xpo.v14.1.Web, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Xpo" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>

<dx:ASPxRoundPanel ID="rpxPostContainer" runat="server" Width="100%" HeaderText='<%# Eval("Title") %>'>
    <PanelCollection>
        <dx:PanelContent runat="server">	        
	        <div class="metaContainer">
		        <span><%# String.Format("Posted at {0:ddd, MM d, yyyy HH:mm} by ", Eval("AddStamp"), Eval("AddUser")) %></span><br />
		        <span><%# String.Format("Group: {0}", Eval("GroupTitle")) %></span>
	        </div>
	        <asp:Panel runat="server" ID="tagPanel">
		        <span>Tags:
			        <asp:Repeater runat="server" ID="rptTags" DataSource='<%# Eval("Tags") %>'>
				        <ItemTemplate><%# Eval("Name") %></ItemTemplate>
				        <SeparatorTemplate>,</SeparatorTemplate>
			        </asp:Repeater>
		        </span>
	        </asp:Panel>

	        <p><%# Eval("Article") %></p>
	        <asp:Panel runat="server" ID="linkPanel" CssClass="floatRight">
		        <dx:ASPxHyperLink runat="server" ID="lnkMore" Text="More..." NavigateUrl='<%# String.Format("~/Post.aspx?ID={0}", Eval("ID")) %>' />
	        </asp:Panel>
	        <asp:Panel runat="server" ID="galleryPanel" Width="100%">
                <dx:ASPxImageGallery ID="ASPxImageSlider1" runat="server" EnableViewState="False"
                    DataSourceID="DSAttachments" 
			        NameField="FileName" 
                    BinaryImageCacheFolder="~\Temp\Thumbs" 
                    ImageContentBytesField="Data"
			        Width="100%" 
                    AllowPaging="False" Layout="Flow" 
                    OnDataBound="ASPxImageSlider1_DataBound">

			        <SettingsFolder ImageCacheFolder="~\Temp\Thumbs" />
			        <SettingsFullscreenViewer ImageSizeMode="FillAndCrop" />			
			        <Styles>
				        <Content>				
					        <Border BorderStyle="None" />
				        </Content>
			        </Styles>
		        </dx:ASPxImageGallery>
		

	        </asp:Panel>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>
<dx:XpoDataSource ID="DSAttachments" runat="server" Criteria="[Post!Key] = ?" OnInit="DSAttachments_Init" TypeName="XPressMe.Data.XPressMeDemoDB.XPOPostAttachment">
	<CriteriaParameters>
		<asp:QueryStringParameter Name="id" QueryStringField="ID" />
	</CriteriaParameters>
</dx:XpoDataSource>


