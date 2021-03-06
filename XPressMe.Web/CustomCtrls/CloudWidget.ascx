﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CloudWidget.ascx.cs" Inherits="XPressMe.Web.CustomCtrls.CloudWidget" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>



<dx:ASPxCallbackPanel runat="server" ID="dxCloudContainer" CssClass="widgetContainer" Width="100%" 
	
	OnCallback="dxCloudContainer_Callback" OnCustomJSProperties="dxCloudContainer_CustomJSProperties" ShowLoadingPanel="False">
	<PanelCollection>
		<dx:PanelContent ID="PanelContent1" runat="server">
			<dx:ASPxCloudControl runat="server" ID="dxCloud" Width="100%">
			</dx:ASPxCloudControl>
		</dx:PanelContent>
	</PanelCollection>
</dx:ASPxCallbackPanel>
