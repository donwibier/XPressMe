﻿<%@ Page Title="" Language="C#" MasterPageFile="~/XPressMe.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="XPressMe.Web.DefaultPage" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register src="CustomCtrls/ArticleControl.ascx" tagname="ArticleControl" tagprefix="uc1" %>
<%@ Register Src="CustomCtrls/CloudWidget.ascx" TagName="TagCloudControl" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">	
    <div class="container content">
		<div class="twelve columns">
           <dx:ASPxDataView runat="server" ID="dxPosts" Width="100%" 
                OnCustomCallback="DataViewPosts_CustomCallback" 
                OnCustomJSProperties="DataViewPosts_CustomJSProperties">
				<SettingsTableLayout ColumnCount="1" RowsPerPage="4" />
				<PagerSettings ShowNumericButtons="False" EndlessPagingMode="OnScroll"></PagerSettings>
				<ItemStyle CssClass="contentContainer" />
				<ItemTemplate>
					<uc1:ArticleControl ID="ArticleControl1" runat="server" CompactMode="true" />
				</ItemTemplate>
				<ClientSideEvents
					 EndCallback="PostDataViewEndCallback"
					 Init="PostDataViewInit" />
			</dx:ASPxDataView>  
        </div>
		<div class="four columns">
            <p></p>
            <uc2:TagCloudControl ID="TagCloudControl1" runat="server" />
        </div>
    </div>
</asp:Content>
