<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucMenuNavigation.ascx.cs" Inherits="CSI.WebUI.Console.Controls.ucMenuNavigation" %>

<asp:LinkButton ID="lnkMenuNavigationSite" runat="server" CssClass="lnkSite"></asp:LinkButton>

<li id="liMenuNavigationSiteProfile" runat="server">
    <a runat="server" id="lnkMenuNavigationSiteProfile">
        <span class="spanIcon"></span>
        <asp:Label ID="lblMenuNavigationSiteProfile" runat="server" CssClass="lblItem"></asp:Label>
    </a>
</li>
<li id="liMenuNavigationSiteMeters" runat="server">
    <a id="lnkMenuNavigationSiteMeters" runat="server">
        <span class="spanIcon"></span>
        <asp:Label ID="lblMenuNavigationSiteMeters" runat="server" CssClass="lblItem"></asp:Label>
    </a>
</li>
<li id="liMenuNavigationSiteMeter" runat="server">
    <a ID="lnkMenuNavigationSiteMeter" runat="server">
        <span id="Span3" class="spanIcon" runat="server"></span>
        <asp:Label ID="lblMenuNavigationSiteMeter" runat="server" CssClass="lblItem active"></asp:Label>
        <div class="clear">
        </div>
    </a>
</li>
<li id="liMenuNavigationSiteMeterSerie" runat="server">
    <a id="lnkMenuNavigationSiteMeterSerie" runat="server">
        <span id="Span4" class="spanIcon" runat="server"></span>
        <asp:Label ID="lblMenuNavigationSiteMeterSerie" runat="server" CssClass="lblItem"></asp:Label>
    </a>
</li>
<li id="liMenuNavigationSiteMeterLoad" runat="server">
    <a id="lnkMenuNavigationSiteMeterLoad" runat="server">
        <span id="Span5" class="spanIcon" runat="server"></span>
        <asp:Label ID="lblMenuNavigationSiteMeterLoad" runat="server" CssClass="lblItem"></asp:Label>
    </a>
</li>
<li id="liMenuNavigationSiteTargets" runat="server">
    <a id="lnkMenuNavigationSiteTargets" runat="server">
        <span id="Span6" class="spanIcon" runat="server"></span>
        <asp:Label ID="lblMenuNavigationSiteTargets" runat="server" CssClass="lblItem"></asp:Label>
    </a>
</li>
<li id="liMenuNavigationSitePayments" runat="server">
    <a id="lnkMenuNavigationSitePayments" runat="server">
        <span id="Span7" class="spanIcon" runat="server"></span>
        <asp:Label ID="lblMenuNavigationSitePayments" runat="server" CssClass="lblItem"></asp:Label>
    </a>
</li>
<li id="liMenuNavigationSiteEFs" runat="server">
    <a id="lnkMenuNavigationSiteEFs" runat="server">
        <span id="Span8" class="spanIcon" runat="server"></span>
        <asp:Label ID="lblMenuNavigationSiteEFs" runat="server" CssClass="lblItem"></asp:Label>
    </a>
</li>
<li id="liMenuNavigationSitePermissions" runat="server">
    <a id="lnkMenuNavigationSitePermissions" runat="server">
        <span id="Span9" class="spanIcon" runat="server"></span>
        <asp:Label ID="lblMenuNavigationSitePermissions" runat="server" CssClass="lblItem"></asp:Label>
    </a>
</li>
<li id="liMenuNavigationSiteReports" runat="server">
    <a id="lnkMenuNavigationSiteReports" runat="server">
        <span id="Span10" class="spanIcon" runat="server"></span>
        <asp:Label ID="lblMenuNavigationSiteReports" runat="server" CssClass="lblItem"></asp:Label>
    </a>
</li>