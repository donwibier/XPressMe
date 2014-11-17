<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PostControl.ascx.cs" Inherits="XPressMe.Web.CustomCtrls.PostControl" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxImageGallery" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxImageSlider" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Xpo.v14.1.Web, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Xpo" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxFormLayout" TagPrefix="dx" %>

<div class="popupPostControl">

<dx:ASPxFormLayout ID="frmLayout" runat="server" Width="100%" 
        ClientInstanceName="quickPostForm" RequiredMarkDisplayMode="None">
		<Items>
			<dx:LayoutItem Caption="Title" Name="Title">
				<LayoutItemNestedControlCollection>
					<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">
						<dx:ASPxTextBox ID="tbxTitle" runat="server" Width="100%" AutoResizeWithContainer="True" NullText="Post title *">
							<ValidationSettings ErrorDisplayMode="None">
								<RequiredField IsRequired="True" />
							</ValidationSettings>
						</dx:ASPxTextBox>
					</dx:LayoutItemNestedControlContainer>
				</LayoutItemNestedControlCollection>
			</dx:LayoutItem>
			<dx:LayoutItem Caption="Is Active" ShowCaption="False" Name="IsActive">
				<LayoutItemNestedControlCollection>
					<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">
						<dx:ASPxCheckBox ID="cbxActive" runat="server" CheckState="Unchecked" Text="This post is active" Width="100%">
						</dx:ASPxCheckBox>
					</dx:LayoutItemNestedControlContainer>
				</LayoutItemNestedControlCollection>
			</dx:LayoutItem>
			<dx:LayoutItem Caption="Group" Name="Group">
				<LayoutItemNestedControlCollection>
					<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" SupportsDisabledAttribute="True">
						<dx:ASPxComboBox ID="cbxGroup" runat="server" DataSourceID="DSGroups" NullText="Select a group"
							TextField="Title" ValueField="ID" Width="100%" DropDownStyle="DropDown" AutoResizeWithContainer="True">
							<ValidationSettings ErrorDisplayMode="None">
							</ValidationSettings>
						</dx:ASPxComboBox>
					</dx:LayoutItemNestedControlContainer>
				</LayoutItemNestedControlCollection>
			</dx:LayoutItem>
			<dx:LayoutItem Name="Tags">
				<LayoutItemNestedControlCollection>
					<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" SupportsDisabledAttribute="True">
						<dx:ASPxTokenBox ID="tkbxTags" runat="server" AllowMouseWheel="True" AutoResizeWithContainer="True" DataSourceID="DSTags" IncrementalFilteringMode="Contains" NullText="Select tags"
							TextField="Name" Tokens="" ValueField="ID" Width="100%">
						</dx:ASPxTokenBox>
					</dx:LayoutItemNestedControlContainer>
				</LayoutItemNestedControlCollection>
			</dx:LayoutItem>
			<dx:LayoutItem Caption="Post Text" Name="PostText" ShowCaption="False">
				<LayoutItemNestedControlCollection>
					<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" SupportsDisabledAttribute="True">
						<dx:ASPxMemo ID="mmoArticle" runat="server" AutoResizeWithContainer="True" Height="118px" NullText="Article *" Width="100%">
							<ValidationSettings ErrorDisplayMode="None">
								<RequiredField IsRequired="True" />
							</ValidationSettings>
						</dx:ASPxMemo>
					</dx:LayoutItemNestedControlContainer>
				</LayoutItemNestedControlCollection>
			</dx:LayoutItem>
			<dx:LayoutItem Caption="Images" Name="Images">
				<LayoutItemNestedControlCollection>
					<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" SupportsDisabledAttribute="True">
						<dx:ASPxUploadControl ID="upxImages" runat="server" NullText="Select one or more images to attach" ShowProgressPanel="True" UploadMode="Auto" Width="100%"							
                            OnFilesUploadComplete="upxImages_FilesUploadComplete">
							<ClientSideEvents TextChanged="function(s, e) {	s.Upload(); }" FilesUploadComplete="function(s, e) { prv.PerformCallback(''); }" />
							<AdvancedModeSettings EnableMultiSelect="True" TemporaryFolder="~\App_Data\UploadTemp" />
						</dx:ASPxUploadControl>
					</dx:LayoutItemNestedControlContainer>
				</LayoutItemNestedControlCollection>
			</dx:LayoutItem>	
            <dx:LayoutItem  Name="UploadPreview">
				<LayoutItemNestedControlCollection>
					<dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server" SupportsDisabledAttribute="True">
                        <dx:ASPxImageGallery ID="PreviewSlider" runat="server" EnableViewState="False" ClientInstanceName="prv"
			                NameField="FileName" 
                            BinaryImageCacheFolder="~\Temp\Thumbs" 
                            ImageContentBytesField="Content"
			                Width="100%" 
                            AllowPaging="False" Layout="Flow"                             
                            OnCustomCallback="PreviewSlider_CustomCallback" ThumbnailHeight="100px" ThumbnailWidth="100px">

			                <SettingsFolder ImageCacheFolder="~\Temp\Thumbs" />
			                <SettingsFullscreenViewer ImageSizeMode="FillAndCrop" />			
			                <Styles>
				                <Content>				
					                <Border BorderStyle="None" />
				                </Content>
			                </Styles>
		                </dx:ASPxImageGallery>    

                               				
					</dx:LayoutItemNestedControlContainer>
				</LayoutItemNestedControlCollection>
			</dx:LayoutItem>	            		
		</Items>
		<SettingsItems ShowCaption="False" />
		<SettingsItemCaptions Location="Top" />
	</dx:ASPxFormLayout>	
</div>

<dx:XpoDataSource ID="DSGroups" runat="server" DefaultSorting="Title" OnInit="DSGroups_Init" TypeName="XPressMe.Data.XPressMeDemoDB.XPOGroup">
</dx:XpoDataSource>
<dx:XpoDataSource ID="DSTags" runat="server" DefaultSorting="Name" OnInit="DSGroups_Init" TypeName="XPressMe.Data.XPressMeDemoDB.XPOTag">
</dx:XpoDataSource>


