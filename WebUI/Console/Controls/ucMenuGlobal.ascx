<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucMenuGlobal.ascx.cs" Inherits="CSI.WebUI.Console.Controls.ucMenuGlobal" %>

<%@ Register TagPrefix="uc" TagName="MenuNavigation" Src="~/Console/Controls/ucMenuNavigation.ascx" %>
<%@ Register TagPrefix="uc" TagName="Guide" Src="~/Console/Controls/ucGuide.ascx" %>

<div id="divMenuGlobal">
    <ul>
        <li id="liMenuGlobalDashboard" runat="server">
            <asp:LinkButton ID="lnkMenuGlobalDashboard" runat="server" PostBackUrl="~/Console/Dashboard/Dashboard.aspx">
                <span id="SpanMenuGlobalDashboard" class="spanIcon" runat="server"></span><asp:Label ID="lblMenuGlobalDashboard" CssClass="lblItem" runat="server"></asp:Label>    
            </asp:LinkButton>
        </li>
        <li id="liMenuGlobalSites" runat="server">
            <asp:LinkButton ID="lnkMenuGlobalSites" runat="server" PostBackUrl="~/Console/Dashboard/Sites.aspx">
                <span id="SpanMenuGlobalSites" class="spanIcon" runat="server"></span>
                <asp:Label ID="lblMenuGlobalSites" runat="server" CssClass="lblItem"></asp:Label>    
            </asp:LinkButton>
        </li>
        <li id="liMenuGlobalAddSite" runat="server">
            <asp:LinkButton ID="lnkMenuGlobalAddSite" runat="server" PostBackUrl="~/Console/Dashboard/SiteAdd.aspx">
                <span id="SpanMenuGlobalAddSite" class="spanIcon" runat="server"></span>
                <asp:Label ID="lblMenuGlobalAddSite" runat="server" CssClass="lblItem"></asp:Label>    
            </asp:LinkButton>
        </li>
        <!--  Menu for Navigation in Pages -->
        <uc:MenuNavigation ID="mnMenuNavigation" runat="server" />
    </ul>
</div>