<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PopupFooter.ascx.cs" Inherits="XPressMe.Web.CustomCtrls.PopupFooter" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<div class="popupFooter">
    <div class="popupFooterSpacer">&nbsp;</div>
    <div class="popupFooterButtonOK">
        <dx:ASPxButton ID="btxOK" runat="server" Text="OK" AutoPostBack="false" Width="100%">
            <Image IconID="actions_apply_16x16">
            </Image>
            <ClientSideEvents Click="popupOkButtonClick" />
        </dx:ASPxButton>
    </div>
    <div class="popupFooterButtonCancel">    
        <dx:ASPxButton ID="btxCancel" runat="server" Text="Cancel" AutoPostBack="false" Width="100%">
            <Image IconID="actions_cancel_16x16">
            </Image>
            <ClientSideEvents Click="popupCancelButtonClick" />
        </dx:ASPxButton>
	    
    </div>
</div>

