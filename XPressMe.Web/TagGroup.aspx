<%@ Page Title="" Language="C#" MasterPageFile="~/XPressMe.Master" AutoEventWireup="true" CodeBehind="TagGroup.aspx.cs" Inherits="XPressMe.Web.TagGroupPage" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register src="CustomCtrls/ArticleControl.ascx" tagname="ArticleControl" tagprefix="uc1" %>
<%@ Register Src="CustomCtrls/CloudWidget.ascx" TagName="TagCloudControl" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">	
    <div class="container content">
		<div class="twelve columns">
			<div class="contentContainer">
				<asp:Literal runat="server" ID="hdr"></asp:Literal>
			</div>
			<dx:ASPxDataView ID="dxPosts" runat="server" ColumnCount="1" Width="100%" 
                OnCustomJSProperties="DataViewPosts_CustomJSProperties" 
                ItemSpacing="0px">
				<SettingsTableLayout ColumnCount="1" RowsPerPage="4" />
				<PagerSettings ShowNumericButtons="False" EndlessPagingMode="OnClick"></PagerSettings>
				<ItemTemplate>
					<uc1:ArticleControl ID="ArticleControl1" runat="server" CompactMode="true" />
				</ItemTemplate>
				<ContentStyle>
					<Border BorderStyle="None" />
				</ContentStyle>
				<ItemStyle CssClass="contentContainer" />
				<ClientSideEvents
					EndCallback="function(s,e) { $('.dxdvItem_Moderno').css('height', 0); } "
					Init="function(s,e) { $('.dxdvItem_Moderno').css('height', 0); } " />
			</dx:ASPxDataView>
		</div>
		<div class="four columns">
            <uc2:TagCloudControl ID="TagCloudControl1" runat="server" />
		</div>
	</div>
</asp:Content>
